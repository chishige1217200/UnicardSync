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

            this.Table.Width = control.Width - 16;
            this.Table.Height = control.Height - 69;
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            if (TorikomiTypeComboBox.SelectedValue == null || TorikomiTypeComboBox.SelectedValue.ToString() == "")
            {
                MessageBox.Show("データ取込を行う前に取込区分を選択してください。", "取込エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        // ファイルの読み込み
                        List<MeisaiData> meisaiDataList = MeisaiReader.ReadMeisai(filePath, TorikomiConfigHelper.Config[TorikomiTypeComboBox.SelectedIndex]);

                        string fileName = Path.GetFileName(filePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ファイル '" + filePath + "' の読み込みに失敗しました: " + ex.Message);
                    }
                }
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {

        }
    }
}
