using pr0.net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pr0.net_Demo
{
    public partial class frmTest : MetroFramework.Forms.MetroForm
    {
        pr0API p = new pr0API();

        public frmTest()
        {
            InitializeComponent();            
        }

        private void PostLine(string line)
        {
            metroTextBox1.Text = line + "\r\n" + metroTextBox1.Text;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            //try
            //{
                p.Login(txbUsername.Text, txbPassword.Text);
                PostLine("Login successfull!");
                PostLine("ID: " + p.Session.Id);
            //}
            //catch (Exception ex)
            //{
            //    PostLine("Login failed!");
            //    PostLine("Exception: " + ex.Message);
            //}
        }
    }
}
