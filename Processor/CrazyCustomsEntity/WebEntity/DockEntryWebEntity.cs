using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrazyCustomsEntity.WebEntity
{
    /// <summary>
    /// 进出门信息
    /// http://edi.easipass.com/dataportal/q.do?qn=dp_codecoinfo_query
    /// </summary>
    public class DockEntryWebEntity
    {
        //CrazyCustomsProcessorEntities con = new CrazyCustomsProcessorEntities();
        public string ContainerNumber; //箱号
        public string OperationName; //箱经营人名称
        public string OperationCode; //	箱经营人代码
        public string Conveyance; //船名
        public string VoyageNumber; //航次
        public string Type;//进/出口
        public string Dock;//进出门场站
        public string Target;//进出门目的
        public DateTime Time;//进出门时间
        public string TimeString
        {
            get { return this.Time.ToString(); }
        }
    }
}
