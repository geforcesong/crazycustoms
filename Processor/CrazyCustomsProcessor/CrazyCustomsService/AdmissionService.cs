using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrazyCustomsEntity.WebEntity;
using ProcessorUtilities;
using SpiderMan;

namespace CrazyCustomsService
{
    /// <summary>
    /// 放行信息
    /// </summary>
    public class AdmissionService
    {
        private List<AdmissionWebEntity> GetAdmissionByBillNumber(string BillNumber)
        {
            CrawlAdmission o = new CrawlAdmission();
            return o.CrawlAdmissionByBillNumber(BillNumber);
        }

        private List<AdmissionWebEntity> GetAdmissionByDeclarationNumber(string DeclarationNumber)
        {
            CrawlAdmission o = new CrawlAdmission();
            return o.CrawlAdmissionByDeclarationNumber(DeclarationNumber);
        }

        private List<AdmissionWebEntity> GetAdmissionByContainerNumber(string ContainerNumber)
        {
            CrawlAdmission o = new CrawlAdmission();
            return o.CrawlAdmissionByContainerNumber(ContainerNumber);
        }

        public List<AdmissionWebEntity> GetAdmission(string inputNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(inputNumber))
                    return null;
                //根据不同的输入调用不同的函数
                switch (Constant.GetQueryType(inputNumber))
                {
                    case QueryTypes.ContainerNumber:
                        return GetAdmissionByContainerNumber(inputNumber);
                    case QueryTypes.BillNumber:
                        return GetAdmissionByBillNumber(inputNumber);
                    case QueryTypes.DeclarationNumber:
                        return GetAdmissionByDeclarationNumber(inputNumber);
                    default:
                        return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<AdmissionWebEntity> GetAdmission(string[] inputNumbers)
        {
            if (inputNumbers == null || inputNumbers.Length == 0)
                return null;
            List<AdmissionWebEntity> result = new List<AdmissionWebEntity>();
            foreach (var num in inputNumbers)
            {
                var search = GetAdmission(num);
                if (search != null)
                    result.AddRange(search);
            }
            return result;
        }

    }
}
