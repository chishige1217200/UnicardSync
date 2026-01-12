using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicardSync
{
    public class DataColumnGenerator
    {
        /// <summary>
        /// TableFormで使用するDataTableに必要な列を追加する
        /// </summary>
        /// <param name="dt">DataTable</param>
        public static void AddMainDataColumns(DataTable dt) {
            dt.Columns.Add("明細番号", typeof(int));          // MeisaiData.ID
            dt.Columns.Add("利用先", typeof(string));           // MeisaiData.Place
            dt.Columns.Add("金額", typeof(long));             // MeisaiData.Amount
            dt.Columns.Add("利用日", typeof(DateTime));         // MeisaiData.Date
            dt.Columns.Add("備考", typeof(string));           // MeisaiData.Note
            dt.Columns.Add("取込区分", typeof(string));       // TorikomiData.TorikomiType
            dt.Columns.Add("ファイル名", typeof(string));     // TorikomiData.FileName
        }

        /// <summary>
        /// ConfirmFormで使用するDataTableに必要な列を追加する
        /// </summary>
        /// <param name="dt"></param>
        public static void AddConfirmDataColumns(DataTable dt, Boolean newData)
        {
            if (!newData) {
                dt.Columns.Add("明細番号", typeof(int));          // MeisaiData.ID
            }
            dt.Columns.Add("利用先", typeof(string));           // MeisaiData.Place
            dt.Columns.Add("金額", typeof(long));             // MeisaiData.Amount
            dt.Columns.Add("利用日", typeof(DateTime));         // MeisaiData.Date
            if (!newData) { 
                dt.Columns.Add("備考", typeof(string));           // MeisaiData.Note
            }
        }   
    }
}
