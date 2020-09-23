using System;
using System.Reflection;

namespace IOC {
    class Program {
        static void Main (string[] args) {
            using (var root = new Cat ()
                .Register<IFoo, Foo> (Lifetime.Transient)
                .Register<IBar> (_ => new Bar (), Lifetime.Self)
                .Register<IBaz, Baz> (Lifetime.Root)
                .Register (Assembly.GetEntryAssembly ()) //IQUX 用特性标签注册，根据反射获取实例服务
                .Register (typeof (IFoobar<,>), typeof (Foobar<,>), Lifetime.Transient)) {
                using (var cat = root.CreateChild ()) {
                    cat.GetService<IFoo> ();
                    cat.GetService<IBar> ();
                    cat.GetService<IBaz> ();
                    cat.GetService<IQux> ();
                    cat.GetService<IFoobar<IFoo, IBar>> ();
                    System.Console.WriteLine ("child cat is disposed.");
                }
                System.Console.WriteLine ("Root cat is disposed.");
            }
        }
    }

    public interface IFoo { }
    public interface IBar { }
    public interface IBaz { }
    public interface IQux { }
    public interface IFoobar<T1, T2> { }
    public class Base : IDisposable {
        public Base () => System.Console.WriteLine ($"Instance of {GetType().Name} is created.");
        public void Dispose () => System.Console.WriteLine ($"Instance of {GetType().Name} is disposed.");
    }
    public class Foo : Base, IFoo { }
    public class Bar : Base, IBar { }
    public class Baz : Base, IBaz { }

    [MapTo (typeof (IQux), Lifetime.Root)]
    public class Qux : Base, IQux { }
    public class Foobar<T1, T2> : Base, IFoobar<T1, T2> {

        public IFoo Foo { get; }
        public IBar Bar { get; }
        public Foobar (IFoo foo, IBar bar) {
            Foo = foo;
            Bar = bar;
        }
    }
}