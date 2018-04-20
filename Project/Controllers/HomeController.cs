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
            ViewBag.Message = "Hands-on - Descubra o Assassino.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Projeto base para validação de conceito em Arquitetura de Soluções e Software.";

            return View();
        }
    }
}