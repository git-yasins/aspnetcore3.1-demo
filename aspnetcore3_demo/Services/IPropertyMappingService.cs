using System.Collections.Generic;

namespace aspnetcore3_demo.Services {
    /// <summary>
    /// 对象属性映射
    /// </summary>
    public interface IPropertyMappingService {
         /// <summary>
        /// 获取属性的键值对象
        /// </summary>
        /// <typeparam name="TSource">源</typeparam>
        /// <typeparam name="TDestination">目标对象</typeparam>
        /// <returns></returns>
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination> ();
        /// <summary>
        /// 校验排序字段是否正确
        /// </summary>
        /// <param name="fields">排序字符串</param>
        /// <typeparam name="TSource">源排序对象</typeparam>
        /// <typeparam name="TDstination">目标对象</typeparam>
        /// <returns></returns>
        bool ValidationMappingExistesFor<TSource, TDstination> (string fields);
    }
}
