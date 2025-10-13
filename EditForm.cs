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
    public partial class EditForm : Form
    {
        private readonly TableForm tableForm;
        private readonly MeisaiData meisaiData;
        public EditForm()
        {
            InitializeComponent();
        }

        public EditForm(MeisaiData meisaiData, TorikomiData torikomiData, TableForm tableForm)
        {
            InitializeComponent();

            this.tableForm = tableForm;
            this.meisaiData = meisaiData;

            // コンポーネントに値を設定
            textBoxUsedID.Text = meisaiData.ID.ToString();
            textBoxPlaceUsed.Text = meisaiData.Place;
            numericUpDownAmountUsed.Value = meisaiData.Amount;
            dateTimePickerDateUsed.Value = meisaiData.Date;
            textBoxNote.Text = meisaiData.Note;
        }

        /// <summary>
        /// 戻るボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 更新するボタン押下時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("入力した内容で更新します。よろしいですか？", $"更新確認[明細番号:{meisaiData.ID}]", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // 表示を更新する
                tableForm.GetDatabaseData();
                this.Close();
            }
        }
    }
}
