using CsvHelper;
using CsvHelper.Configuration;
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
        public static List<MeisaiData> ReadMeisai(string filePath, TorikomiConfig config)
        {

            Encoding encoding = Encoding.GetEncoding(config.Encoding);
            List<string[]> csvDataList = ReadCsv(filePath, encoding);
            PrintRecords(csvDataList);

            if (csvDataList.Count <= config.SkipTopRows + config.SkipBottomRows)
            {
                throw new Exception("取り込みするファイルの行数が不足しています。");
            }

            if (csvDataList[config.SkipTopRows].Length < TorikomiConfigHelper.GetMaxColumnsIndex(config))
            {
                throw new Exception("取り込みするファイルの列数が不足しています。");
            }

            List<MeisaiData> meisaiDataList = new List<MeisaiData>();

            for (int i = config.SkipTopRows; i < csvDataList.Count - config.SkipBottomRows; i++)
            {
                meisaiDataList.Add(new MeisaiData
                {
                    ID = null,
                    Place = csvDataList[i][config.PlaceUsed],
                    Amount = long.Parse(csvDataList[i][config.AmountUsed], NumberStyles.AllowThousands, CultureInfo.InvariantCulture),
                    Date = DateTime.Parse(csvDataList[i][config.DateUsed].Trim()),
                    Note = config.Note != -1 ? csvDataList[i][config.Note] : "",
                    TorikomiID = null,
                    InsDateTime = null,
                    UpdDateTime = null,
                    RecVer = null
                });
            }

            return meisaiDataList;
        }

        private static List<string[]> ReadCsv(string filePath, Encoding encoding)
        {
            var result = new List<string[]>();

            try
            {
                using (var reader = new StreamReader(filePath, encoding))
                {
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HasHeaderRecord = false,
                        BadDataFound = null
                    };

                    using (var csv = new CsvReader(reader, config))
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

            }
            catch (Exception ex)
            {
                throw ex;
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
        public int? ID { get; set; } // 取り込み時はnullを設定
        public string Place { get; set; }
        public long Amount { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public int? TorikomiID { get; set; } // 取り込み時はnullを設定
        public DateTime? InsDateTime { get; set; } // 取り込み時はnullを設定
        public DateTime? UpdDateTime { get; set; } // 取り込み時はnullを設定
        public int? RecVer { get; set; } // 取り込み時はnullを設定
    }

    public class TorikomiData
    {
        public int? ID { get; set; } // 取り込み時はnullを設定
        public string FileName { get; set; }
        public string TorikomiType { get; set; }
        public DateTime? InsDateTime { get; set; } // 取り込み時はnullを設定
        public DateTime? UpdDateTime { get; set; } // 取り込み時はnullを設定
        public int? RecVer { get; set; } // 取り込み時はnullを設定
    }
}
