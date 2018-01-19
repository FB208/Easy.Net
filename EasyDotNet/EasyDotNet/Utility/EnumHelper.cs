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
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetHtmlFormated(this Enum value, params object[] paras)
        {
            FieldInfo info = value.GetType().GetField(value + "");
            var attr = info.GetAttribute<HtmlElementAttributes>();
            List<object> objs = new List<object>();
            objs.Add(value);
            objs.AddRange(paras);
            return attr.GetType()
                .InvokeMember("GetFormated", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public, null, attr,
                    objs.ToArray()) + "";
        }
        
        /// <summary>
        /// 获取HtmlElementAttributes修饰的枚举值的格式化结果
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetHtmlFormated(this Type value, string displayVlaue)
        {
            var attr = value.GetAttribute<HtmlElementAttributes>();
            return attr.GetType()
                .InvokeMember("GetFormated", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public, null, attr,
                    new[] { displayVlaue }) + "";
        }
        
        /// <summary>
        /// 获取指定对象中指定类型的自定义属性字段实例
        /// </summary>
        /// <typeparam name="T">要搜索的属性类型</typeparam>
        /// <param name="self">被搜索的对象实例</param>
        /// <returns>返回的T类型属性的实例</returns>
        public static T GetAttribute<T>(this object self) where T : Attribute
        {
            CustomAttributeData attrData = null;
            if (!self.GetType().IsSubclassOf(typeof(MemberInfo)))
            {
                //attrData = self.GetType().CustomAttributes.SingleOrDefault(x => x.AttributeType == typeof(T));
            }
            else
            {
                //attrData = (self as MemberInfo).CustomAttributes.SingleOrDefault(x => x.AttributeType == typeof(T));
            }
            if (attrData != null)
            {
                T attr = (T)attrData.Constructor.Invoke(attrData.ConstructorArguments.Select(x => x.Value).ToArray());
                foreach (CustomAttributeNamedArgument customAttributeNamedArgument in attrData.NamedArguments)
                {
                    if (customAttributeNamedArgument.MemberInfo.MemberType == MemberTypes.Field)
                    {
                        ((FieldInfo)customAttributeNamedArgument.MemberInfo).SetValue(attr, customAttributeNamedArgument.TypedValue.Value);
                    }
                    else if (customAttributeNamedArgument.MemberInfo.MemberType == MemberTypes.Property)
                    {
                        //((PropertyInfo)customAttributeNamedArgument.MemberInfo).SetValue(attr, customAttributeNamedArgument.TypedValue.Value);
                    }
                }
                return attr;
            }
            else
            {
                throw new Exception("在类型" + self.GetType().FullName + "未找到类型为" + typeof(T).FullName + "的Attribute对象");
            }
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
            public string GetFormated(object value, params object[] paras)
            {
                string result = "";
                if (!paras.Any())
                {
                    result = string.Format(Format, value, "");
                }
                else if (paras.Any())
                {
                    result = string.Format(Format,value, paras[0]);
                }
                return result;
            }
        }
    }
}
