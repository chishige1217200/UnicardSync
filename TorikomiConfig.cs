using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicardSync;

namespace UnicardSync
{
    public class TorikomiConfigHelper
    {
        public static List<TorikomiConfig> Config { get; private set; } = new List<TorikomiConfig>();
        public static void LoadConfig()
        {
            string configFolder = "config";

            if (!Directory.Exists(configFolder))
            {
                Console.WriteLine("設定フォルダが存在しません。");
                return;
            }

            string[] configFiles = Directory.GetFiles(configFolder, "*.dat");

            foreach (string filePath in configFiles)
            {
                try
                {
                    using (var reader = new StreamReader(filePath, Encoding.GetEncoding("UTF-8")))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csv.Context.RegisterClassMap<TorikomiConfigMap>();

                        TorikomiConfig config = csv.GetRecords<TorikomiConfig>().FirstOrDefault();
                        if (config != null)
                        {
                            Config.Add(config);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ファイル '" + filePath + "' の読み込みに失敗しました: " + ex.Message);
                }
            }

            // 出力して確認
            Console.WriteLine("読み込まれた設定:");
            foreach (TorikomiConfig cfg in Config)
            {
                Console.WriteLine("- " + cfg.TorikomiType + ", " + cfg.Encoding + ", TopSkip=" + cfg.SkipTopRows);
            }
        }

        public static int GetMaxColumnsIndex(TorikomiConfig config)
        {
            int maxColumnsIndex = 0;

            // 設定されている列番号の内、最も大きい値を取得
            if (config.PlaceUsed > maxColumnsIndex) maxColumnsIndex = config.PlaceUsed;
            if (config.AmountUsed > maxColumnsIndex) maxColumnsIndex = config.AmountUsed;
            if (config.DateUsed > maxColumnsIndex) maxColumnsIndex = config.DateUsed;
            if (config.Note > maxColumnsIndex) maxColumnsIndex = config.Note;

            return maxColumnsIndex;
        }
    }

    public class TorikomiConfig
    {
        public string TorikomiType { get; set; }
        public string Encoding { get; set; }
        public int SkipTopRows { get; set; }
        public int SkipBottomRows { get; set; }
        public int PlaceUsed { get; set; }
        public int AmountUsed { get; set; }
        public int DateUsed { get; set; }
        public int Note { get; set; }
    }
}

internal sealed class TorikomiConfigMap : ClassMap<TorikomiConfig>
{
    public TorikomiConfigMap()
    {
        Map(m => m.TorikomiType).Name("torikomi_type");
        Map(m => m.Encoding).Name("encoding");
        Map(m => m.SkipTopRows).Name("skip_top_rows");
        Map(m => m.SkipBottomRows).Name("skip_bottom_rows");
        Map(m => m.PlaceUsed).Name("place_used");
        Map(m => m.AmountUsed).Name("amount_used");
        Map(m => m.DateUsed).Name("date_used");
        Map(m => m.Note).Name("note");
    }
}
