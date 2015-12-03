using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NSA.Controllers
{
    public class ResultControl
    {
        public string Mensaje { get; set; }
        public string Tipo { get; set; }

        public string Data { get; set; }
    }
    public class HomeController : Controller
    {
         
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Encrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Json(new ResultControl() { Data = "Error Empty String :-P"});
            }
            if (value.Length > 7999)
            {
                return Json(new ResultControl() { Data = "Error string is to large :-P" });
            }

            var result = Data.Fuciones.CRYPT.Encrypt(value);

            return Json(new ResultControl() { Data = result });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}