using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib
{
    public class DataAccessBase
    {
        private const string connectionString = "CrazyCustomsWebConnection";

        public static Microsoft.Practices.EnterpriseLibrary.Data.Database CreateDatabaseInstance()
        {
            return Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase(connectionString);
        }
    }
}
