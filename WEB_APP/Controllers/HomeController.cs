using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace WEB_APP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var optionsList = JsonConvert.DeserializeObject<List<string>>("[]");
            return View();
        }
    }
}