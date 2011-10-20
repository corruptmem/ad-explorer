using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CameronHarris.ADManagement.Models;

namespace CameronHarris.ADManagement.Services
{
    public class LdapServerSearcher
    {
        private readonly AppConfig _appConfig;

        public LdapServerSearcher(AppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public LdapEntity GetRoot()
        {
            var rootDirectory = new DirectoryEntry(_appConfig.LdapServer, _appConfig.LdapServerUser, _appConfig.LdapServerPassword);
            return MakeEntity(rootDirectory);
        }

        private static IEnumerable<LdapEntity> GetChildren(DirectoryEntry dirEnt)
        {
            return dirEnt.Children.Cast<DirectoryEntry>().Select(MakeEntity);
        }

        private static LdapEntity MakeEntity(DirectoryEntry dirent)
        {
            return new LdapEntity
                       {
                           Name = (string) dirent.Properties["name"][0],
                           Children = GetChildren(dirent),
                           Path = (string) dirent.Properties["distinguishedName"][0],
                           HasChildren = dirent.Children.Cast<DirectoryEntry>().Count() > 0,
                           ObjectClasses = dirent.Properties["objectClass"].Cast<string>().ToList(),
                           Email =
                               dirent.Properties["mail"] != null && dirent.Properties["mail"].Count > 0
                                   ? (string) dirent.Properties["mail"][0]
                                   : null,
                           Groups = dirent.Properties["memberOf"] != null ? dirent.Properties["memberOf"].Cast<string>() : Enumerable.Empty<string>()
                       };
        }

        public LdapEntity GetPath(string path)
        {
            return MakeEntity(new DirectoryEntry(_appConfig.LdapServer + "/" + path, _appConfig.LdapServerUser, _appConfig.LdapServerPassword));
        }
    }
}