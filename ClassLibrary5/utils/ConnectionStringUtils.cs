using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace MPPLab3.utils
{
    public class ConnectionStringUtils
    {
        public  static  string GetConnectionStringByName(string name) {
            string value = null;
          
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
            if (settings != null)
                value = settings.ConnectionString;

            return value;
        }
    }
}
