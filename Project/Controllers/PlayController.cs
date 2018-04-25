using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.Business;
using Services.Data.Models;

namespace HandsOn.Controllers
{
    public class PlayController : Controller
    {
        public ActionResult Index()
        {
            Services.Business.PlayBusiness.Instance.Crime = null;
            return View();
        }

        /// <summary>
        /// Confere as seleções do usuário para o crime selecionado
        /// </summary>
        /// <param name="suspect"></param>
        /// <param name="place"></param>
        /// <param name="weapon"></param>
        /// <returns></returns>
        public JsonResult Check(int suspect, int place, int weapon)
        {
            List<string> result = Services.Business.PlayBusiness.Instance.Valid(suspect, place, weapon);

            var _result = result[0].ToBoolNullSafe();
            var _message = result[1].ToStringNullSafe();

            return this.Json(new { success = _result, message = _message }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Grava o histório de operações de seleções do operador
        /// </summary>
        /// <param name="suspect"></param>
        /// <param name="place"></param>
        /// <param name="weapon"></param>
        /// <returns></returns>
        public JsonResult History_Save(int suspect, int place, int weapon)
        {
            //registrar o log de operação.
            var _operador = Common.GetIpAddress;

            return this.Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}