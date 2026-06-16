using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnicardSync
{
    public partial class ConfirmForm : Form
    {
        private ConfirmNormal confirmNormal = null;
        private ConfirmCompare confirmCompare = null;

        public ConfirmForm()
        {
            InitializeComponent();
        }

        public ConfirmForm(List<MeisaiData> meisaiDataList, TorikomiData torikomiData)
        {
            InitializeComponent();

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

            List<int> torikomiHistoryIDList = new List<int>();

            // 過去に取込履歴がある場合とそうでない場合で表示するコンポーネントを変更する
            using (var connection = DatabaseConfig.GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                    SELECT id FROM torikomi WHERE file_name = $fileName AND del_flag = 0;
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
                confirmCompare = new ConfirmCompare();
                this.Controls.Add(confirmCompare);
                confirmCompare.Dock = DockStyle.Bottom;

                confirmCompare.Table.DataSource = dt;
                confirmCompare.Table.Columns["利用先"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                confirmCompare.Table.Columns["利用先"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                confirmCompare.Table.Columns["金額"].DefaultCellStyle.Format = "N0";
                confirmCompare.Table.Columns["金額"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                confirmCompare.Table.Columns["利用日"].DefaultCellStyle.Format = "yyyy/MM/dd";
                confirmCompare.Table.Columns["利用日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                CreateOldDataTable(torikomiHistoryIDList);
                this.label1.Text = "過去に同じ内容を取り込んだ可能性があります。" + this.label1.Text;
            }
            else
            {
                Console.WriteLine("取込履歴がありません。");
                confirmNormal = new ConfirmNormal();
                this.Controls.Add(confirmNormal);
                confirmNormal.Dock = DockStyle.Bottom;

                confirmNormal.Table.DataSource = dt;
                confirmNormal.Table.Columns["利用先"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                confirmNormal.Table.Columns["利用先"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                confirmNormal.Table.Columns["金額"].DefaultCellStyle.Format = "N0";
                confirmNormal.Table.Columns["金額"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                confirmNormal.Table.Columns["利用日"].DefaultCellStyle.Format = "yyyy/MM/dd";
                confirmNormal.Table.Columns["利用日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        public void CreateOldDataTable(List<int> idList)
        {
            var meisaiDataList = new List<MeisaiData>();
            using (var connection = DatabaseConfig.GetConnection())
            {
                connection.Open();

                var parameters = idList
                    .Select((id, index) => $"$id{index}")
                    .ToArray();

                var command = connection.CreateCommand();
                command.CommandText =
                    $"SELECT used.* " +
                    $"FROM used " +
                    $"INNER JOIN torikomi ON torikomi.id = used.torikomi_id " +
                    $"    AND torikomi.id IN ({string.Join(",", parameters)}) " +
                    $"    AND torikomi.del_flag = 0 " +
                    $"WHERE used.del_flag = 0";

                for (int i = 0; i < idList.Count; i++)
                {
                    command.Parameters.AddWithValue(parameters[i], idList[i]);
                }

                // 取込明細テーブルからデータを取得
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        meisaiDataList.Add(new MeisaiData
                        {
                            ID = int.Parse(reader["id"].ToString()),
                            Place = reader["place_used"].ToString(),
                            Amount = Convert.ToInt64(reader["amount_used"]),
                            Date = DateTime.Parse(reader["date_used"].ToString()),
                            Note = reader["note"].ToString(),
                            TorikomiID = int.Parse(reader["torikomi_id"].ToString()),
                            InsDateTime = DateTime.Parse(reader["ins_datetime"].ToString()),
                            UpdDateTime = DateTime.Parse(reader["upd_datetime"].ToString()),
                            RecVer = int.Parse(reader["rec_ver"].ToString()),
                            DelFlag = int.Parse(reader["del_flag"].ToString())
                        });
                    }
                }
            }

            DataTable dt = new DataTable("比較対象");
            DataColumnGenerator.AddConfirmDataColumns(dt, false);
            foreach (var item in meisaiDataList)
            {
                dt.Rows.Add(
                    item.ID,                  // 明細番号
                    item.Place,               // 利用先
                    item.Amount,              // 金額
                    item.Date,                // 利用日
                    item.Note,                // 備考
                    item.TorikomiID           // 取込番号
                );
            }
            confirmCompare.Table2.DataSource = dt;
            confirmCompare.Table2.Columns["明細番号"].Visible = false;
            confirmCompare.Table2.Columns["利用先"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            confirmCompare.Table2.Columns["利用先"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            confirmCompare.Table2.Columns["金額"].DefaultCellStyle.Format = "N0";
            confirmCompare.Table2.Columns["金額"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            confirmCompare.Table2.Columns["利用日"].DefaultCellStyle.Format = "yyyy/MM/dd";
            confirmCompare.Table2.Columns["利用日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            confirmCompare.Table2.Columns["備考"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            confirmCompare.Table2.Columns["備考"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        /// <summary>
        /// 画面リサイズ時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmForm_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            if (confirmNormal != null) confirmNormal.Height = control.Height - 69;
            if (confirmCompare != null) confirmCompare.Height = control.Height - 69;
        }

        /// <summary>
        /// はいボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YesButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            this.Close();
        }

        /// <summary>
        /// いいえボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            this.Close();
        }
    }
}
