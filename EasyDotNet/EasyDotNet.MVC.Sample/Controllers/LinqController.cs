using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyDotNet.LinQ;
using EasyDotNet.MVC.Sample.Models;

namespace EasyDotNet.MVC.Sample.Controllers
{
    public class LinqController : Controller
    {
        
        // GET: Linq
        public string DistinctExtensions()
        {
            List<User> users = new User().GetList();

            users=users.Distinct(m => m.Age).ToList();

            string resultStr = "";
            foreach (var user in users)
            {
                resultStr += user.Name+"<br/>";
            }
            return resultStr;
        }
    }
}