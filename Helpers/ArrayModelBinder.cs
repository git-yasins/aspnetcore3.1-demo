using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace aspnetcore3_demo.Helpers {
    public class ArrayModelBinder : IModelBinder {
        public Task BindModelAsync (ModelBindingContext bindingContext) {
            if (!bindingContext.ModelMetadata.IsEnumerableType) {
                //绑定参数对象不为集合
                bindingContext.Result = ModelBindingResult.Failed ();
                return Task.CompletedTask;
            }
            var value = bindingContext.ValueProvider.GetValue (bindingContext.ModelName).ToString ();

            if (string.IsNullOrWhiteSpace (value)) {
                bindingContext.Result = ModelBindingResult.Success (null);
                return Task.CompletedTask;
            }

            var elementType = bindingContext.ModelType.GetTypeInfo ().GenericTypeArguments[0];
            var converter = TypeDescriptor.GetConverter (elementType);

            var values = value.Split (new [] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select (x => converter.ConvertFromString (x.Trim ())).ToArray ();

            var typedValues = Array.CreateInstance (elementType, value.Length);
            values.CopyTo (typedValues, 0);
            bindingContext.Model = typedValues;

            bindingContext.Result = ModelBindingResult.Success (bindingContext.Model);
            return Task.CompletedTask;
        }
    }
}
