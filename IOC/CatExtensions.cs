using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IOC {
    /// <summary>
    /// Cat容器扩展方法
    /// </summary>
    public static class CatExtensions {
        public static Cat Register (this Cat cat, Type from, Type to, Lifetime lifetime) {
            Func<Cat, Type[], object> factory = (_cat, arguments) => Create (_cat, to, arguments);
            cat.Register (new ServiceRegistry (from, lifetime, factory));
            return cat;
        }

        public static Cat Register<TFrom, TTo> (this Cat cat, Lifetime lifetime)
        where TTo : TFrom => cat.Register (typeof (TFrom), typeof (TTo), lifetime);

        public static Cat Register<TService> (this Cat cat, Func<Cat, TService> factory, Lifetime lifetime) {
            cat.Register (new ServiceRegistry (typeof (TService), lifetime, (_cat, arguments) => factory (_cat)));
            return cat;
        }

        public static Cat Register (this Cat cat, Assembly assembly) {
            var typedAttributes = from type in assembly.GetExportedTypes ()
            let attribute = type.GetCustomAttribute<MapToAttribute> ()
            where attribute != null
            select new {
                ServiceType = type, Attribute = attribute
            };
            foreach (var typeAttribute in typedAttributes) {
                cat.Register (typeAttribute.Attribute.ServiceType, typeAttribute.ServiceType, typeAttribute.Attribute.Lifetime);
            }
            return cat;
        }

        public static T GetService<T> (this Cat cat) => (T) cat.GetService (typeof (T));

        /// <summary>
        /// 创建子容器
        /// </summary>
        /// <param name="cat"></param>
        /// <returns></returns>
        public static Cat CreateChild (this Cat cat) => new Cat (cat);

        private static object Create (Cat cat, Type type, Type[] genericArguments) {
            if (genericArguments.Length > 0) {
                type = type.MakeGenericType (genericArguments);
            }
            var constructors = type.GetConstructors ();
            if (constructors.Length == 0) {
                throw new InvalidOperationException ($"Cannot create the instance of {type} which does not have a public constructor.");
            }

            var constructor = constructors.FirstOrDefault (x => x.GetCustomAttributes (false).OfType<InjectionAttribute> ().Any ());
            constructor ??= constructors.First ();
            var parameters = constructor.GetParameters ();
            if (parameters.Length == 0) {
                return Activator.CreateInstance (type);
            }
            var arguments = new object[parameters.Length];
            for (int index = 0; index < arguments.Length; index++) {
                arguments[index] = cat.GetService (parameters[index].ParameterType);
            }
            return constructor.Invoke (arguments);
        }
        public static Cat Register (this Cat cat, Type serviceType, object instance) {
            Func<Cat, Type[], object> factory = (_cat, arguments) => instance;
            cat.Register (new ServiceRegistry (serviceType, Lifetime.Root, factory));
            return cat;
        }

        public static Cat Register<TService> (this Cat cat, TService instance) {
            Func<Cat, Type[], object> factory = (_cat, arguments) => instance;
            cat.Register (new ServiceRegistry (typeof (TService), Lifetime.Root, factory));
            return cat;
        }

        public static Cat Register (this Cat cat, Type serviceType, Func<Cat, object> factory, Lifetime lifetime) {
            cat.Register (new ServiceRegistry (serviceType, lifetime, (_cat, arguments) => factory (_cat)));
            return cat;
        }
        public static IEnumerable<T> GetServices<T> (this Cat cat) => cat.GetService<IEnumerable<T>> ();
    }
}