using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            if (HttpContext.Session.GetInt32("_IdEmployee") == null)
            {
                return RedirectToAction("index", "Login");
            }

            return View();
        }
    }
}
