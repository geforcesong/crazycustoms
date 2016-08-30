using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProcessorUtilities;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using CrazyCustomsEntity.WebEntity;

namespace SpiderMan
{
    class CrawlHSCode
    {
        private HSCodeWebEntity GetHSCodeByCode(string Code)
        {
            if (!string.IsNullOrEmpty(Code))
            {
                DataSet ds = SqlHelper.ExecuteDataset(Constant.SqlConnectionString, CommandType.Text, string.Format("select * from HSCodeDictionary where code = '{0}'", Code));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    HSCodeWebEntity entity = new HSCodeWebEntity();
                    entity.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                    entity.ManagementCondition = ds.Tables[0].Rows[0]["ManagementCondition"].ToString();
                    entity.ExaminationCondition = ds.Tables[0].Rows[0]["ExaminationCondition"].ToString();
                    entity.DeclarationFactor = ds.Tables[0].Rows[0]["DeclarationFactor"].ToString();
                    entity.FirstUnitName = ds.Tables[0].Rows[0]["FirstUnitName"].ToString();
                    entity.SecondUnitName = ds.Tables[0].Rows[0]["SecondUnitName"].ToString();
                    entity.BestCountryRate = ds.Tables[0].Rows[0]["BestCountryRate"].ToString();
                    entity.ImportRate = ds.Tables[0].Rows[0]["ImportRate"].ToString();
                    entity.ExportRate = ds.Tables[0].Rows[0]["ExportRate"].ToString();
                    entity.ConsumeRate = ds.Tables[0].Rows[0]["ConsumeRate"].ToString();
                    entity.AddedValueTaxRate = ds.Tables[0].Rows[0]["AddedValueTaxRate"].ToString();
                    entity.DrawbackRate = ds.Tables[0].Rows[0]["DrawbackRate"].ToString();
                    entity.Note = ds.Tables[0].Rows[0]["Note"].ToString();

                    return entity;
                }
            }
            return null;
        }
    }
}
