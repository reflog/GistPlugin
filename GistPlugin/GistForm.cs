using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GistPlugin
{
    public partial class GistForm : Form
    {
        public GistForm()
        {
            InitializeComponent();
            txtAPI.Text = Properties.Settings.Default.api;
            txtUser.Text = Properties.Settings.Default.login;
        }
        public string Output = "";
        public string Filename = "";
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtAPI.Text.Length == 0 || txtContent.Text.Length == 0 || txtFile.Text.Length == 0 || txtUser.Text.Length == 0)
            {
                MessageBox.Show("Please fill in all the fields!");
            }
            else
            {
                Properties.Settings.Default.api = txtAPI.Text;
                Properties.Settings.Default.login = txtUser.Text;
                Properties.Settings.Default.Save();
                DialogResult = DialogResult.OK;
                Output = txtContent.Text;
                Filename = txtFile.Text;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
