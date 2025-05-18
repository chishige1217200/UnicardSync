using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicardSync
{
    public class DatabaseConfig
    {
        public static string DBFileName { get; private set; } = "database.db";
        public static string DBPath { get; private set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DBFileName);

        public static SqliteConnection GetConnection()
        {
            string connectionString = $"Data Source={DBPath}";
            return new SqliteConnection(connectionString);
        }
    }
}
