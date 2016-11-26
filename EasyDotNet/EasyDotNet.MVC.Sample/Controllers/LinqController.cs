using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyDotNet.LinQ;

namespace EasyDotNet.MVC.Sample.Controllers
{
    public class LinqController : Controller
    {
        class User
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
        // GET: Linq
        public string DistinctExtensions()
        {
            List<User> users = new List<User>()
            {
                new User() {Name="张三",Age=10 },
                new User() {Name="李四",Age=12 },
                new User() {Name="王五",Age=10 },
                new User() {Name="赵六",Age=20 }
            };

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