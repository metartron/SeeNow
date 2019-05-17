using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SeeNow
{
	public static partial class Extention
    {
		/// <summary>
        /// 取出相對key值的value
        /// </summary>
        /// <param name="PropertyName">key值</param>
        /// <param name="o">傳入物件</param>
        /// <returns>value值</returns>
        public static Object GetPropertyValueByName(string PropertyName, Object o)
        {
            if (o == null)
            {
                o = new { };
            }
            
            Object returnObject = new Object();
            PropertyInfo[] p1 = o.GetType().GetProperties();
            foreach (PropertyInfo pi in p1)
            {
                if (pi.Name.ToLower() == PropertyName.ToLower())
                {
                    returnObject = pi.GetValue(o);
                }
            }
            return returnObject;
        }
    }
}