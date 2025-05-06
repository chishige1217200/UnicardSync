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
        }

        private void TableForm_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            this.table.Width = control.Width - 16;
            this.table.Height = control.Height - 69;
        }
    }
}
