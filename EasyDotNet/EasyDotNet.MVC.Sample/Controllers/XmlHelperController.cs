using EasyDotNet.MVC.Sample.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace EasyDotNet.MVC.Sample.Controllers
{
    public class XmlHelperController : Controller
    {
        /// <summary>
        /// Class序列化
        /// </summary>
        /// <returns></returns>
        public string Serializer_Class()
        {
            List<User> users = new User().GetList();
            var result=EasyDotNet.Utility.XmlHelper.Serializer(typeof(List<User>), users);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result);
            return xml.InnerXml.ToString();
        }

        /// <summary>
        /// Class反序列化
        /// </summary>
        /// <returns></returns>
        public object Deserialize_Class(string xmlStr)
        {
            if (string.IsNullOrWhiteSpace(xmlStr))
            {
                xmlStr =
                    "<ArrayOfUser><User><Id>1</Id><Name>张三</Name><Age>10</Age></User><User><Id>2</Id><Name>李四</Name><Age>12</Age></User><User><Id>3</Id><Name>王五</Name><Age>10</Age></User><User><Id>4</Id><Name>赵六</Name><Age>20</Age></User></ArrayOfUser>";
            }
            var result = EasyDotNet.Utility.XmlHelper.Deserialize(typeof(List<User>), xmlStr);
            return result;
        }
        /// <summary>
        /// DataTable序列化
        /// </summary>
        /// <returns></returns>
        public string Serializer_DataTable()
        {
            // 生成DataTable对象用于测试
            DataTable dt1 = new DataTable("mytable");   // 必须指明DataTable名称

            dt1.Columns.Add("Dosage", typeof(int));
            dt1.Columns.Add("Drug", typeof(string));
            dt1.Columns.Add("Patient", typeof(string));
            dt1.Columns.Add("Date", typeof(DateTime));

            // 添加行
            dt1.Rows.Add(25, "Indocin", "David", DateTime.Now);
            dt1.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
            dt1.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
            dt1.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
            dt1.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);

            // 序列化
            string xmlStr = EasyDotNet.Utility.XmlHelper.Serializer(typeof(DataTable), dt1);
            return xmlStr;
        }
        /// <summary>
        /// DataTable反序列化
        /// </summary>
        /// <returns></returns>
        public object Deserialize_DataTable(string xmlStr)
        {
            if (string.IsNullOrWhiteSpace(xmlStr))
            {
                
                //先序列化一个
                xmlStr =
                    Serializer_DataTable();
            }
            // 反序列化成DataTable对xml的格式要求比较严格，如果不是由DataTable序列化来的xml而是手写的xml很难反序列化成功
            DataTable dt2 = EasyDotNet.Utility.XmlHelper.Deserialize(typeof(DataTable), xmlStr) as DataTable;

            return dt2;
        }

    }
}