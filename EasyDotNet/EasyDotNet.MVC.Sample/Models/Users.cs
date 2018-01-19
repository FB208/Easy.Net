using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyDotNet.MVC.Sample.Models
{
    
    public class User
    {
        private User user;
        public User(){
           
            }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public User GetModel()
        {
            user = new User()
            {
                Id=1,Name="张三",Age=20
            };
            return user;
        }

        public List<User> GetList()
        {
            List<User> list = new List<User>()
            {
                new User() {Id=1,Name="张三",Age=10 },
                new User() {Id=2,Name="李四",Age=12 },
                new User() {Id=3,Name="王五",Age=10 },
                new User() {Id=4,Name="赵六",Age=20 }
            };
            return list;
        }
    }
}