using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrazyCustomsEntity.WebEntity;
using ProcessorUtilities;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Data;

namespace CrazyCustomsService
{
    /// <summary>
    /// 离港信息
    /// </summary>
    public class LeaveDateService
    {
        public List<LeaveDateWebEntity> GetLeaveDate(Docks Dock, string Conveyance)
        {
            List<LeaveDateWebEntity> lstRet = new List<LeaveDateWebEntity>();
            string strDock = "";
            switch(Dock)
            {
                case Docks.Yang1:
                    strDock = "洋一";
                    break;
                case Docks.Yang3:
                    strDock = "洋三";
                    break;
                case Docks.Wai1:
                    strDock = "外一";
                    break;
                case Docks.Wai2:
                    strDock = "外二";
                    break;
                case Docks.Wai4:
                    strDock = "外四";
                    break;
                case Docks.Wai5:
                    strDock = "外五";
                    break;
                case Docks.LuoJing:
                    strDock = "上港罗泾";
                    break;
                case Docks.ZhangHuaBang:
                    strDock = "上港张华浜";
                    break;
            }

            return GetLeaveDate(Conveyance, strDock);
        }

        public List<LeaveDateWebEntity> GetLeaveDate(string strDock, string Conveyance)
        {
            List<LeaveDateWebEntity> lstRet = new List<LeaveDateWebEntity>();

            if (!string.IsNullOrEmpty(strDock))
            {
                using (SqlDataReader reader = SqlHelper.ExecuteReader(Constant.SqlConnectionString, CommandType.Text, string.Format("select * from LeaveDate where dock = '{0}' and conveyance = '{1}'", strDock, Conveyance)))
                {
                    while (reader.Read())
                    {
                        lstRet.Add(ConvertLeaveDate(reader));
                    }
                }
            }
            else
            {
                using (SqlDataReader reader = SqlHelper.ExecuteReader(Constant.SqlConnectionString, CommandType.Text, string.Format("select * from LeaveDate where conveyance = '{1}'", Conveyance)))
                {
                    while (reader.Read())
                    {
                        lstRet.Add(ConvertLeaveDate(reader));
                    }
                }
            }

            return lstRet;
        }


        private LeaveDateWebEntity ConvertLeaveDate(SqlDataReader reader)
        {
            LeaveDateWebEntity entity = new LeaveDateWebEntity();

            entity.Dock = reader["Dock"].ToString();
            entity.Conveyance = reader["Conveyance"].ToString();
            entity.VoyageNumber = reader["VoyageNumber"].ToString();
            entity.PlanLeavingDate = reader["PlanLeavingDate"].ToString();
            entity.ActualLeavingDate = reader["ActualLeavingDate"].ToString();
            entity.PlanArrivalDate = reader["PlanArrivalDate"].ToString();
            entity.ActualArrivalDate = reader["ActualArrivalDate"].ToString();
            entity.IsClosed = reader["IsClosed"].ToString();

            return entity;
        }
    }
}
