using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProcessorUtilities;
using CrazyCustomsEntity;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using CrazyCustomsEntity.WebEntity;

namespace SpiderMan
{
    public class CrawlDockEntry
    {
        //public CrazyCustomsProcessorEntities context = new CrazyCustomsProcessorEntities();
        
        public List<DockEntryWebEntity> CrawlByContainerNumber(string ContainerNumber)
        {
            string strUrl = string.Format("http://edi.easipass.com/dataportal/query.do?ctn_no={0}&qid=402803af0ecb1a4c010ecb1bcc1500e8&pagesize=30&submit=%E6%9F%A5%E8%AF%A2", ContainerNumber);
            string htmlContent = HtmlParseUtils.FormatHtml(HtmlParseUtils.SaveWebPage(strUrl, Encoding.UTF8), false, true);

            List<string> trList = HtmlParseUtils.GetSubStrings(htmlContent, "<tr height=\"21\">", null, "</tr>", null, null, null);

            List<DockEntryWebEntity> entryList = new List<DockEntryWebEntity>();

            foreach (string tr in trList)
            {
                List<string> tdList = HtmlParseUtils.GetSubStrings(tr, "<td align=\"left\" bgcolor=\"#(EEEEEE|E1E1E1)\">", null, "</td>", "<td align=\"left\" bgcolor=\"#EEEEEE\">", "<td align=\"left\" bgcolor=\"#E1E1E1\">", "</td>");
                string type = tdList[8];
                
                DockEntryWebEntity entry = new DockEntryWebEntity();
                entry.ContainerNumber = tdList[1];
                entry.OperationName = tdList[2];
                entry.OperationCode = tdList[3];
                entry.Conveyance = tdList[4];
                entry.VoyageNumber = tdList[5];

                entry.Dock = tdList[7];
                entry.Type = tdList[8];
                entry.Target = tdList[9];
                entry.Time = new DateTime(int.Parse(tdList[10].Substring(0, 4)), int.Parse(tdList[10].Substring(4, 2)), int.Parse(tdList[10].Substring(6, 2)), int.Parse(tdList[10].Substring(8, 2)), int.Parse(tdList[10].Substring(10, 2)), 0);
                entryList.Add(entry);
            }

            //context.SaveChanges();
            return entryList;
        }
    }
}
