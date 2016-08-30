using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrazyCustomsEntity.WebEntity
{
    /// <summary>
    /// 放行信息
    /// 
    /// http://edi.easipass.com/dataportal/q.do?qn=dp_query_letpas
    /// </summary>
    public class AdmissionWebEntity
    {
        public string Conveyance;
        public string VoyageNumber;
        public string BillNumber;
        public string ContainerNumber;
        public string DeclarationNumber;
        public string Dock;
        public string AdmissionStatus;
        public DateTime EDITime;
        public string ReceivedStatus;
        public string SerialNumber;
    }
}
