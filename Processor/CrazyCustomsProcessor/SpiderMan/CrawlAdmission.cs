using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProcessorUtilities;
using CrazyCustomsEntity.WebEntity;

namespace SpiderMan
{

    public class CrawlAdmission
    {
        public List<AdmissionWebEntity> CrawlAdmissionByBillNumber(string billNumber)
        {
            return RunParseAdmission(billNumber, "", "");
        }
        public List<AdmissionWebEntity> CrawlAdmissionByDeclarationNumber(string declarationNumber)
        {
            return RunParseAdmission("", declarationNumber, "");
        }
        public List<AdmissionWebEntity> CrawlAdmissionByContainerNumber(string containerNumber)
        {
            return RunParseAdmission("", "", containerNumber);
        }

        public static List<AdmissionWebEntity> RunParseAdmission(string billNumber, string declarationNumber, string containerNumber)
        {
            if (string.IsNullOrEmpty(billNumber) && string.IsNullOrEmpty(declarationNumber) && string.IsNullOrEmpty(containerNumber))
                return null;
            try
            {
                string _strRootSite = "http://edi.easipass.com/dataportal/q.do?qn=dp_query_letpas";
                string rootHtmlContent = HtmlParseUtils.SaveWebPage(_strRootSite, Encoding.UTF8);
                if (!string.IsNullOrEmpty(rootHtmlContent))
                {
                    string qid = HtmlParseUtils.GetSubString(rootHtmlContent, "<input name=\"qid\" value=\"", null, "\" type=\"hidden\" />", null, null, null);
                    if (!string.IsNullOrEmpty(qid))
                    {
                        string htmlContent = HtmlParseUtils.SaveWebPage(string.Format("http://edi.easipass.com/dataportal/query.do?entryid={0}&blno={1}&ctnno={2}&pagesize=100&submit=%E6%89%A7%E8%A1%8C&qid={3}",declarationNumber, billNumber, containerNumber, qid), Encoding.UTF8);
                        if (!string.IsNullOrEmpty(htmlContent))
                        {
                            return ParseAdmissionDeclarationContainer(htmlContent);
                        }
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        private static List<AdmissionWebEntity> ParseAdmissionDeclarationContainer(string htmlContent)
        {
            string content = HtmlParseUtils.FormatHtml(htmlContent, false, true);
            if (!string.IsNullOrEmpty(content))
            {
                List<string> trList = HtmlParseUtils.GetSubStrings(content, "<tr height=\"21\">", null, "</tr>", null, null, null);
                if (trList != null && trList.Count > 0)
                {
                    List<AdmissionWebEntity> lstAdmissionWebEntity = new List<AdmissionWebEntity>();
                    foreach (string s in trList)
                    {
                        List<string> tdList = HtmlParseUtils.GetSubStrings(s, "<td align=\"left\" bgcolor=\"#[A-Za-z0-9]+\">", null, "</td>", "<td align=\"left\" bgcolor=\"#EEEEEE\">", "<td align=\"left\" bgcolor=\"#E1E1E1\">", "</td>");
                        if (tdList != null && tdList.Count == 10)
                        {
                            try
                            {
                                AdmissionWebEntity entity = new AdmissionWebEntity();

                                entity.Conveyance = tdList[0];
                                entity.VoyageNumber = tdList[1];
                                entity.BillNumber = tdList[2];
                                entity.ContainerNumber = tdList[3];
                                entity.DeclarationNumber = tdList[4];
                                entity.Dock = tdList[5];
                                entity.AdmissionStatus = tdList[6];
                                entity.EDITime = ProcessorUtilities.Constant.ConvertToDateTime(tdList[7]);
                                entity.ReceivedStatus = tdList[8];
                                entity.SerialNumber = tdList[9];

                                lstAdmissionWebEntity.Add(entity);
                            }
                            catch (Exception ex)
                            {
                                return null;
                            }
                        }
                    }
                    
                    return lstAdmissionWebEntity;
                }
            }
            return null;
        }
    }
}
