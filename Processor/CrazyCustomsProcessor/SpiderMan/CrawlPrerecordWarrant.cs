using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProcessorUtilities;
using CrazyCustomsEntity.WebEntity;

namespace SpiderMan
{
    public class CrawlPrerecordWarrant
    {
        List<PrerecordWarrantWebEntity> lstEntity = new List<PrerecordWarrantWebEntity>();
        public List<PrerecordWarrantWebEntity> CrawlByBillNumber(string billNumber, string conveyance = "", string voyageNumber = "")
        {
            if (!string.IsNullOrEmpty(billNumber))
            {
                if (!string.IsNullOrEmpty(conveyance) && !string.IsNullOrEmpty(voyageNumber))
                {
                    CrawlData(billNumber, conveyance, voyageNumber);
                }
                else
                {
                    //第一次得到convayance和voyagenumber
                    string firstUrl = string.Format("http://edi.easipass.com/dataportal/query.do?qn=dp_premanifest_bill_list&blno={0}&vslname=&voyage=", billNumber);
                    string htmlContent = HtmlParseUtils.SaveWebPage(firstUrl, Encoding.UTF8);
                    if (!string.IsNullOrEmpty(htmlContent))
                    {
                        List<string> trList = HtmlParseUtils.GetSubStrings(htmlContent, "<tr height=\"21\">", null, "</tr>", "<tr height=\"21\">", null, "</tr>");
                        if (trList != null )
                        {
                            foreach (var tr in trList)
                            {
                                List<string> tdList = HtmlParseUtils.GetSubStrings(tr, "<td align=\"left\" bgcolor=\"#EEEEEE\">", null, "</td>", "<td align=\"left\" bgcolor=\"#EEEEEE\">", null, "</td>");
                                conveyance = tdList[0];
                                voyageNumber = tdList[1];
                                CrawlData(billNumber, conveyance, voyageNumber);
                            }
                        }
                    }
                }
            }

            return lstEntity;
        }

        private void CrawlData(string billNumber, string conveyance, string voyageNumber)
        {
            string htmlContent = HtmlParseUtils.SaveWebPage(string.Format("http://edi.easipass.com/dataportal/query.do?qn=dp_premanifest_ack_detail&blno={2}&vslname={0}&voyage={1}", conveyance, voyageNumber, billNumber), Encoding.UTF8);
            if (!string.IsNullOrEmpty(htmlContent))
            {
                string content = HtmlParseUtils.FormatHtml(htmlContent, false, true);
                if (!string.IsNullOrEmpty(content))
                {
                    List<string> headList = HtmlParseUtils.GetSubStrings(content, "<td width=\"40%\">", null, "</td>", "<td width=\"40%\">", null, "</td>");
                    if (headList != null)
                    {
                        try
                        {
                            if (headList.Count > 0)
                            {
                                PrerecordWarrantWebEntity entity = new PrerecordWarrantWebEntity();
                                entity.BillNumber = billNumber;
                                entity.Conveyance = conveyance;
                                entity.VoyageNumber = voyageNumber;
                                entity.PrerecordWarrantStatus = headList[0].Trim();

                                lstEntity.Add(entity);
                            }
                        }
                        catch (Exception ex)
                        {
                            ex.HelpLink = "BillNumber:\"" + billNumber + "\"";
                        }
                    }
                }
            }
        }
    }
}
