using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CameronHarris.ADManagement.Services
{
    public class AppConfig
    {
        public string LdapServer
        {
            get { return ConfigurationManager.AppSettings["ldapServer"]; }
        }

        public string LdapServerPassword
        {
            get { return ConfigurationManager.AppSettings["ldapServerPassword"]; }
        }

        public string LdapServerUser
        {
            get { return ConfigurationManager.AppSettings["ldapServerUser"]; }
        }
    }
}