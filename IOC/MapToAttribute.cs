using System;

namespace IOC
{
   /// <summary>
    /// Qux类型生命周期映射特性
    /// </summary>
    [AttributeUsage (AttributeTargets.Class, AllowMultiple = true)]
    public sealed class MapToAttribute : Attribute {
        public Type ServiceType { get; }
        public Lifetime Lifetime { get; }
        public MapToAttribute (Type serviceType, Lifetime lifetime) {
            ServiceType = serviceType;
            Lifetime = lifetime;
        }
    }
}