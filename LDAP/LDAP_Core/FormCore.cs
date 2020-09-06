using Novell.Directory.Ldap;
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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (LdapConnection testConn = new LdapConnection() { SecureSocketLayer = (chkSSL.Checked) })
            {
                try
                {

                    testConn.Connect(txtDomain.Text, Convert.ToInt32(txtPort.Text));

                    testConn.Bind(txtUser.Text, txtPassword.Text);

                    var res = testConn.Bound;

                    if (res)
                        txtResult.Text = "Login Success";
                    else
                        txtResult.Text = "Login Failed";
                }
                catch (Exception ex)
                {
                    txtResult.Text = "Login Failed - " + ex.Message;
                }
            }
        }
    }
}
