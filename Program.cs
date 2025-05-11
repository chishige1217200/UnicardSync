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
            bool isNewDatabase = !File.Exists(dbPath);

            string connectionString = $"Data Source={dbPath}";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                if (isNewDatabase)
                {
                    Console.WriteLine("データベースが存在しないため、新規作成します...");
                    CreateTables(connection);
                    InsertSampleRecords(connection);
                }
                else
                {
                    Console.WriteLine("既存のデータベースを使用します。");
                }
            }

            TorikomiConfigHelper.LoadConfig();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TableForm());
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

            Console.WriteLine("テーブルを作成しました。");
        }

        /**
         * サンプルデータを登録するメソッド
         * @param connection SQLite接続オブジェクト
         */
        private static void InsertSampleRecords(SqliteConnection connection)
        {
            var insertCmd = connection.CreateCommand();
            insertCmd.CommandText = @"
                INSERT INTO torikomi(file_name, torikomi_type)
                VALUES ($fileName, $torikomiType);
                SELECT last_insert_rowid();
            ";
            insertCmd.Parameters.AddWithValue("$fileName", "test.csv");
            insertCmd.Parameters.AddWithValue("$torikomiType", "テストカード");
            long torikomiID = (long)insertCmd.ExecuteScalar();

            var insertCmd2 = connection.CreateCommand();
            insertCmd2.CommandText = @"
                INSERT INTO used(place_used, amount_used, date_used, note, torikomi_id)
                VALUES($placeUsed, $amountUsed, $dateUsed, $note, $torikomiID);
            ";
            insertCmd2.Parameters.AddWithValue("$placeUsed", "テストストア");
            insertCmd2.Parameters.AddWithValue("$amountUsed", 10000);
            insertCmd2.Parameters.AddWithValue("$dateUsed", "2025/05/05");
            insertCmd2.Parameters.AddWithValue("$note", "備考");
            insertCmd2.Parameters.AddWithValue("$torikomiID", torikomiID);
            insertCmd2.ExecuteNonQuery();

            Console.WriteLine("サンプルデータを登録しました。");
        }
    }
}
