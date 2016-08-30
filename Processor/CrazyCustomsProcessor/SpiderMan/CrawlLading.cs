using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrazyCustomsEntity.WebEntity;
using ProcessorUtilities;
using System.Xml;

namespace SpiderMan
{
    public class CrawlLading
    {
        #region Public Method
        public LadingBillNumberWebEntity CrawlLadingByBillNumber(string billNumber, string conveyance = "", string voyageNumber = "")
        {
            //LadingBillNumberWebEntity entity = new LadingBillNumberWebEntity();

            if (!string.IsNullOrEmpty(billNumber))
            {
                if (string.IsNullOrEmpty(conveyance) || string.IsNullOrEmpty(voyageNumber))
                {
                    //取出多条，取第一条船名和航次
                    //
                    string htmlContent = HtmlParseUtils.SaveWebPage(string.Format("http://edi.easipass.com/dataportal/query.do?ctno=&blno={0}&pagesize=100&submit=%E6%9F%A5%E8%AF%A2&qn=dp_cst_vsl", billNumber), Encoding.UTF8);
                    if (!string.IsNullOrEmpty(htmlContent))
                    {
                        var ret = ParseAdmissionLadingDeclaration(billNumber, htmlContent);
                        if (ret == null)
                        {
                            string realUrl = GetRealUrl(billNumber, htmlContent);
                            if (!string.IsNullOrEmpty(realUrl))
                            {
                                htmlContent = HtmlParseUtils.SaveWebPage(realUrl);
                                if (!string.IsNullOrEmpty(htmlContent))
                                {
                                    ret = ParseAdmissionLadingDeclaration(billNumber, htmlContent);
                                }
                            }
                        }
                        return ret;
                    }
                }
                else
                {
                    string htmlContent = HtmlParseUtils.SaveWebPage(string.Format("http://edi.easipass.com/dataportal/query.do?qn=dp_cst_query_billdetail&blno={0}&vslname={1}&voyage={2}", billNumber, conveyance, voyageNumber), Encoding.UTF8);
                    if (!string.IsNullOrEmpty(htmlContent))
                    {
                        var ret = ParseAdmissionLadingDeclaration(billNumber, htmlContent);
                        return ret;
                    }
                }
            }

            return null;
        }

        public LadingContainerNumberWebEntity CrawlLadingByContainerNumber(string containerNumber, string conveyance = "", string voyageNumber = "")
        {
            if (!string.IsNullOrEmpty(containerNumber))
            {
                if (string.IsNullOrEmpty(conveyance) || string.IsNullOrEmpty(voyageNumber))
                {
                    CrawlDockEntry crawlDock = new CrawlDockEntry();
                    List<DockEntryWebEntity> lstdockEntry = crawlDock.CrawlByContainerNumber(containerNumber);
                    var dock = lstdockEntry.OrderByDescending(o=>o.Time).FirstOrDefault(o=>o.Target == "出口");
                    if (dock != null)
                    {
                        ParseAdmissionLadingDeclarationByContainerNumber(containerNumber, dock.Conveyance, dock.VoyageNumber);
                    }
                }
                else
                {
                    ParseAdmissionLadingDeclarationByContainerNumber(containerNumber, conveyance, voyageNumber);
                }
            }

            return null;
        }

        #endregion

        #region Private method

        private LadingContainerNumberWebEntity ParseAdmissionLadingDeclarationByContainerNumber(string containerNumber, string conveyance, string voyageNumber)
        {
            LadingContainerNumberWebEntity ret = new LadingContainerNumberWebEntity();
            string url = string.Format("http://edi.easipass.com/dataportal/query.do?qn=dp_cst_query_ctndetail&ctno={0}&vslname={1}&voyage={2}", containerNumber, conveyance, voyageNumber);
            string htmlContent = HtmlParseUtils.SaveWebPage(url, Encoding.UTF8);
            string content = HtmlParseUtils.FormatHtml(htmlContent, false, true);
            if (!string.IsNullOrEmpty(content))
            {
                var headList = HtmlParseUtils.GetSubStrings(content, "<td align=\"left\" width=\"200\">", null, "</td>", "<td align=\"left\" width=\"200\">", null, "</td>");
                ret.ContainerNumber = containerNumber;
                ret.Conveyance = conveyance;
                ret.VoyageNumber = voyageNumber;

                var LadingDate = HtmlParseUtils.GetSubString(content, "<td align=\"left\">进港时间:", null, "</td>", "<td align=\"left\">进港时间:", null, "</td>");
                var LadingDock = HtmlParseUtils.GetSubString(content, "<td align=\"left\">进港地点:", null, "</td>", "<td align=\"left\">进港地点:", null, "</td>");
                var OwnerCode = HtmlParseUtils.GetSubString(content, "<td align=\"left\">箱经营人代码:", null, "</td>", "<td align=\"left\">箱经营人代码:", null, "</td>");
                var COSTCOCode = HtmlParseUtils.GetSubString(content, "<td align=\"left\">COSTCO报文号:", null, "</td>", "<td align=\"left\">COSTCO报文号:", null, "</td>");
                var COSTCOSentDate = HtmlParseUtils.GetSubString(content, "<td align=\"left\" colspan=\"2\">COSTCO报文发送时间:", null, "</td>", "<td align=\"left\" colspan=\"2\">COSTCO报文发送时间:", null, "</td>");

                if (!string.IsNullOrEmpty(LadingDate))
                {
                    ret.LadingDate = Constant.ConvertToDateTime(LadingDate.Trim());
                }
                if (!string.IsNullOrEmpty(LadingDock))
                {
                    ret.Dock = LadingDock.Trim();
                }
                if (!string.IsNullOrEmpty(OwnerCode))
                {
                    ret.Owner = OwnerCode.Trim();
                }
                if (!string.IsNullOrEmpty(COSTCOCode))
                {
                    ret.COSTRPSentNumber = COSTCOCode.Trim();
                }
                if (!string.IsNullOrEmpty(COSTCOSentDate))
                {
                    ret.COSTRPSentDate = Constant.ConvertToDateTime(COSTCOSentDate.Trim());
                }

                ret.LadingDetail = new List<LadingContainerNumberDetailWebEntity>();
                var detailList = HtmlParseUtils.GetSubStrings(content, "<tr height=\"21\">", null, "</tr>", "<tr height=\"21\">", null, "</tr>");
                if (detailList != null)
                {
                    foreach (var detail in detailList)
                    {
                        var detailContent = HtmlParseUtils.GetSubStrings(detail, "<td align=\"left\" bgcolor=\"#[1E]+\">", null, "</td>", "<td align=\"left\" bgcolor=\"#E1E1E1\">", "<td align=\"left\" bgcolor=\"#EEEEEE\">", "</td>");

                        if (detailContent != null)
                        {
                            LadingContainerNumberDetailWebEntity detailEntity = new LadingContainerNumberDetailWebEntity();

                            detailEntity.BillNumber = detailContent[0].Trim();
                            detailEntity.Weight = detailContent[1].Trim();
                            detailEntity.Amount = detailContent[2].Trim();
                            detailEntity.CustomsInfo = detailContent[3].Trim();
                            detailEntity.ReveivedDate = Constant.ConvertToDateTime(detailContent[4].Trim());
                            detailEntity.LadingCode = detailContent[5].Trim();
                            detailEntity.COSTRPNumber = detailContent[6].Trim();

                            ret.LadingDetail.Add(detailEntity);
                        }
                    }
                }
                return ret;
            }
            return null;
        }

        private string GetRealUrl(string billNumber, string htmlContent)
        {
            string content = HtmlParseUtils.FormatHtml(htmlContent, false, true);
            string strUrlTemp = string.Empty;
            List<string> headList = HtmlParseUtils.GetSubStrings(content, "<td align=\"left\" bgcolor=\"#[A-Za-z0-9]+\">", null, "</td>", "<td align=\"left\" bgcolor=\"#EEEEEE\">", "<td align=\"left\" bgcolor=\"#E1E1E1\">", "</td>");
            if (headList != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        strUrlTemp = string.Format("http://edi.easipass.com/dataportal/query.do?qn=dp_cst_query_billdetail&blno={0}&vslname={1}&voyage={2}", headList[3 * i + 2], headList[3 * i], headList[3 * i + 1]);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return strUrlTemp;
        }

        private LadingBillNumberWebEntity ParseAdmissionLadingDeclaration(string billNumber, string htmlContent)
        {
            string content = HtmlParseUtils.FormatHtml(htmlContent, false, true);
            if (string.IsNullOrEmpty(content))
            {
                return null;
            }
            List<string> headList = HtmlParseUtils.GetSubStrings(content, "<td align=\"left\">", null, "</td>", "<td align=\"left\">", null, "</td>");
            if (headList == null)
            {
                return null;
            }
            try
            {
                if (billNumber != headList[2])
                {
                    return null;
                }
                LadingBillNumberWebEntity ret = new LadingBillNumberWebEntity();
                ret.Conveyance = headList[0] == null ? "" : headList[0];
                ret.VoyageNumber = headList[1] == null ? "" : headList[1];
                ret.BillNumber = headList[2] == null ? "" : headList[2];
                ret.TotalWeight = headList[3] == null ? "" : headList[3];
                ret.TotalAmount = headList[4] == null ? "" : headList[4];

                //add detail
                List<string> detailList = HtmlParseUtils.GetSubStrings(content, "<tr height=\"21\">", null, "</tr>", null, null, null);
                if (detailList != null)
                {
                    ret.LadingDetail = new List<LadingBillNumberDetailWebEntity>();

                    foreach (string s in detailList)
                    {
                        List<string> list = HtmlParseUtils.GetSubStrings(s, "<td align=\"left\" bgcolor=\"#[A-Za-z0-9]+\">", null, "</td>", "<td align=\"left\" bgcolor=\"#EEEEEE\">", "<td align=\"left\" bgcolor=\"#E1E1E1\">", "</td>");
                        if (list != null)
                        {

                            try
                            {
                                LadingBillNumberDetailWebEntity detailEntity = new LadingBillNumberDetailWebEntity();

                                detailEntity.ContainerNumber = list[0] == null ? "" : list[0];
                                detailEntity.Weight = list[1] == null ? "" : list[1];
                                detailEntity.Amount = list[2] == null ? "" : list[2];
                                detailEntity.CustomsInfo = list[3] == null ? "" : list[3];
                                detailEntity.ReveivedDate = list[4].Length != 12 ? DateTime.MinValue : Constant.ConvertToDateTime(list[4]);
                                detailEntity.Owner = list[5] == null ? "" : list[5];
                                detailEntity.LadingCode = list[6] == null ? "" : list[6];
                                detailEntity.COSTRPNumber = list[7] == null ? "" : list[7];

                                ret.LadingDetail.Add(detailEntity);
                            }
                            catch { }
                            finally
                            {
                            }
                        }
                    }
                }
                
                return ret;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion
    }
}
