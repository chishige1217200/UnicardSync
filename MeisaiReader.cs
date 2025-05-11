using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicardSync
{
    public class MeisaiReader
    {
        public static void ReadMeisai(string fileName, TorikomiConfig config)
        {
            Encoding encoding = Encoding.GetEncoding(config.Encoding);
            List<string[]> meisai = ReadCsv(fileName, encoding);
            PrintRecords(meisai);
        }

        private static List<string[]> ReadCsv(string filePath, Encoding encoding)
        {
            var result = new List<string[]>();

            try
            {
                using (var reader = new StreamReader(filePath, encoding))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    while (csv.Read())
                    {
                        var row = new List<string>();
                        for (int i = 0; i < csv.Parser.Count; i++)
                        {
                            row.Add(csv.GetField(i));
                        }
                        result.Add(row.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ファイル '" + filePath + "' の読み込みに失敗しました: " + ex.Message);
            }

            return result;
        }

        public static void PrintRecords(List<string[]> records)
        {
            foreach (var row in records)
            {
                Console.WriteLine(string.Join(", ", row));
            }
        }
    }

    public class MeisaiData
    {
        public int ID { get; set; }
        public string Place { get; set; }
        public long Amount { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public int TorikomiID { get; set; }
        public DateTime InsDateTime { get; set; }
        public DateTime UpdDateTime { get; set; }
        public int RecVer { get; set; }
    }

    public class TorikomiData
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        public string TorikomiType { get; set; }
        public DateTime InsDateTime { get; set; }
        public DateTime UpdDateTime { get; set; }
        public int RecVer { get; set; }
    }
}
