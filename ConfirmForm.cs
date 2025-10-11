using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnicardSync
{
    public partial class ConfirmForm : Form
    {
        private readonly List<MeisaiData> meisaiDataList = new List<MeisaiData>();
        private readonly TorikomiData torikomiData = null;

        public ConfirmForm()
        {
            InitializeComponent();
        }

        public ConfirmForm(List<MeisaiData> meisaiDataList, TorikomiData torikomiData)
        {
            InitializeComponent();
            this.meisaiDataList = meisaiDataList;
            this.torikomiData = torikomiData;

            DataTable dt = new DataTable("取込対象");
            DataColumnGenerator.AddConfirmDataColumns(dt, true);

            foreach (var item in meisaiDataList)
            {
                dt.Rows.Add(
                    item.Place,               // 利用先
                    item.Amount,              // 金額
                    item.Date                 // 利用日
                );
            }

            // DataGridViewにバインド
            Table.DataSource = dt;

            Table.Columns["利用先"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Table.Columns["利用先"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Table.Columns["金額"].DefaultCellStyle.Format = "N0";
            Table.Columns["金額"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns["利用日"].DefaultCellStyle.Format = "yyyy/MM/dd";
            Table.Columns["利用日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            List<int> torikomiHistoryIDList = new List<int>();

            // 過去に取込履歴がある場合とそうでない場合で表示するコンポーネントを変更する
            using (var connection = DatabaseConfig.GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                    SELECT id FROM torikomi where file_name = $fileName;
                ";
                command.Parameters.AddWithValue("$fileName", torikomiData.FileName);

                // 取込履歴テーブルからデータを取得
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        torikomiHistoryIDList.Add(int.Parse(reader["id"].ToString()));
                    }
                }
            }

            if (torikomiHistoryIDList.Count > 0)
            {
                Console.WriteLine("取込履歴があります。");
                // TODO: 取込履歴がある場合、表示するコンポーネントを変更
            }
        }

        /// <summary>
        /// 画面リサイズ時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmForm_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            Table.Height = control.Height - 69;
        }

        /// <summary>
        /// はいボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YesButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        /// <summary>
        /// いいえボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }
    }
}
