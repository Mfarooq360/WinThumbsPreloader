using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinThumbsPreloader.Properties;

namespace WinThumbsPreloader
{
    public partial class HelpForm : Form // Currently a work in progress
    {
        public HelpForm()
        {
            InitializeComponent();
        }

        private void HelpForm_Load(object sender, EventArgs e)
        {
            this.Icon = Resources.MainIcon;
        }

        private void HelpTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HelpButton_Click(object sender, EventArgs e)
        {

        }

        private void FAQButton_Click(object sender, EventArgs e)
        {

        }
    }
}
