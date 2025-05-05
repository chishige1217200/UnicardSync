using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnicardSync
{
    internal static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // SQLiteデータベースの初期化
            string dbFileName = "database.db";
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dbFileName);

            string connectionString = $"Data Source={dbPath}";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                CreateTables(connection);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        /**
         * 初期テーブルを作成するメソッド
         * @param connection SQLite接続オブジェクト
         */
        private static void CreateTables(SqliteConnection connection)
        {
            string createTableSql = @"
            CREATE TABLE IF NOT EXISTS torikomi (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                file_name TEXT NOT NULL,
                torikomi_type TEXT NOT NULL,
                ins_datetime TEXT NOT NULL default CURRENT_TIMESTAMP, -- 'YYYY-MM-DD HH:MM:SS'形式で保存
                upd_datetime TEXT NOT NULL default CURRENT_TIMESTAMP, -- 'YYYY-MM-DD HH:MM:SS'形式で保存
                rec_ver INTEGER NOT NULL default 1
            );
            CREATE TABLE IF NOT EXISTS used (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                place_used TEXT,
                amount_used INTEGER NOT NULL default 0,
                date_used TEXT NOT NULL, -- 'YYYY-MM-DD'形式で保存
                note TEXT,
                torikomi_id INTEGER,
                ins_datetime TEXT NOT NULL default CURRENT_TIMESTAMP, -- 'YYYY-MM-DD HH:MM:SS'形式で保存
                upd_datetime TEXT NOT NULL default CURRENT_TIMESTAMP, -- 'YYYY-MM-DD HH:MM:SS'形式で保存
                rec_ver INTEGER NOT NULL default 1,
                FOREIGN KEY (torikomi_id) REFERENCES torikomi(id)
            );
        ";

            using (var command = connection.CreateCommand())
            {
                command.CommandText = createTableSql;
                command.ExecuteNonQuery();
            }
        }
    }
}
