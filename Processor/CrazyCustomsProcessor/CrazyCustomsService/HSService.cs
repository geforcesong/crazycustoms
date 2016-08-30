using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrazyCustomsEntity.WebEntity;
using Microsoft.ApplicationBlocks.Data;
using ProcessorUtilities;
using System.Data;
using System.Data.SqlClient;

namespace CrazyCustomsService
{
    public class HSService
    {
        public HSCodeWebEntity GetHSCode(string Code)
        {
            SqlDataReader dataRead = SqlHelper.ExecuteReader(Constant.SqlConnectionString, CommandType.Text, string.Format("select * from [dbo].[HSCodeDictionary] where Code = '{0}'", Code));
            while (dataRead.Read())
            {
                HSCodeWebEntity entity = new HSCodeWebEntity();
                entity.Code = dataRead["Code"].ToString();
                entity.Name = dataRead["Name"].ToString();
                entity.ManagementCondition = dataRead["ManagementCondition"].ToString();
                entity.ExaminationCondition = dataRead["ExaminationCondition"].ToString();
                entity.DeclarationFactor = dataRead["DeclarationFactor"].ToString();
                entity.FirstUnitName = dataRead["FirstUnitName"].ToString();
                entity.SecondUnitName = dataRead["SecondUnitName"].ToString();
                entity.BestCountryRate = dataRead["BestCountryRate"].ToString();
                entity.ImportRate = dataRead["ImportRate"].ToString();
                entity.ExportRate = dataRead["ExportRate"].ToString();
                entity.ConsumeRate = dataRead["ConsumeRate"].ToString();
                entity.AddedValueTaxRate = dataRead["AddedValueTaxRate"].ToString();
                entity.DrawbackRate = dataRead["DrawbackRate"].ToString();
                entity.Note = dataRead["Note"].ToString();

                return entity;
            }

            return null;
        }
    }
}
