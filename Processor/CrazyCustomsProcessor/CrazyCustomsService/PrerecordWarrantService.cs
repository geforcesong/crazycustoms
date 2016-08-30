using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrazyCustomsEntity.WebEntity;
using SpiderMan;

namespace CrazyCustomsService
{
    /// <summary>
    /// 预配仓单
    /// </summary>
    public class PrerecordWarrantService
    {
        private List<PrerecordWarrantWebEntity> GetPrerecordWarrant(string BillNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(BillNumber))
                    return null;
                CrawlPrerecordWarrant o = new CrawlPrerecordWarrant();
                return o.CrawlByBillNumber(BillNumber.Trim());
            }
            catch
            {
                return null;
            }
        }

        public List<PrerecordWarrantWebEntity> GetPrerecordWarrant(string[] billNumbers)
        {
            if (billNumbers == null || billNumbers.Length == 0)
                return null;
            List<PrerecordWarrantWebEntity> result = new List<PrerecordWarrantWebEntity>();
            foreach (var num in billNumbers)
            {
                var search = GetPrerecordWarrant(num);
                if (search != null)
                    result.AddRange(search);
            }
            return result;
        }
    }
}
