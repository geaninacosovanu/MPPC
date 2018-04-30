using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace MPPLab3.utils
{
    public class SQLiteConnectionFactory : ConnectionFactory
    {
        public override IDbConnection CreateConnection(IDictionary<string, string> props)
        {
            
            String connectionString = props["ConnectionString"];
            return new SQLiteConnection(connectionString);

        }
    }
}
