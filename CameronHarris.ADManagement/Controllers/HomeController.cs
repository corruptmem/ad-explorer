using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CameronHarris.ADManagement.Services;

namespace CameronHarris.ADManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly LdapServerSearcher _ldapServerSearcher;

        public HomeController(LdapServerSearcher ldapServerSearcher)
        {
            _ldapServerSearcher = ldapServerSearcher;
        }

        public ActionResult Index(string path)
        {
            if (path == null)
            {
                return View(new[] { _ldapServerSearcher.GetRoot() });
            }
            else
            {
                var root = _ldapServerSearcher.GetRoot();

                var paths = new List<string>();
                var selected = new Dictionary<string, string>();

                foreach (var section in path.Split(',').Reverse())
                {
                    string existing = string.Empty;
                    if (paths.Count != 0)
                    {
                        existing = "," + paths[paths.Count - 1];
                    }

                    paths.Add(section + existing);
                    if (paths.Count != 1)
                    {
                        selected.Add(paths[paths.Count - 2], section + existing);
                    }
                }

                var rootSections = root.Path.Count(c => c == ',') + 1;

                ViewBag.Selected = selected;
                return View(new[] { root }.Union(paths.Skip(rootSections).Select(x => _ldapServerSearcher.GetPath(x))));
            }
        }

        public ActionResult SubPath(string path)
        {
            var entity = _ldapServerSearcher.GetPath(path);
            if (entity.ObjectClasses.Any(x => x == "user"))
            {
                return View("UserInfo", entity);
            }

            return View(entity);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
