using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnicardSync
{
    public partial class TableForm : Form
    {
        public TableForm()
        {
            InitializeComponent();

            TorikomiTypeComboBox.DataSource = TorikomiConfigHelper.Config;
            TorikomiTypeComboBox.DisplayMember = "torikomiType";
            TorikomiTypeComboBox.ValueMember = "torikomiType";
        }

        private void TableForm_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            this.Table.Height = control.Height - 69;
        }

        /// <summary>
        /// データ取込ボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportButton_Click(object sender, EventArgs e)
        {
            if (TorikomiTypeComboBox.SelectedValue == null || TorikomiTypeComboBox.SelectedValue.ToString() == "")
            {
                MessageBox.Show("データ取込を行う前に取込区分を選択してください。", "取込エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 初期選択値
            string filePath = string.Empty;

            // ファイル選択ダイアログ
            using (var dlg = new CommonOpenFileDialog())
            {
                dlg.IsFolderPicker = false;  // true:フォルダ選択  false:ファイル選択

                dlg.Filters.Add(new CommonFileDialogFilter("テキストファイル", "*.csv"));
                dlg.Filters.Add(new CommonFileDialogFilter("すべてのファイル", "*.*"));
                dlg.Multiselect = false;
                dlg.Title = "ファイルを選択してください";

                // ファイル選択ダイアログ表示
                if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    // 選択値で更新
                    filePath = dlg.FileName;
                    try
                    {
                        // ファイルの読込
                        List<MeisaiData> meisaiDataList = MeisaiReader.ReadMeisai(filePath, TorikomiConfigHelper.Config[TorikomiTypeComboBox.SelectedIndex]);
                        TorikomiData torikomiData = new TorikomiData
                        {
                            FileName = Path.GetFileName(filePath),
                            TorikomiType = TorikomiTypeComboBox.SelectedValue.ToString(),
                            InsDateTime = null,
                            UpdDateTime = null,
                            RecVer = null
                        };

                        InsertData(meisaiDataList, torikomiData);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ファイル '" + filePath + "' の読込に失敗しました。取込区分を間違えていませんか?\nエラー内容: " + ex.Message, "読込エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// データ出力ボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportButton_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 検索ボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            GetDatabaseData();
        }

        /// <summary>
        /// データベースからデータを取得する処理
        /// </summary>
        private void GetDatabaseData()
        {
            List<MeisaiData> meisaiDataList = new List<MeisaiData>();
            List<TorikomiData> torikomiDataList = new List<TorikomiData>();
            using (var connection = DatabaseConfig.GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM used";

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
                            RecVer = int.Parse(reader["rec_ver"].ToString())
                        });
                    }
                }

                var command2 = connection.CreateCommand();
                command2.CommandText = "SELECT * FROM torikomi";

                // 取込履歴テーブルからデータを取得
                using (var reader = command2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        torikomiDataList.Add(new TorikomiData
                        {
                            ID = int.Parse(reader["id"].ToString()),
                            FileName = reader["file_name"].ToString(),
                            TorikomiType = reader["torikomi_type"].ToString(),
                            InsDateTime = DateTime.Parse(reader["ins_datetime"].ToString()),
                            UpdDateTime = DateTime.Parse(reader["upd_datetime"].ToString()),
                            RecVer = int.Parse(reader["rec_ver"].ToString())
                        });
                    }
                }
            }

            // LINQでデータを結合
            var joinedData = from meisai in meisaiDataList
                             join torikomi in torikomiDataList on meisai.TorikomiID equals torikomi.ID
                             select new
                             {
                                 meisai.ID,
                                 meisai.Place,
                                 meisai.Amount,
                                 meisai.Date,
                                 meisai.Note,
                                 torikomi.FileName,
                                 torikomi.TorikomiType
                             };

            // DataTableを作成（日本語の列名を指定）
            DataTable dt = new DataTable("明細データ");
            dt.Columns.Add("明細番号", typeof(int));          // MeisaiData.ID
            dt.Columns.Add("利用先", typeof(string));           // MeisaiData.Place
            dt.Columns.Add("金額", typeof(long));             // MeisaiData.Amount
            dt.Columns.Add("利用日", typeof(DateTime));         // MeisaiData.Date
            dt.Columns.Add("備考", typeof(string));           // MeisaiData.Note
            dt.Columns.Add("取込区分", typeof(string));       // TorikomiData.TorikomiType
            dt.Columns.Add("ファイル名", typeof(string));     // TorikomiData.FileName

            foreach (var item in joinedData)
            {
                dt.Rows.Add(
                    item.ID,                  // 明細番号
                    item.Place,               // 利用先
                    item.Amount,              // 金額
                    item.Date,                // 利用日
                    item.Note,                // 備考
                    item.TorikomiType,        // 取込区分
                    item.FileName             // ファイル名
                );
            }

            // DataGridViewにバインド
            Table.DataSource = dt;

            Table.Columns["明細番号"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Table.Columns["利用先"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Table.Columns["利用先"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Table.Columns["金額"].DefaultCellStyle.Format = "N0";
            Table.Columns["金額"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns["利用日"].DefaultCellStyle.Format = "yyyy/MM/dd";
            Table.Columns["利用日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Table.Columns["備考"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Table.Columns["備考"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Table.Columns["ファイル名"].Width = 200;
        }

        public void InsertData(List<MeisaiData> meisaiDataList, TorikomiData torikomiData)
        {
            using (var connection = DatabaseConfig.GetConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        INSERT INTO torikomi(file_name, torikomi_type)
                        VALUES ($fileName, $torikomiType);
                        SELECT last_insert_rowid();
                    ";
                    command.Parameters.AddWithValue("$fileName", torikomiData.FileName);
                    command.Parameters.AddWithValue("$torikomiType", torikomiData.TorikomiType);
                    long torikomiID = (long)command.ExecuteScalar();

                    foreach (var meisaiData in meisaiDataList)
                    {
                        var insertCommand = connection.CreateCommand();
                        insertCommand.CommandText = @"
                            INSERT INTO used(place_used, amount_used, date_used, note, torikomi_id)
                            VALUES ($placeUsed, $amountUsed, $dateUsed, $note, $torikomiID);
                        ";
                        insertCommand.Parameters.AddWithValue("$placeUsed", meisaiData.Place);
                        insertCommand.Parameters.AddWithValue("$amountUsed", meisaiData.Amount);
                        insertCommand.Parameters.AddWithValue("$dateUsed", meisaiData.Date.ToString("yyyy-MM-dd"));
                        insertCommand.Parameters.AddWithValue("$note", meisaiData.Note);
                        insertCommand.Parameters.AddWithValue("$torikomiID", torikomiID);
                        insertCommand.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
