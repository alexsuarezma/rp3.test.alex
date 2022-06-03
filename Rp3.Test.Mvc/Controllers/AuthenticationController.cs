using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Rp3.Test.Mvc.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            Rp3.Test.Proxies.Proxy proxy = new Proxies.Proxy();

            List<Rp3.Test.Mvc.Models.PersonViewModel> persons = proxy.GetPersons().
                Select(p => new Models.PersonViewModel()
                {
                    PersonId = p.PersonId,
                    Name = p.Name
                }).ToList();

            ViewBag.Persons = persons;

            return View();
        }

        [HttpPost]
        public ActionResult Index(Rp3.Test.Mvc.Models.PersonViewModel person)
        {

            if (person.PersonId != null)
            {
                Rp3.Test.Proxies.Proxy proxy = new Proxies.Proxy();

                var commonModel = proxy.GetPerson(person.PersonId);

                person.PersonId = commonModel.PersonId;
                person.Name = commonModel.Name;

                FormsAuthentication.SetAuthCookie(person.Name, false);

                Session["Person"] = person;

                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}