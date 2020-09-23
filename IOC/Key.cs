using System;

namespace IOC
{
    internal class Key : IEquatable<Key> {
        public ServiceRegistry Registery { get; }
        public Type[] GenericArgument { get; }
        public Key (ServiceRegistry registry, Type[] genericArguments) {
            Registery = registry;
            GenericArgument = genericArguments;
        }
        public bool Equals (Key other) {
            if (Registery != other.Registery) {
                return false;
            }
            if (GenericArgument.Length != other.GenericArgument.Length) {
                return false;
            }
            for (int index = 0; index < GenericArgument.Length; index++) {
                if (GenericArgument[index] != other.GenericArgument[index]) {
                    return false;
                }
            }
            return true;
        }
        public override int GetHashCode () {
            var hashCode = Registery.GetHashCode ();
            for (int index = 0; index < GenericArgument.Length; index++) {
                hashCode ^= GenericArgument[index].GetHashCode ();
            }
            return hashCode;
        }
        public override bool Equals (object obj) => obj is Key key ? Equals (key) : false;
    }
}