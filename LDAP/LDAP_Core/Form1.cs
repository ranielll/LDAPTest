﻿using Novell.Directory.Ldap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LDAP_Core
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool aaa = chkSSL.Checked;
            using (LdapConnection testConn = new LdapConnection() { SecureSocketLayer = (chkSSL.Checked) })
            {
                try
                {

                    testConn.Connect(txtDomain.Text, Convert.ToInt32(txtPort.Text));

                    testConn.Bind(txtUser.Text, txtPassword.Text);

                    var res = testConn.Bound;

                    if (res)
                        txtResult.Text = "Login Success";
                }
                catch (Exception)
                {
                    txtResult.Text = "Login Failed";
                }
            }
        }
    }
}
