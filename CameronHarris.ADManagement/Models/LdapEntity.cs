using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CameronHarris.ADManagement.Models
{
    public class LdapEntity
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> Groups { get; set; }

        public IEnumerable<string> Members { get; set; }

        public bool HasChildren { get; set; }

        public IEnumerable<LdapEntity> Children { get; set; }

        public IEnumerable<string> ObjectClasses { get; set; }
    }
}