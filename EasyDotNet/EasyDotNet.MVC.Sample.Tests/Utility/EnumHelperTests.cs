using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyDotNet.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyDotNet.MVC.Sample.EnumCodes;

namespace EasyDotNet.Utility.Tests
{
    [TestClass()]
    public class EnumHelperTests
    {
        [TestMethod()]
        public void GetDescriptionTest()
        {
            var en = ApproveEnum.Pass;
            var result=en.GetDescription();
            Assert.AreEqual("通过", result);
        }
        [TestMethod()]
        public void GetDescriptionTest2()
        {
            var en = (ApproveEnum)2;
            var result = en.GetDescription();
            Assert.AreEqual("退回", result);
        }
    }
}