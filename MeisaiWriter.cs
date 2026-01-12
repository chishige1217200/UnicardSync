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
    public class MeisaiWriter
    {
        public static void WriteMeisai(string filePath, List<MeisaiData> meisaiDataList, List<TorikomiData> torikomiDataList)
        {
            Encoding encoding = Encoding.GetEncoding("UTF-8");
            WriteCsv(filePath, encoding, meisaiDataList, torikomiDataList);
        }

        private static void WriteCsv(string filePath, Encoding encoding, List<MeisaiData> meisaiDataList, List<TorikomiData> torikomiDataList)
        {
            try
            {
                var records = (from meisai in meisaiDataList
                               join torikomi in torikomiDataList on meisai.TorikomiID equals torikomi.ID
                               select new
                               {
                                   meisai.Place,
                                   meisai.Amount,
                                   meisai.Date,
                                   meisai.Note,
                                   torikomi.FileName,
                                   torikomi.TorikomiType
                               });

                var outputRecords = new List<UnicardSyncData>();
                foreach (var record in records)
                {
                    outputRecords.Add(new UnicardSyncData
                    {
                        Place = record.Place,
                        Amount = record.Amount,
                        Date = record.Date,
                        Note = record.Note,
                        FileName = record.FileName,
                        TorikomiType = record.TorikomiType
                    });
                }

                using (var writer = new StreamWriter(filePath, false, encoding))
                {
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(outputRecords);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private class UnicardSyncData
        {
            public string Place { get; set; }
            public long Amount { get; set; }
            public DateTime Date { get; set; }
            public string Note { get; set; }
            public string FileName { get; set; }
            public string TorikomiType { get; set; }
        }
    }
}
