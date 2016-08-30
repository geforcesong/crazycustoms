using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrazyCustomsEntity.WebEntity
{
    /// <summary>
    /// 预配仓单
    /// http://edi.easipass.com/dataportal/q.do?qn=dp_premanifest_entry
    /// </summary>
    public class PrerecordWarrantWebEntity
    {
        public string BillNumber;
        public string Conveyance;
        public string VoyageNumber;
        public string PrerecordWarrantStatus;
    }
}
