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

            //const string LDAP_PATH = "EX://exldap.example.com:5555";
            //const string LDAP_DOMAIN = "exldap.example.com:5555";

            //using (var context = new PrincipalContext(ContextType.Domain, LDAP_DOMAIN, "service_acct_user", "service_acct_pswd"))
            //{
            //    if (context.ValidateCredentials(username, password))
            //    {
            //        using (var de = new DirectoryEntry(LDAP_PATH))
            //        using (var ds = new DirectorySearcher(de))
            //        {
            //            // other logic to verify user has correct permissions

            //            // User authenticated and authorized
            //            var identities = new List<ClaimsIdentity> { new ClaimsIdentity("custom auth type") };
            //            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identities), Options.Scheme);
            //            return Task.FromResult(AuthenticateResult.Success(ticket));
            //        }
            //    }
            //}

            //// User not authenticated
            //return Task.FromResult(AuthenticateResult.Fail("Invalid auth key."));
        }

        private string _path { get; set; }
        private string _filterAttribute { get; set; }

        public bool IsAuthenticated(string domain, string username, string pwd)
        {
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
