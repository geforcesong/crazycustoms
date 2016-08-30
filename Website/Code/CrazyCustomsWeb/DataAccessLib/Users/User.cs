using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Users
{
    public class User
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CellPhone { get; set; }
        public string UserRole { get; set; }
        public string UserName { get; set; }

        public bool IsValid
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password))
                    return true;
                return false;
            }
        }

        public static User Logon(string email, string password)
        {
            string sql = @"SELECT [UserID]
                                  ,R.[RoleName]
                                  ,[Email]
                                  ,[Password]
                                  ,[CellPhone]
                                  ,[CreatedTime]
                              FROM [User] U
                              INNER JOIN [userrole] R ON u.[RoleID] =R.[RoleID]
                              WHERE U.Email=@email AND u.[Password]=@password";

            var db = DataAccessBase.CreateDatabaseInstance();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "email", DbType.String, email);
            db.AddInParameter(cmd, "password", DbType.String, password);

            using (IDataReader idr = db.ExecuteReader(cmd))
            {
                if (idr.Read())
                {
                    User u = new User();
                    u.UserID = Convert.ToInt32(idr["UserID"]);
                    u.Email = idr["Email"] == DBNull.Value ? "" : idr["Email"].ToString();
                    u.CellPhone = idr["CellPhone"] == DBNull.Value ? "" : idr["CellPhone"].ToString();
                    u.UserRole = idr["RoleName"] == DBNull.Value ? "" : idr["RoleName"].ToString();
                    return u;
                }
            }
            return null;
        }

        public static void Register(string email, string password, string cellphone)
        {
            var db = DataAccessBase.CreateDatabaseInstance();
            string sql = @"INSERT INTO [User] (RoleID, Email, [Password], CellPhone, CreatedTime) 
                        VALUES(1, @email, @password, @cellphone, @createTime)";
            DateTime createTime = DateTime.Now;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "email", DbType.String, email);
            db.AddInParameter(cmd, "password", DbType.String, password);
            db.AddInParameter(cmd, "cellphone", DbType.String, cellphone);
            db.AddInParameter(cmd, "createTime", DbType.DateTime, createTime);
            db.ExecuteNonQuery(cmd);
        }
    }
}
