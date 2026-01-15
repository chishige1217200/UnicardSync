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
        private List<MeisaiData> meisaiDataList = new List<MeisaiData>();
        private List<TorikomiData> torikomiDataList = new List<TorikomiData>();

        public TableForm()
        {
            InitializeComponent();

            // 取込区分コンボボックスの初期化
            TorikomiTypeComboBox.DataSource = TorikomiConfigHelper.Config;
            TorikomiTypeComboBox.DisplayMember = "torikomiType";
            TorikomiTypeComboBox.ValueMember = "torikomiType";

            // 取込区分コンボボックスの初期化（検索用）
            List<TorikomiConfig> torikomiConfigSearchList = new List<TorikomiConfig> (TorikomiConfigHelper.Config);
            torikomiConfigSearchList.Insert(0, new TorikomiConfig { TorikomiType = "" }); // 空行追加
            TorikomiTypeComboBoxSearch.DataSource = torikomiConfigSearchList;
            TorikomiTypeComboBoxSearch.DisplayMember = "torikomiType";
            TorikomiTypeComboBoxSearch.ValueMember = "torikomiType";

            // 今日の日付
            DateTime today = DateTime.Today;
            // 今月の末日
            DateTime endOfMonth = new DateTime(
                today.Year,
                today.Month,
                DateTime.DaysInMonth(today.Year, today.Month)
            );

            // 利用日（まで）の初期化
            this.DateTo.Value = endOfMonth;
        }

        /// <summary>
        /// 画面リサイズ時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TableForm_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            Table.Height = control.Height - 129;
        }

        /// <summary>
        /// フォーム初期表示時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TableForm_Shown(object sender, EventArgs e)
        {
            GetDatabaseData();
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
                dlg.Title = "入力元ファイル選択";

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

                        var confirmForm = new ConfirmForm(meisaiDataList, torikomiData);
                        DialogResult result = confirmForm.ShowDialog(this);
                        confirmForm.Dispose();

                        if (result == DialogResult.Yes)
                        {
                            InsertData(meisaiDataList, torikomiData);
                            MessageBox.Show("取込が完了しました。", "取込完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("取込を中止しました。", "取込中止", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ファイル '" + filePath + "' の取込に失敗しました。取込区分を間違えていませんか？\nエラー内容: " + ex.Message, "取込エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            DialogResult result = MessageBox.Show("UnicardSync形式で明細情報を全件出力します。\n取込履歴は引継できませんが、よろしいですか？\n\n＊database.dbをコピーすれば履歴も引継できます。", "出力確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (var dlg = new CommonSaveFileDialog())
                {
                    dlg.DefaultFileName = "UnicardSync_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
                    dlg.Filters.Add(new CommonFileDialogFilter("テキストファイル", "*.csv"));
                    dlg.Filters.Add(new CommonFileDialogFilter("すべてのファイル", "*.*"));
                    dlg.Title = "出力先ファイル選択";
                    // ファイル選択ダイアログ表示
                    if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        string filePath = dlg.FileName;
                        try
                        {
                            MeisaiWriter.WriteMeisai(filePath, this.meisaiDataList, this.torikomiDataList);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ファイル '" + filePath + "' の出力に失敗しました。\nエラー内容: " + ex.Message, "出力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("出力を中止しました。", "出力中止", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                MessageBox.Show("出力が完了しました。", "出力完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("出力を中止しました。", "出力中止", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
        /// DataGridViewのセルダブルクリック時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Table_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // 無効な範囲の場合
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }

            // 集計行の場合
            if (String.Empty.Equals(Table.Rows[e.RowIndex].Cells["明細番号"].Value.ToString()))
            {
                return;
            }

            // 明細編集画面を表示
            int meisaiID = int.Parse(Table.Rows[e.RowIndex].Cells["明細番号"].Value.ToString());
            var torikomiID = this.meisaiDataList.Single(m => m.ID == meisaiID).TorikomiID;
            EditForm editForm = new EditForm(this.meisaiDataList.Single(m => m.ID == meisaiID), this.torikomiDataList.Single(t => t.ID == torikomiID), this);
            editForm.Show();
        }

        /// <summary>
        /// データベースからデータを取得・表示する処理
        /// </summary>
        public void GetDatabaseData()
        {
            this.meisaiDataList = new List<MeisaiData>();
            this.torikomiDataList = new List<TorikomiData>();
            using (var connection = DatabaseConfig.GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM used WHERE del_flag = 0";

                // 取込明細テーブルからデータを取得
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        this.meisaiDataList.Add(new MeisaiData
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

                var command2 = connection.CreateCommand();
                command2.CommandText = "SELECT * FROM torikomi WHERE del_flag = 0";

                // 取込履歴テーブルからデータを取得
                using (var reader = command2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        this.torikomiDataList.Add(new TorikomiData
                        {
                            ID = int.Parse(reader["id"].ToString()),
                            FileName = reader["file_name"].ToString(),
                            TorikomiType = reader["torikomi_type"].ToString(),
                            InsDateTime = DateTime.Parse(reader["ins_datetime"].ToString()),
                            UpdDateTime = DateTime.Parse(reader["upd_datetime"].ToString()),
                            RecVer = int.Parse(reader["rec_ver"].ToString()),
                            DelFlag = int.Parse(reader["del_flag"].ToString())
                        });
                    }
                }
            }

            // LINQでデータを結合
            var joinedData = from meisai in this.meisaiDataList
                             join torikomi in this.torikomiDataList on meisai.TorikomiID equals torikomi.ID
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
            DataColumnGenerator.AddMainDataColumns(dt);

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

            // 集計行の作成
            var sum = joinedData.Sum(x => x.Amount);
            dt.Rows.Add(
                DBNull.Value,              // 明細番号
                "【合計】",                 // 利用先
                sum,                       // 金額
                DBNull.Value,              // 利用日
                DBNull.Value,              // 備考
                DBNull.Value,              // 取込区分
                DBNull.Value               // ファイル名
            );

            // DataGridViewにバインド
            Table.DataSource = dt;

            // 明細番号昇順で表示
            dt.DefaultView.Sort = "明細番号 ASC";

            Table.Columns["明細番号"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Table.Columns["利用先"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Table.Columns["利用先"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Table.Columns["金額"].DefaultCellStyle.Format = "N0";
            Table.Columns["金額"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Table.Columns["利用日"].DefaultCellStyle.Format = "yyyy/MM/dd";
            Table.Columns["利用日"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Table.Columns["備考"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Table.Columns["備考"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            Table.Columns["取込区分"].Width = 200;
            Table.Columns["ファイル名"].Width = 200;
        }

        /// <summary>
        /// 取込データ登録処理
        /// </summary>
        /// <param name="meisaiDataList"></param>
        /// <param name="torikomiData"></param>
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

                // 再検索を実施
                GetDatabaseData();
            }
        }

        /// <summary>
        /// 明細データ更新処理
        /// </summary>
        /// <param name="meisaiData">明細データ</param>
        public void UpdateMeisaiData(MeisaiData meisaiData)
        {
            using (var connection = DatabaseConfig.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    UPDATE used
                    SET place_used = $placeUsed,
                        amount_used = $amountUsed,
                        date_used = $dateUsed,
                        note = $note,
                        upd_datetime = CURRENT_TIMESTAMP,
                        rec_ver = rec_ver + 1
                    WHERE id = $id
                      AND rec_ver = $recVer
                      AND del_flag = 0;
                ";
                command.Parameters.AddWithValue("$placeUsed", meisaiData.Place);
                command.Parameters.AddWithValue("$amountUsed", meisaiData.Amount);
                command.Parameters.AddWithValue("$dateUsed", meisaiData.Date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("$note", meisaiData.Note);
                command.Parameters.AddWithValue("$id", meisaiData.ID);
                command.Parameters.AddWithValue("$recVer", meisaiData.RecVer);
                int count = command.ExecuteNonQuery();

                if (count == 0)
                {
                    throw new UpdateConcurrencyException("明細の更新に失敗しました。");
                }
            }
        }

        /// <summary>
        /// 明細データ削除処理
        /// </summary>
        /// <param name="meisaiData"></param>
        /// <exception cref="UpdateConcurrencyException"></exception>
        public void DeleteMeisaiData(MeisaiData meisaiData)
        {
            using (var connection = DatabaseConfig.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    UPDATE used
                    SET del_flag = 1,
                        upd_datetime = CURRENT_TIMESTAMP,
                        rec_ver = rec_ver + 1
                    WHERE id = $id
                      AND rec_ver = $recVer
                      AND del_flag = 0;
                ";
                command.Parameters.AddWithValue("$id", meisaiData.ID);
                command.Parameters.AddWithValue("$recVer", meisaiData.RecVer);
                int count = command.ExecuteNonQuery();

                if (count == 0)
                {
                    throw new UpdateConcurrencyException("明細の削除に失敗しました。");
                }
            }
        }

        /// <summary>
        /// 取込履歴データ削除処理
        /// </summary>
        /// <param name="meisaiData"></param>
        /// <exception cref="UpdateConcurrencyException"></exception>
        public void DeleteTorikomiData(MeisaiData meisaiData)
        {
            using (var connection = DatabaseConfig.GetConnection())
            {
                connection.Open();
                var countCommand = connection.CreateCommand();
                countCommand.CommandText = @"
                    SELECT COUNT(u1.id)
                    FROM used u1
                    JOIN used u2 ON u1.torikomi_id = u2.torikomi_id
                    WHERE u1.del_flag = 0
                      AND u2.id = $id;
                ";
                countCommand.Parameters.AddWithValue("$id", meisaiData.ID);
                long count = (long)countCommand.ExecuteScalar();

                // 有効な取込明細行が無くなったら、取込履歴も論理削除する
                if (count == 0)
                {
                    var updateCommand = connection.CreateCommand();
                    updateCommand.CommandText = @"
                    UPDATE torikomi
                    SET del_flag = 1,
                        upd_datetime = CURRENT_TIMESTAMP,
                        rec_ver = rec_ver + 1
                    WHERE id = $id
                      AND rec_ver = $recVer
                      AND del_flag = 0;
                ";
                    updateCommand.Parameters.AddWithValue("$id", meisaiData.TorikomiID);
                    updateCommand.Parameters.AddWithValue("$recVer", meisaiData.RecVer);
                    int updateCount = updateCommand.ExecuteNonQuery();

                    if (updateCount == 0)
                    {
                        throw new UpdateConcurrencyException("取込履歴の削除に失敗しました。");
                    }
                }
            }
        }
    }
}
