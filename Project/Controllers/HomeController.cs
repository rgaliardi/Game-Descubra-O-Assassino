using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HandsOn.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewData["Message"] = "Hands-on - Projeto base para validação de conceito em ASP.NET MVC Framework 4.7.1";

            return View();
        }

        public ActionResult Contact()
        {
            ViewData["Message"] = "Arquitetura de Soluções e Software.";

            return View();
        }
    }
}