using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace aspnetcore3_demo.Helpers {
    /// <summary>
    /// 集合数据塑形
    /// </summary>
    public static class IEnumerableExtensions {
        /// <summary>
        /// 数据塑形扩展方法
        /// 根据传入字段查询指定字段的值
        /// </summary>
        /// <param name="source">数据源对象</param>
        /// <param name="fields">指定的字段字符</param>
        /// <typeparam name="TSource">源数据类型</typeparam>
        ///  <returns>塑形后的数据字段集合</returns>
        public static IEnumerable<ExpandoObject> ShapeData<TSource> (this IEnumerable<TSource> source, string fields) {
            if (source == null) throw new ArgumentNullException (nameof (source));

            var expandoObjectList = new List<ExpandoObject> (source.Count ());

            var propertyInfoList = new List<PropertyInfo> ();
            //取出全部属性
            if (string.IsNullOrWhiteSpace (fields)) {
                var propertyInfos = typeof (TSource).GetProperties (BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                propertyInfoList.AddRange (propertyInfos);
            } else {
                //取出指定属性
                var fieldsAfterSplit = fields.Split (',');
                foreach (var field in fieldsAfterSplit) {
                    var propertyName = field.Trim ();
                    var propertyInfo = typeof (TSource).GetProperty (propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                    if (propertyInfo == null) throw new Exception ($"Property:{propertyName} 没有找到: {typeof(TSource)}");
                    propertyInfoList.Add (propertyInfo);
                }
            }
            //循环根据属性获取对象数据
            foreach (TSource obj in source) {
                var shapedObj = new ExpandoObject ();
                foreach (var propertyInfo in propertyInfoList) {
                    var propertyValue = propertyInfo.GetValue (obj);
                    ((IDictionary<string, object>) shapedObj).Add (propertyInfo.Name, propertyValue);
                }
                expandoObjectList.Add (shapedObj);
            }
            return expandoObjectList;
        }
    }
}
