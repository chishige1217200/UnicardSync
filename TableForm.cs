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

            //Console.WriteLine(torikomiTypeComboBox.SelectedValue.ToString());
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {

        }
    }
}
