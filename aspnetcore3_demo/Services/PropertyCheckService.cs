using System.Reflection;

namespace aspnetcore3_demo.Services {
    public class PropertyCheckService : IPropertyCheckService
    {
        public bool TypeHasProperties<T>(string fields)
        {
            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }
            var fieldsAfterSplit = fields.Split(',');
            foreach (var field in fieldsAfterSplit)
            {
                var propertyName = field.Trim();
                var propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                if (propertyInfo == null)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
