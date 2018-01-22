using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EasyDotNet.Utility
{
    public static class EnumHelper
    {
        /// <summary>
        /// 获取枚举描述特性值
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="enumerationValue">枚举值</param>
        /// <returns>枚举值的描述/returns>
        public static string GetDescription<TEnum>(this TEnum enumerationValue)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue必须是一个枚举值", "enumerationValue");
            }

            //使用反射获取该枚举的成员信息
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //返回枚举值得描述信息
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            //如果没有描述特性的值，返回该枚举值得字符串形式
            return enumerationValue.ToString();
        }

        /// <summary>
        /// 将枚举对象转换为对应的字典形式 key enumString value enumValue
        /// </summary>
        /// <param name="e">typeof(Enum)</param>
        /// <returns></returns>
        public static Dictionary<string, string> ToDictionary(this Type e)
        {
            return e.ToDictionary(null, null);
        }
        /// <summary>
        /// 将枚举对象转换为对应的字典形式 key enumString value enumValue
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="e"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ToDictionary(this Type e, Func<Type, string, string> keySelector, Func<Type, int, string> valueSelector)
        {
            if (!e.IsEnum)
            {
                throw new NotImplementedException("扩展方法只能使用在Type System.Enum上");
            }
            Dictionary<string, string> results = new Dictionary<string, string>();
            int[] values = (int[])Enum.GetValues(e);
            foreach (int value in values)
            {
                results.Add(keySelector == null ? Enum.ToObject(e, value) + "" : keySelector(e, Enum.ToObject(e, value) + ""), valueSelector == null ? value + "" : valueSelector(e, value));
            }
            return results;
        }

        /// <summary>
        /// 获取HtmlElementAttributes修饰的枚举值的格式化结果
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="value">枚举值</param>
        /// <param name="paras">string.format中替换{0},{1}的value</param>
        /// <returns></returns>
        public static string GetHtmlFormated(this Type type, int value, params object[] paras)
        {
            string result = "";
            //Type type = eEnum.GetType();
            MemberInfo[] memberInfo = type.GetMember(Enum.GetName(type,value));
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(HtmlElementAttributes), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //返回枚举值得描述信息
                    result = ((HtmlElementAttributes)attrs[0]).Format;
                }
                if (!paras.Any())
                {
                    result = string.Format(result, "");
                }
                else if (paras.Any())
                {
                    result = string.Format(result, paras);
                }
            }
            return result;
        }
        
        
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum, AllowMultiple = false, Inherited = true)]
        public class HtmlElementAttributes : Attribute
        {
            /// <summary>
            /// the html style of this value
            /// </summary>
            public string Style = "";
            public string Class = "";
            public string Format = "";
            public HtmlElementAttributes()
            {

            }
            
        }
    }
}
