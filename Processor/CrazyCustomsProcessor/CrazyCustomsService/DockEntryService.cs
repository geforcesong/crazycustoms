using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrazyCustomsEntity.WebEntity;
using SpiderMan;

namespace CrazyCustomsService
{
    /// <summary>
    /// 进门信息
    /// </summary>
    public class DockEntryService
    {
        
        private List<DockEntryWebEntity> GetDockEntity(string ContainerNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ContainerNumber))
                    return null;
                CrawlDockEntry o = new CrawlDockEntry();
                return o.CrawlByContainerNumber(ContainerNumber.Trim());
            }
            catch
            {
                return null;
            }
        }

        public List<DockEntryWebEntity> GetDockEntity(string[] containerNumbers)
        {
            if (containerNumbers == null || containerNumbers.Length == 0)
                return null;
            List<DockEntryWebEntity> result = new List<DockEntryWebEntity>();
            foreach (var num in containerNumbers)
            {
                var search = GetDockEntity(num);
                if (search != null)
                    result.AddRange(search);
            }
            return result;
        }
    }
}
