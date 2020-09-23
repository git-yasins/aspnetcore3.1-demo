using System;

namespace IOC {
    [AttributeUsage (AttributeTargets.Constructor)]
    public class InjectionAttribute : Attribute { }
}