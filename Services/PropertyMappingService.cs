using System;
using System.Collections.Generic;
using System.Linq;
using aspnetcore3_demo.Entities;
using aspnetcore3_demo.Models;

namespace aspnetcore3_demo.Services {
    /// <summary>
    /// 属性映射服务
    /// </summary>
    public class PropertyMappingService : IPropertyMappingService {
        /// <summary>
        /// 将EmployeeDto 映射到 Employee对应字段
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, PropertyMappingValue> _employeePropertyMapping = new Dictionary<string, PropertyMappingValue> (StringComparer.OrdinalIgnoreCase)
             { { "Id", new PropertyMappingValue (new List<string> { "Id" }) },
             { "CompanyId", new PropertyMappingValue (new List<string> { "CompanyId" }) },
             { "EmployeeNo", new PropertyMappingValue (new List<string> { "EmployeeNo" }) },
             { "Name", new PropertyMappingValue (new List<string> { "FirstName", "LastName" }) },
             { "GenderDisplay", new PropertyMappingValue (new List<string> { "EmployeeNo" }) },
             { "Age", new PropertyMappingValue (new List<string> { "DateOfBirth" }, true) },
        };

        /// <summary>
        /// 公司DTO映射公司实体属性
        /// /// </summary>
        /// <returns></returns>
        private Dictionary<string, PropertyMappingValue> _companyPropertyMapping = new Dictionary<string, PropertyMappingValue> (StringComparer.OrdinalIgnoreCase)
        {
            { "Id", new PropertyMappingValue (new List<string> { "Id" }) },
            { "CompanyName", new PropertyMappingValue (new List<string> { "Name" }) },
            { "Country", new PropertyMappingValue (new List<string> { "Country" }) },
            { "Industry", new PropertyMappingValue (new List<string> { "Industry" }) },
            { "Product", new PropertyMappingValue (new List<string> { "Product" }) },
            { "Introduction", new PropertyMappingValue (new List<string> { "Introduction" }) },
            { "BankrupTime", new PropertyMappingValue (new List<string> { "BankrupTime" }) }
        };

        private IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping> ();
        public PropertyMappingService () {
            _propertyMappings.Add (new PropertyMapping<EmployeeDto, Employee> (_employeePropertyMapping));
            _propertyMappings.Add (new PropertyMapping<CompanyDto, Company> (_companyPropertyMapping));
        }

        /// <summary>
        /// 获取属性的键值对象
        /// </summary>
        /// <typeparam name="TSource">源</typeparam>
        /// <typeparam name="TDestination">目标对象</typeparam>
        /// <returns></returns>
        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination> () {

            var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>> ();
            var propertyMappings = matchingMapping.ToList ();

            if (propertyMappings.Count == 1) {
                return propertyMappings.First ().MappingDictionary;
            }
            throw new Exception ($"无法找到唯一的映射关系:{typeof(TSource)},{typeof(TDestination)}");
        }

        /// <summary>
        /// 校验排序字段是否正确
        /// </summary>
        /// <param name="fields">排序字符串</param>
        /// <typeparam name="TSource">源排序对象</typeparam>
        /// <typeparam name="TDstination">目标对象</typeparam>
        /// <returns></returns>
        public bool ValidationMappingExistesFor<TSource,TDstination>(string fields){

            var propertyMapping = GetPropertyMapping<TSource,TDstination>();

            if(string.IsNullOrWhiteSpace(fields)){
                return true;
            }

            var fieldsAfterSplit = fields.Split(",");

            foreach(var field in fieldsAfterSplit){
                var trimmedField = field.Trim();
                var indexOfFirstSpace = trimmedField.IndexOf(" ",StringComparison.Ordinal);

                //如果带 空格的排序字段 移除空格后面的字符,取前面的属性名
                var propertyName = indexOfFirstSpace == -1 ? trimmedField:trimmedField.Remove(indexOfFirstSpace);
                if(!propertyMapping.ContainsKey(propertyName)){
                    return false;
                }
            }
            return true;
        }
    }
}
