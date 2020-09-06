using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LDAP_DotNetF
{
    public partial class frmCore : Form
    {
        public frmCore()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            IsAuthenticated(txtDomain.Text, txtUser.Text, txtPassword.Text);
        }

        private string _path { get; set; }
        private string _filterAttribute { get; set; }

        public bool IsAuthenticated(string domain, string username, string pwd)
        {
            txtResult.Text = "";
            txtPath.Text = "";
            txtAttr.Text = "";
            string domainAndUsername = domain + "\\" + username;
            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);
            try
            {
                object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();
                if ((result == null))
                {
                    txtResult.Text = "Login Failed";
                    return false;
                }

                _path = result.Path;
                _filterAttribute = result.Properties["cn"][0].ToString();

                txtPath.Text = _path;
                txtAttr.Text = _filterAttribute;

            }
            catch (Exception ex)
            {
                txtResult.Text = "Login Failed - " + ex.Message;
                return false;
            }

            txtResult.Text = "Login Success";
            return true;
        }
    }
}
