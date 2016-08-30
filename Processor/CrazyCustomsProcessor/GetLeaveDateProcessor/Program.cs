using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using CrazyCustomsEntity.WebEntity;
using ProcessorUtilities;
using System.Data;

namespace GetLeaveDateProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            SpiderMan.CrawlLeaveDate o = new SpiderMan.CrawlLeaveDate();
            List<LeaveDateWebEntity> lstEntity = o.CrawlWai2();
            foreach (var entity in lstEntity)
            {
                SqlHelper.ExecuteNonQuery(Constant.SqlConnectionString,CommandType.Text, string.Format(@"INSERT INTO [CrazyCustomsProcessor].[dbo].[LeaveDate] ([Conveyance] ,[VoyageNumber] ,[PlanArrivalDate] ,[ActualArrivalDate] ,[PlanLeavingDate] ,[ActualLeavingDate] ,[Dock] ,[IsClosed]) VALUES ('{0}' ,'{1}' ,'{2}' ,'{3}' ,'{4}' ,'{5}' ,'{6}' ,'{7}')",
                                                                                                                    entity.Conveyance.Replace("'", "''"), entity.VoyageNumber.Replace("'", "''"), entity.PlanArrivalDate.Replace("'", "''"), entity.ActualArrivalDate.Replace("'", "''"), entity.PlanLeavingDate.Replace("'", "''"), entity.ActualLeavingDate.Replace("'", "''"), entity.Dock.Replace("'", "''"), entity.IsClosed.Replace("'", "''")));

            }

        }
    }
}
