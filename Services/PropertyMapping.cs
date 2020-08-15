using System;
using System.Collections.Generic;
namespace aspnetcore3_demo.Services {
    public class PropertyMapping<TSource, TDestination> : IPropertyMapping {
        public Dictionary<string, PropertyMappingValue> MappingDictionary { get; private set; }
        public PropertyMapping (Dictionary<string, PropertyMappingValue> mappingDictionary) {
            MappingDictionary = mappingDictionary??throw new ArgumentNullException (nameof (mappingDictionary));
        }
    }
}