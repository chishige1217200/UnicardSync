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
    public partial class ConfirmCompare : UserControl
    {
        public ConfirmCompare()
        {
            InitializeComponent();

            typeof(DataGridView).InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty, null, Table, new object[] { true });
            typeof(DataGridView).InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty, null, Table2, new object[] { true });
        }

        private void ConfirmCompare_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            Table.Height = control.Height - 30;
            Table2.Height = control.Height - 30;

            Table.Width = control.Width / 2;
            Table2.Width = control.Width / 2;

            Table.Location = new Point(Table.Location.X, 30);
            Table2.Location = new Point(control.Width / 2, 30);

            label2.Location = new Point(control.Width / 2 + 12, 9);
        }
    }
}
