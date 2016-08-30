using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrazyCustomsEntity.WebEntity;
using SpiderMan;

namespace CrazyCustomsService
{
    /// <summary>
    /// 进港信息
    /// </summary>
    public class LadingService
    {
        public LadingServiceWebData GetAdmissionByContainerNumber(string ContainerNumber, string Conveyance, string VoyageNumber)
        {
            try
            {
                LadingServiceWebData ret = new LadingServiceWebData();
                CrawlLading o = new CrawlLading();
                ret.LadingContainerNumberWebEntity = o.CrawlLadingByContainerNumber(ContainerNumber, Conveyance, VoyageNumber);
                return ret;
            }
            catch
            {
                return null;
            }
        }

        public LadingServiceWebData GetAdmissionByContainerNumber(string ContainerNumber)
        {
            try
            {
                LadingServiceWebData ret = new LadingServiceWebData();
                CrawlLading o = new CrawlLading();
                ret.LadingContainerNumberWebEntity = o.CrawlLadingByContainerNumber(ContainerNumber);
                return ret;
            }
            catch
            {
                return null;
            }
        }

        public LadingServiceWebData GetAdmissionByBillNumber(string BillNumber, string Conveyance, string VoyageNumber)
        {
            try
            {
                LadingServiceWebData ret = new LadingServiceWebData();
                CrawlLading o = new CrawlLading();
                ret.LadingBillNumberWebEntity = o.CrawlLadingByBillNumber(BillNumber, Conveyance, VoyageNumber);
                return ret;
            }
            catch
            {
                return null;
            }
        }

        public LadingServiceWebData GetAdmissionByBillNumber(string BillNumber)
        {
            try
            {
                LadingServiceWebData ret = new LadingServiceWebData();
                CrawlLading o = new CrawlLading();
                ret.LadingBillNumberWebEntity = o.CrawlLadingByBillNumber(BillNumber);
                return ret;
            }
            catch
            {
                return null;
            }
        }
    }

    public class LadingServiceWebData
    {
        public LadingContainerNumberWebEntity LadingContainerNumberWebEntity { get; set; }
        public LadingBillNumberWebEntity LadingBillNumberWebEntity { get; set; }
    }
}
