using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using aspnetcore3_demo.Services;

namespace aspnetcore3_demo.Helpers {
    public static class IQueryableExtensions {
        public static IQueryable<T> ApplySort<T> (this IQueryable<T> source, string orderBy, Dictionary<string, PropertyMappingValue> mappingDictionary) {
            if (source == null) {
                throw new ArgumentNullException (nameof (source));
            }

            if (mappingDictionary == null) {
                throw new ArgumentNullException (nameof (mappingDictionary));
            }

            if (string.IsNullOrWhiteSpace (orderBy)) {
                return source;
            }
            //order by dtoProperty desc,dtoProperty2
            var orderByAfterSplit = orderBy.Split (',');
            foreach (var orderByCaluse in orderByAfterSplit.Reverse ()) {
                var trimmedOrderByClause = orderByCaluse.Trim ();
                var orderDesending = trimmedOrderByClause.EndsWith (" desc");
                var indexOfFristSpace = trimmedOrderByClause.IndexOf (" ");
                var propertyName = indexOfFristSpace == -1 ? trimmedOrderByClause : trimmedOrderByClause.Remove (indexOfFristSpace);

                if (!mappingDictionary.ContainsKey (propertyName)) {
                    throw new ArgumentNullException ($"没有找到key为{propertyName}的映射");
                }

                var PropertyMappingValue = mappingDictionary[propertyName];
                if (PropertyMappingValue == null) {
                    throw new ArgumentNullException (nameof (PropertyMappingValue));
                }

                foreach (var destinationProperty in PropertyMappingValue.DestinationProperties) {
                    if (PropertyMappingValue.Revert) {
                        orderDesending = !orderDesending;
                    }

                    source = source.OrderBy (destinationProperty + (orderDesending ? " descending" : " ascending"));
                }
            }
            return source;
        }
    }
}
