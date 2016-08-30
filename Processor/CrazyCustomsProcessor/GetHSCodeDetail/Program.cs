using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ProcessorUtilities;
using System.Net;
using System.IO;
using System.Net.Cache;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using CrazyCustomsEntity;
using SpiderMan;

namespace CrazyCustomsProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            //CrawlDockEntry.CrawlByContainerNumber("SHFU2237965");
            //ParseRootPage();
            //CrawlPrerecordWarrant o = new CrawlPrerecordWarrant();
            //o.CrawlByBillNumber("COSU6046394664");

            //CrawlLading o = new CrawlLading();
            //o.CrawlLadingByContainerNumber("KKFU1623889");

            CrawlLeaveDate o = new CrawlLeaveDate();
            o.CrawlWai1();

            //CrawlAdmission o = new CrawlAdmission();
            //var result = o.CrawlAdmissionByContainerNumber("KKFU1623889");

        }

//        private static void ParseRootPage()
//        {
//            for (int i = 1; i < 5000; i++)
//            {
//                string totalPage = string.Empty;
//                string totalCount = string.Empty;
//                string url = string.Format("http://www.hscode.net/IntegrateQueries/YsInfoPager?pageIndex={0}&taxCode=0&productName=", i);
//                string htmlContent = HtmlParseUtils.FormatHtml(HtmlParseUtils.SaveWebPage(url), false, true);

//                List<string> rowList = HtmlParseUtils.GetSubStrings(htmlContent, "<div class=\"scx_item\">", null, "</table></div>", null, null, null);
//                if (rowList != null)
//                {
//                    HSCode hsCode = new HSCode();
//                    foreach (string row in rowList)
//                    {
//                        List<string> itemList1 = HtmlParseUtils.GetSubStrings(row, "<table", null, "</table>", null, null, null);
//                        if (itemList1 != null)
//                        {
//                            foreach (var item in itemList1)
//                            {
//                                if (item.Contains("商品描述"))
//                                {
//                                    hsCode.Name = HtmlParseUtils.GetSubString(item, "<span style=\"line-height: 20px;\">", null, "</span>", "<span style=\"line-height: 20px;\">", null, "</span>").Replace("\r\n", "").Trim();
//                                }
//                                if (item.Contains("申报要素"))
//                                {
//                                    hsCode.DeclarationFactor = HtmlParseUtils.GetSubString(item, "<span style=\"line-height: 20px;\">", null, "</span>", "<span style=\"line-height: 20px;\">", null, "</span>").Replace("\r\n", "").Trim();
//                                }
//                                if (item.Contains("备注"))
//                                {
//                                    hsCode.Note = HtmlParseUtils.GetSubString(item, "<span style=\"line-height: 20px;\">", null, "</span>", "<span style=\"line-height: 20px;\">", null, "</span>").Replace("\r\n", "").Trim();
//                                }
//                            }
//                        }

//                        hsCode.Code = HtmlParseUtils.GetSubString(row, "<div class=\"even red\">", null, "</div>", "<div class=\"even red\">", null, "</div>").Replace("\r\n", "").Trim();

//                        List<string> itemList = HtmlParseUtils.GetSubStrings(row, "<div class=\"even1\">", "<div class=\"even red\">", "</div>", "<div class=\"even1\">", "<div class=\"even red\">", "</div>");
//                        if (itemList != null)
//                        {
//                            //hsCode.Code = itemList[0].Replace("\r\n", "").Trim();
//                            hsCode.FirstUnitName = itemList[0].Replace("\r\n", "").Trim();
//                            hsCode.SecondUnitName = itemList[1].Replace("\r\n", "").Trim();
//                            hsCode.BestCountryRate = itemList[2].Replace("\r\n", "").Trim();
//                            hsCode.ImportRate = itemList[3].Replace("\r\n", "").Trim();
//                            hsCode.ExportRate = itemList[4].Replace("\r\n", "").Trim();
//                            hsCode.ConsumeRate = itemList[5].Replace("\r\n", "").Trim();
//                            hsCode.AddedValueTaxRate = itemList[6].Replace("\r\n", "").Trim();
//                            hsCode.ManagementCondition = HtmlParseUtils.GetSubString(itemList[7], "style=\"color: black\">", null, "</label>", "style=\"color: black\">", null, "</label>").Replace("\r\n", "").Trim();
//                            hsCode.ExaminationCondition = HtmlParseUtils.GetSubString(itemList[8], "style=\"color: black\">", null, "</label>", "style=\"color: black\">", null, "</label>").Replace("\r\n", "").Trim();
//                        }

//                        //Save
//                        SqlHelper.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["SqlServer"].ToString(), 
//                                                    CommandType.Text, 
//                                                    string.Format(@"INSERT INTO [dbo].[HSCodeDictionary]
//                                                                    ([Code]
//                                                                    ,[Name]
//                                                                    ,[ManagementCondition]
//                                                                    ,[ExaminationCondition]
//                                                                    ,[DeclarationFactor]
//                                                                    ,[FirstUnitName]
//                                                                    ,[SecondUnitName]
//                                                                    ,[BestCountryRate]
//                                                                    ,[ImportRate]
//                                                                    ,[ExportRate]
//                                                                    ,[ConsumeRate]
//                                                                    ,[AddedValueTaxRate]
//                                                                    ,[DrawbackRate]
//                                                                    ,[Note])
//                                                                VALUES
//                                                                    ('{0}'
//                                                                    ,'{1}'
//                                                                    ,'{2}'
//                                                                    ,'{3}'
//                                                                    ,'{4}'
//                                                                    ,'{5}'
//                                                                    ,'{6}'
//                                                                    ,'{7}'
//                                                                    ,'{8}'
//                                                                    ,'{9}'
//                                                                    ,'{10}'
//                                                                    ,'{11}'
//                                                                    ,'{12}'
//                                                                    ,'{13}')", hsCode.Code, hsCode.Name.Replace("'", "''"), hsCode.ManagementCondition.Replace("'", "''"), hsCode.ExaminationCondition, hsCode.DeclarationFactor.Replace("'", "''"), hsCode.FirstUnitName, hsCode.SecondUnitName, hsCode.BestCountryRate,
//                                                                            hsCode.ImportRate, hsCode.ExportRate, hsCode.ConsumeRate, hsCode.AddedValueTaxRate, hsCode.DrawbackRate, hsCode.Note.Replace("'", "''")));
//                    }
//                }
//                else
//                {
//                    break;
//                }
//            }
//        }
        
    }
}
