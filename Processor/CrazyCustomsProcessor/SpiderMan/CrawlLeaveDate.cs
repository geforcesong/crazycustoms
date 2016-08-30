using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrazyCustomsEntity.WebEntity;
using System.Web;

namespace SpiderMan
{
    public class CrawlLeaveDate
    {

        #region Public
        #region 洋山

        public List<LeaveDateWebEntity> CrawlYang1()
        {
            List<string> lstConveyance = GetYangConvayance("http://www.shsict.com/as/query/search/com_user.jsp");
            if (lstConveyance != null && lstConveyance.Count > 0)
            {
                List<LeaveDateWebEntity> lstEntity = new List<LeaveDateWebEntity>();
                foreach(string conveyance in lstConveyance)
                {
                    string url = "http://www.shsict.com/as/query/search/comm/shipname.jsp?ename=" + conveyance;
                    //lstEntity.AddRange(CrawlYangData(url, "洋一"));
                    string shipInfo = ProcessorUtilities.HtmlParseUtils.SaveWebPage(url);
                    if (!shipInfo.Contains("没有查到任何纪录"))
                    {
                        List<string> trList = ProcessorUtilities.HtmlParseUtils.GetSubStrings(shipInfo, "<tr bgcolor=\"#E6EDFD\">", null, "</tr>", "<tr bgcolor=\"#E6EDFD\">", null, "</tr>");
                        if (trList != null && trList.Count > 0)
                        {

                            foreach (string tr in trList)
                            {
                                List<string> tdList = ProcessorUtilities.HtmlParseUtils.GetSubStrings(tr, "<td height=\"35\">", null, "</td>", "<td height=\"35\">", null, "</td>");

                                if (tdList != null && tdList.Count == 9)
                                {
                                    LeaveDateWebEntity entity = new LeaveDateWebEntity();

                                    entity.Dock = "洋一";
                                    entity.Conveyance = tdList[1].Trim();
                                    entity.VoyageNumber = tdList[2].Trim();
                                    entity.PlanArrivalDate = tdList[4].Trim();
                                    entity.ActualArrivalDate = tdList[5].Trim();
                                    entity.PlanLeavingDate = tdList[6].Trim();
                                    entity.ActualLeavingDate = tdList[7].Trim();
                                    entity.IsClosed = tdList[8].Trim();

                                    lstEntity.Add(entity);
                                }
                            }
                        }
                    }
                }
                return lstEntity;
            }

            return null;
        }

        public List<LeaveDateWebEntity> CrawlYang3()
        {
            List<string> lstConveyance = GetYangConvayance("http://www.sgict.com.cn:7001/query/search/comm/com_user.jsp");
            if (lstConveyance != null && lstConveyance.Count > 0)
            {
                List<LeaveDateWebEntity> lstEntity = new List<LeaveDateWebEntity>();
                foreach (string conveyance in lstConveyance)
                {
                    string url = "http://www.sgict.com.cn:7001/query/search/comm/shipname.jsp?ename=" + conveyance;
                    //lstEntity.AddRange(CrawlYangData(url, "洋三"));
                    string shipInfo = ProcessorUtilities.HtmlParseUtils.SaveWebPage(url);
                    if (!shipInfo.Contains("没有查到任何纪录"))
                    {
                        List<string> trList = ProcessorUtilities.HtmlParseUtils.GetSubStrings(shipInfo, "<tr class=\"result_tr[0-9]+\">", null, "</tr>", "<tr class=\"result_tr1\">", "<tr class=\"result_tr2\">", "</tr>");
                        if (trList != null && trList.Count > 0)
                        {
                            foreach (string tr in trList)
                            {
                                List<string> tdList = ProcessorUtilities.HtmlParseUtils.GetSubStrings(tr, "<td>", null, "</td>", "<td>", null, "</td>");

                                if (tdList != null && tdList.Count == 9)
                                {
                                    LeaveDateWebEntity entity = new LeaveDateWebEntity();

                                    entity.Dock = "洋三";
                                    entity.Conveyance = tdList[1].Trim();
                                    entity.VoyageNumber = tdList[2].Trim();
                                    entity.PlanArrivalDate = tdList[4].Trim();
                                    entity.ActualArrivalDate = tdList[5].Trim();
                                    entity.PlanLeavingDate = tdList[6].Trim();
                                    entity.ActualLeavingDate = tdList[7].Trim();
                                    entity.IsClosed = tdList[8].Trim();

                                    lstEntity.Add(entity);
                                }
                            }
                        }
                    }
                }
                return lstEntity;
            }

            return null;
        }

        #endregion

        #region 外港

        public List<LeaveDateWebEntity> CrawlWai1()
        {
            //第一次取所有船名
            
            string strUrl = "http://www.spict.com/portinfo/Normal/ShipPlanPage.aspx?";// + strParam;
            string content = ProcessorUtilities.HtmlParseUtils.SaveWebPage(strUrl, Encoding.UTF8);

            List<string> strConveyance = ProcessorUtilities.HtmlParseUtils.GetSubStrings(content, "dxo.itemsValue=\\[", null, "\\]", "dxo.itemsValue=[", null, "]");
            List<string> lstConveyance = strConveyance[2].Split(',').ToList();

            foreach (var conveyance in lstConveyance)
            {
                string strViewState = ProcessorUtilities.HtmlParseUtils.GetSubString(content, "id=\"__VIEWSTATE\" value=\"", null, "\"", "id=\"__VIEWSTATE\" value=\"", null, "\"");
                string strViewValidation = ProcessorUtilities.HtmlParseUtils.GetSubString(content, "id=\"__EVENTVALIDATION\" value=\"", null, "\"", "id=\"__EVENTVALIDATION\" value=\"", null, "\"");

                DateTime dtStart = DateTime.Today.AddDays(-14);
                DateTime dtEnd = DateTime.Today.AddDays(7);
                DateTime dt1 = new DateTime(1970, 1, 1);
                TimeSpan tsStart = new TimeSpan(dtStart.Ticks - dt1.Ticks);
                TimeSpan tsEnd = new TimeSpan(dtEnd.Ticks - dt1.Ticks);

                string strParam = string.Format("__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE={0}&__EVENTVALIDATION={1}&ctl00_ASPxSplitter2_CS=%5B%7B%22st%22%3A%22px%22%2C%22s%22%3A88%2C%22c%22%3A0%2C%22spt%22%3A0%2C%22spl%22%3A0%7D%2C%7B%22st%22%3A%22%25%22%2C%22s%22%3A100%2C%22i%22%3A%5B%7B%22st%22%3A%22px%22%2C%22s%22%3A218%2C%22c%22%3A0%2C%22spt%22%3A0%2C%22spl%22%3A0%7D%2C%7B%22st%22%3A%22%25%22%2C%22s%22%3A100%2C%22i%22%3A%5B%7B%22st%22%3A%22%25%22%2C%22s%22%3A100%2C%22c%22%3A0%2C%22spt%22%3A0%2C%22spl%22%3A0%7D%5D%2C%22c%22%3A0%7D%5D%2C%22c%22%3A0%7D%5D&ctl00_ASPxSplitter2_ASPxNavBar1GS=1%3B1&ctl00_ASPxSplitter2_ContentPlaceHolder1_dtStarting_Raw={2}&ctl00%24ASPxSplitter2%24ContentPlaceHolder1%24dtStarting=2013-2-28&ctl00_ASPxSplitter2_ContentPlaceHolder1_dtStarting_DDDWS=0%3A0%3A-1%3A-10000%3A-10000%3A0%3A-10000%3A-10000%3A1&ctl00_ASPxSplitter2_ContentPlaceHolder1_dtStarting_DDD_C_FNPWS=0%3A0%3A-1%3A-10000%3A-10000%3A0%3A0px%3A-10000%3A1&ctl00%24ASPxSplitter2%24ContentPlaceHolder1%24dtStarting%24DDD%24C=02%2F19%2F2013%3A02%2F19%2F2013&ctl00_ASPxSplitter2_ContentPlaceHolder1_dtExpiring_Raw={3}&ctl00%24ASPxSplitter2%24ContentPlaceHolder1%24dtExpiring=2013-3-5&ctl00_ASPxSplitter2_ContentPlaceHolder1_dtExpiring_DDDWS=0%3A0%3A-1%3A-10000%3A-10000%3A0%3A-10000%3A-10000%3A1&ctl00_ASPxSplitter2_ContentPlaceHolder1_dtExpiring_DDD_C_FNPWS=0%3A0%3A-1%3A-10000%3A-10000%3A0%3A0px%3A-10000%3A1&ctl00%24ASPxSplitter2%24ContentPlaceHolder1%24dtExpiring%24DDD%24C=03%2F05%2F2013%3A03%2F05%2F2013&ctl00_ASPxSplitter2_ContentPlaceHolder1_SearchPagesATI=1&ctl00_ASPxSplitter2_ContentPlaceHolder1_SearchPages_cbShipForm_VI=&ctl00%24ASPxSplitter2%24ContentPlaceHolder1%24SearchPages%24cbShipForm=&ctl00_ASPxSplitter2_ContentPlaceHolder1_SearchPages_cbShipForm_DDDWS=0%3A0%3A-1%3A-10000%3A-10000%3A0%3A-10000%3A-10000%3A1&ctl00_ASPxSplitter2_ContentPlaceHolder1_SearchPages_cbShipForm_DDD_LDeletedItems=&ctl00_ASPxSplitter2_ContentPlaceHolder1_SearchPages_cbShipForm_DDD_LInsertedItems=&ctl00_ASPxSplitter2_ContentPlaceHolder1_SearchPages_cbShipForm_DDD_LCustomCallback=&ctl00%24ASPxSplitter2%24ContentPlaceHolder1%24SearchPages%24cbShipForm%24DDD%24L=&ctl00%24ASPxSplitter2%24ContentPlaceHolder1%24SearchPages%24cbShipForm%24CVS=&ctl00_ASPxSplitter2_ContentPlaceHolder1_SearchPages_cbShipBerthingStatus_VI=&ctl00%24ASPxSplitter2%24ContentPlaceHolder1%24SearchPages%24cbShipBerthingStatus=&ctl00_ASPxSplitter2_ContentPlaceHolder1_SearchPages_cbShipBerthingStatus_DDDWS=0%3A0%3A-1%3A-10000%3A-10000%3A0%3A-10000%3A-10000%3A1&ctl00_ASPxSplitter2_ContentPlaceHolder1_SearchPages_cbShipBerthingStatus_DDD_LDeletedItems=&ctl00_ASPxSplitter2_ContentPlaceHolder1_SearchPages_cbShipBerthingStatus_DDD_LInsertedItems=&ctl00_ASPxSplitter2_ContentPlaceHolder1_SearchPages_cbShipBerthingStatus_DDD_LCustomCallback=&ctl00%24ASPxSplitter2%24ContentPlaceHolder1%24SearchPages%24cbShipBerthingStatus%24DDD%24L=&ctl00%24ASPxSplitter2%24ContentPlaceHolder1%24SearchPages%24cbShipBerthingStatus%24CVS=&ctl00_ASPxSplitter2_ContentPlaceHolder1_SearchPages_cbVessel_VI={4}&ctl00%24ASPxSplitter2%24ContentPlaceHolder1%24SearchPages%24cbVessel=AJCD007%2F%E5%AE%89%E5%90%89%E5%B7%9D%E8%BE%BE007%2F0134E%2FAJCD007%2F%E8%BF%9B%E5%8F%A3&ctl00_ASPxSplitter2_ContentPlaceHolder1_SearchPages_cbVessel_DDDWS=0%3A0%3A12000%3A276%3A199%3A0%3A-10000%3A-10000%3A1&ctl00_ASPxSplitter2_ContentPlaceHolder1_SearchPages_cbVessel_DDD_LDeletedItems=&ctl00_ASPxSplitter2_ContentPlaceHolder1_SearchPages_cbVessel_DDD_LInsertedItems=&ctl00_ASPxSplitter2_ContentPlaceHolder1_SearchPages_cbVessel_DDD_LCustomCallback=&ctl00%24ASPxSplitter2%24ContentPlaceHolder1%24SearchPages%24cbVessel%24DDD%24L=AJCD007&ctl00%24ASPxSplitter2%24ContentPlaceHolder1%24SearchPages%24cbVessel%24CVS=&ctl00%24ASPxSplitter2%24ContentPlaceHolder1%24SearchPages%24btnSearch2=&ctl00%24ASPxSplitter2%24ContentPlaceHolder1%24gridView%24DXSelInput=&ctl00%24ASPxSplitter2%24ContentPlaceHolder1%24gridView%24DXFocusedRowInput=-1&ctl00%24ASPxSplitter2%24ContentPlaceHolder1%24gridView%24CallbackState=%2FwEWBB4ERGF0YQXQBEFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFBQUFFQUFBQUFaV1pYTnpaV3dHNklpNTVaQ05Cd0FBQ0ZabGMzTmxiRVZPRE9pTHNlYVdoK2lJdWVXUWpRY0FBQWRKVm05NVlXZGxET2kvbStXUG8raUlxdWFzb1FjQUFBTkJWRUlNNWE2ZTZabUY2WjJnNXJPS0NBQUFBMFZVUWd6b3JxSGxpSkxwbmFEbXM0b0lBQUFEUlZSRURPaXVvZVdJa3VlbXUrYXppZ2dBQUFOU1ZFUU01YTZlNlptRjU2YTc1ck9LQ0FBQUIwVlVRMVJPU1U0TTZMK2I1NjZ4NWJ5QTVhZUxDQUFBQ0VWRlZFTlVUa2xPRE9pL20rZXVzZWFJcXVhdG9nZ0FBQkJKYm5SbGNtZHlZWFJwYjI1UWIzSjBET2lCbE9XS3FPZWdnZVdrdEFjQUFBaEpjME4xZEU5bVpnbmx0N0xtaUtybGhiTUhBQUFKU1ZadmVXRm5aVWxFRXVpL20rV1BvK2lJcXVhc29lZThsdVdQdHdjQUFBZEZWbTk1WVdkbERPV0h1dVdQbytpSXF1YXNvUWNBQUFsRlZtOTVZV2RsU1VRUzVZZTY1WStqNklpcTVxeWg1N3lXNVkrM0J3QUFDRlpsYzNObGJFTk9ET1M0cmVhV2graUl1ZVdRalFjQUFBdERiMngxYlc1SmJtUmxlQXhEYjJ4MWJXNGdTVzVrWlhnREFBQUNBQUFBQ1VsV2IzbGhaMlZKUkFsRlZtOTVZV2RsU1VRSEFBY0EeBVN0YXRlBZgDQnc4SEFBSUJCd0VDQVFjQ0FnRUhBd0lCQndVQ0FRY0hBZ0VIQ0FJQkJ3WUNBUWNKQWdFSENnSUJCd3NDQVFjTUFnRUhEUUlCQnc4Q0FBY1FBZ0FIRHdjQUJ3RUNBQUFBQUFBQVhrQUhBUWNCQWdBQUFBQUFBRjVBQndJSEFRSUFBQUFBQUFCZVFBY0RCd0VDQUFBQUFBQUFYa0FIQkFjQkFnQUFBQUFBQUY1QUJ3VUhBUUlBQUFBQUFBQmVRQWNHQndFQ0FBQUFBQUFBWGtBSEJ3Y0JBZ0FBQUFBQUFGNUFCd2dIQVFJQUFBQUFBQUJlUUFjSkJ3RUNBQUFBQUFBQVhrQUhDZ2NCQWdBQUFBQUFBRjVBQndzSEFRSUFBQUFBQUFCZVFBY01Cd0VDQUFBQUFBQUFYa0FIRFFjQkFnQUFBQUFBQUY1QUJ3NEhBUUlBQUFBQUFBQUFBQWNBQndBSEFBVUFBQUNBQ1FJTFEyOXNkVzF1U1c1a1pYZ0pBZ0FDQUFNSEFBSUJCd0FIQUFJQkJ3QUhBQT09&ctl00%24ASPxSplitter2%24ContentPlaceHolder1%24gridView%24DXColResizedInput=&ctl00%24ASPxSplitter2%24ContentPlaceHolder1%24gridView%24DXSyncInput=&ctl00_ASPxSplitter2_ContentPlaceHolder1_popupInfoWS=0%3A0%3A-1%3A-10000%3A-10000%3A0%3A500px%3A400px%3A1&DXScript=1_42%2C1_74%2C1_73%2C1_59%2C2_22%2C2_28%2C2_29%2C2_21%2C1_46%2C1_67%2C1_64%2C2_16%2C1_75%2C1_72%2C2_24%2C2_15%2C1_54%2C1_52%2C1_65%2C3_8%2C3_7%2C2_25%2C2_27", HttpUtility.UrlEncode(strViewState), HttpUtility.UrlEncode(strViewValidation), tsStart.TotalMilliseconds, tsEnd.TotalMilliseconds, conveyance.Replace("'", ""));
                content = ProcessorUtilities.HtmlParseUtils.PostWebRequest(strUrl, strParam, Encoding.UTF8);

                List<string> trList = ProcessorUtilities.HtmlParseUtils.GetSubStrings(content, "<tr id=\"ctl00_ASPxSplitter2_ContentPlaceHolder1_gridView_DXDataRow", null, "</tr>", null, null, null);
                if (trList != null)
                {
                    foreach (var tr in trList)
                    {
                        List<string> tdList = ProcessorUtilities.HtmlParseUtils.GetSubStrings(tr, "<td class=\"dxgv\" style=\"white-space:nowrap;\">", null, "</td>", "<td class=\"dxgv\" style=\"white-space:nowrap;\">", null, "</td>");

                        LeaveDateWebEntity entity = new LeaveDateWebEntity();
                        entity.Dock = "外一";
                        entity.Conveyance = tdList[0];
                        entity.VoyageNumber = tdList[0];
                        entity.ActualLeavingDate = tdList[0];
                        entity.PlanLeavingDate = tdList[0];
                        entity.ActualArrivalDate = tdList[0];
                        entity.PlanArrivalDate = tdList[0];
                        entity.IsClosed = tdList[0];
                    }
                }
            }

            ////strUrl = "http://www.spict.com/portinfo/Normal/ShipPlanPage.aspx?" + strParam;
            //content = ProcessorUtilities.HtmlParseUtils.SaveWebPage(strUrl, Encoding.UTF8);

            return null;
        }

        public List<LeaveDateWebEntity> CrawlWai2()
        {
            var lstConveyance = GetWaiConvayance("http://www.sipgzct.com/wat/controllerServlet.do?method=getsubformsinput&querysid=g0001");
            if (lstConveyance != null && lstConveyance.Count > 0)
            {
                List<LeaveDateWebEntity> lstEntity = new List<LeaveDateWebEntity>();
                foreach (var conveyance in lstConveyance)
                {
                    string strUrl = "http://www.sipgzct.com/wat/controllerServlet.do?querysid=g0001&queryid=0001004&method=doquery&shipname=" + conveyance;
                    lstEntity.AddRange(CrawlWaiData(strUrl, "外二"));
                }
                return lstEntity;
            }
            return null;
        }

        public List<LeaveDateWebEntity> CrawlWai4()
        {
            var lstConveyance = GetWaiConvayance("http://www.sect.com.cn/wat/controllerServlet.do?method=getsubformsinput&querysid=g0001");
            if (lstConveyance != null && lstConveyance.Count > 0)
            {
                List<LeaveDateWebEntity> lstEntity = new List<LeaveDateWebEntity>();
                foreach (var conveyance in lstConveyance)
                {
                    string strUrl = "http://www.sect.com.cn/wat/controllerServlet.do?querysid=g0001&queryid=0001004&method=doquery&shipname=" + conveyance;
                    lstEntity.AddRange(CrawlWaiData(strUrl, "外四"));
                }
                return lstEntity;
            }
            return null;
        }

        public List<LeaveDateWebEntity> CrawlWai5()
        {
            var lstConveyance = GetWaiConvayance("http://www.smct.com.cn/wat/controllerServlet.do?method=getsubformsinput&querysid=g0001");
            if (lstConveyance != null && lstConveyance.Count > 0)
            {
                List<LeaveDateWebEntity> lstEntity = new List<LeaveDateWebEntity>();
                foreach (var conveyance in lstConveyance)
                {
                    string strUrl = "http://www.smct.com.cn/wat/controllerServlet.do?querysid=g0001&queryid=0001004&method=doquery&shipname=" + conveyance;
                    lstEntity.AddRange(CrawlWaiData(strUrl, "外五"));
                }
                return lstEntity;
            }
            return null;
        }
        #endregion

        #region 上港

        public List<LeaveDateWebEntity> CrawlLuoJing()
        {
            string strUrl = "http://180.169.9.36:8080/jzx/model/input.do?queryId=1005";
            List<string> lstReturn = new List<string>();
            string content = ProcessorUtilities.HtmlParseUtils.SaveWebPage(strUrl);
            List<string> lstConveyance = ProcessorUtilities.HtmlParseUtils.GetSubStrings(content, "<option VALUE=\"", null, "\">", "<option VALUE=\"", null, "\">");

            if (lstConveyance != null && lstConveyance.Count > 0)
            {
                List<LeaveDateWebEntity> lstEntity = new List<LeaveDateWebEntity>();
                foreach (string conveyance in lstConveyance)
                {
                    string url = "http://180.169.9.36:8080/jzx/query/search.do?method=doPage&clearCondition=yes&flag=1&queryId=1005&temp=" + conveyance;
                    //lstEntity.AddRange(CrawlYangData(url, "洋三"));
                    string shipInfo = ProcessorUtilities.HtmlParseUtils.SaveWebPage(url);
                    if (!shipInfo.Contains("没有查到任何纪录"))
                    {
                        List<string> tableList = ProcessorUtilities.HtmlParseUtils.GetSubStrings(shipInfo, "<table align=\"center\"", null, "</table>", null, null, null);
                        if (tableList != null && tableList.Count == 1)
                        {
                            List<string> trList = ProcessorUtilities.HtmlParseUtils.GetSubStrings(shipInfo, "<tr>", null, "</tr>", "<tr>", null, "</tr>");
                            if (trList != null && trList.Count > 0)
                            {
                                foreach (string tr in trList)
                                {
                                    List<string> tdList = ProcessorUtilities.HtmlParseUtils.GetSubStrings(tr, "<td class=\"content\" nowrap=\"\">", null, "</td>", "<td class=\"content\" nowrap=\"\">", null, "</td>");

                                    if (tdList != null && tdList.Count == 8)
                                    {
                                        LeaveDateWebEntity entity = new LeaveDateWebEntity();

                                        entity.Dock = "上港罗泾";
                                        entity.Conveyance = tdList[1].Trim();
                                        entity.VoyageNumber = tdList[2].Trim();
                                        entity.PlanArrivalDate = tdList[4].Trim();
                                        entity.ActualArrivalDate = tdList[5].Trim();
                                        entity.PlanLeavingDate = tdList[6].Trim();
                                        entity.ActualLeavingDate = tdList[7].Trim();
                                        //entity.IsClosed = tdList[8].Trim();

                                        lstEntity.Add(entity);
                                    }
                                }
                            }
                        }
                    }
                }
                return lstEntity;
            }
            return null;

        }

        public List<LeaveDateWebEntity> CrawlZhangHuaBang()
        {
            var lstConveyance = GetWaiConvayance("http://218.1.104.251/zhbwat/controllerServlet.do?method=getsubformsinput&querysid=g0001");
            if (lstConveyance != null && lstConveyance.Count > 0)
            {
                List<LeaveDateWebEntity> lstEntity = new List<LeaveDateWebEntity>();
                foreach (var conveyance in lstConveyance)
                {
                    string strUrl = "http://218.1.104.251/zhbwat/controllerServlet.do?queryid=0001004&querysid=g0001&method=doquery&shipname=" + conveyance;
                    lstEntity.AddRange(CrawlWaiData(strUrl, "上港张华浜"));
                }
                return lstEntity;
            }
            return null;
        }

        #endregion

        #endregion

        #region Private
        private List<string> GetWaiConvayance(string Url)
        {
            List<string> lstReturn = new List<string>();
            string content = ProcessorUtilities.HtmlParseUtils.SaveWebPage(Url, Encoding.UTF8);
            List<string> lstConveyance = ProcessorUtilities.HtmlParseUtils.GetSubStrings(content, "<option value=\"[0-9a-zA-Z\\s.]+\">", null, "</option>", null, null, null);

            if (lstConveyance != null)
            {
                foreach (string conveyance in lstConveyance)
                {
                    string realConveyance = ProcessorUtilities.HtmlParseUtils.GetSubString(conveyance, ">", null, "/", ">", null, "/");
                    if (!string.IsNullOrEmpty(realConveyance) && !realConveyance.Contains("<"))
                    {
                        lstReturn.Add(realConveyance);
                    }
                }
            }

            return lstReturn;
        }

        private List<LeaveDateWebEntity> CrawlWaiData(string Url, string dock)
        {
            List<LeaveDateWebEntity> lstEntity = new List<LeaveDateWebEntity>();
            string content = ProcessorUtilities.HtmlParseUtils.SaveWebPage(Url, Encoding.UTF8);
            content = ProcessorUtilities.HtmlParseUtils.GetSubString(content, "<table id=\"ctable\" border=\"0\"", null, "</table>", null, null, null);
            List<string> trList = ProcessorUtilities.HtmlParseUtils.GetSubStrings(content, "<tr>", null, "</tr>", null, null, null);
            if (trList != null && trList.Count > 0)
            {
                foreach (var tr in trList)
                {
                    List<string> tdList = ProcessorUtilities.HtmlParseUtils.GetSubStrings(tr, "<td>", null, "</td>", "<td>", null, "</td>");
                    LeaveDateWebEntity entity = new LeaveDateWebEntity();

                    entity.Dock = dock;
                    entity.Conveyance = tdList[1].Trim();
                    entity.VoyageNumber = tdList[2].Trim();
                    entity.PlanArrivalDate = tdList[4].Trim();
                    entity.ActualArrivalDate = tdList[5].Trim();
                    entity.PlanLeavingDate = tdList[6].Trim();
                    entity.ActualLeavingDate = tdList[7].Trim();
                    entity.IsClosed = "";

                    lstEntity.Add(entity);
                }
            }
            return lstEntity;
        }

        private List<string> GetYangConvayance(string Url)
        {
            string strUrl = Url;
            string content = ProcessorUtilities.HtmlParseUtils.SaveWebPage(strUrl);
            List<string> lstConveyance = ProcessorUtilities.HtmlParseUtils.GetSubStrings(content, "<option>", null, "</option>", "<option>", null, "</option>");
            return lstConveyance;
        }
        #endregion
    }
}
