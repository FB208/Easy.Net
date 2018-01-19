using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyDotNet.MVC.Sample.EnumCodes;
using EasyDotNet.Utility;

namespace EasyDotNet.MVC.Sample.Controllers
{
    public class EnumHelperController : Controller
    {
        // GET: EnumHelper
        public ActionResult ToDictionary()
        {
            var dict = typeof(ApproveEnum).ToDictionary();
            return View();
        }

        public ActionResult ToDictionary2()
        {
            
            var dict = typeof(ApproveEnum).ToDictionary((m,n)=>m.FullName+n,(m,n)=>(n+1).ToString());
            return View();
        }
    }
}