using System;
using System.Collections.Generic;

namespace aspnetcore3_demo.Services
{
    /// <summary>
    /// 排序的属性和值映射
    /// </summary>
    public class PropertyMappingValue {
        /// <summary>
        /// 排序的映射属性
        /// </summary>
        /// <value></value>
        public IEnumerable<string> DestinationProperties { get; set; }
        /// <summary>
        /// 反序
        /// </summary>
        /// <value></value>
        public bool Revert { get; set; }
        /// <summary>
        /// 初始化属性键值映射
        /// </summary>
        /// <param name="destinationProperties">映射列表</param>
        /// <param name="revert">默认不反序</param>
        public PropertyMappingValue (IEnumerable<string> destinationProperties, bool revert = false) {
            this.DestinationProperties = destinationProperties ??
                throw new ArgumentNullException (nameof (destinationProperties));
            this.Revert = revert;
        }
    }
}
