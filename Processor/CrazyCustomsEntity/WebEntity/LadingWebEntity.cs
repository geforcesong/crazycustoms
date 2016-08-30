using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrazyCustomsEntity.WebEntity
{
    /// <summary>
    /// 进港信息
    /// http://edi.easipass.com/dataportal/q.do?qn=dp_cst_vsl
    /// </summary>
    public class LadingBillNumberWebEntity
    {
        public string Conveyance;
        public string VoyageNumber;
        public string Dock;
        public string BillNumber;
        public string TotalWeight;
        public string TotalAmount;

        public List<LadingBillNumberDetailWebEntity> LadingDetail;
    }
    public class LadingBillNumberDetailWebEntity
    {
        public string ContainerNumber;
        public string Weight;
        public string Amount;
        public string CustomsInfo;
        public DateTime ReveivedDate;
        public string Owner;
        public string LadingCode;
        public string COSTRPNumber;
    }

    public class LadingContainerNumberWebEntity
    {
        public string Conveyance;
        public string VoyageNumber;
        public string Dock;
        public string ContainerNumber;
        public DateTime LadingDate;
        public string Owner;
        public string COSTRPSentNumber;
        public DateTime COSTRPSentDate;

        public List<LadingContainerNumberDetailWebEntity> LadingDetail;
    }

    public class LadingContainerNumberDetailWebEntity
    {
        public string BillNumber;
        public string Weight;
        public string Amount;
        public string CustomsInfo;
        public DateTime ReveivedDate;
        public string LadingCode;
        public string COSTRPNumber;
    }
}
