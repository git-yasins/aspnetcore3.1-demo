using System;
/// <summary>
/// 外观模式
/// 为子系统的一组接口提供一个一致的界面
/// 定义一个高层接口，使得子系统更加容易使用
/// </summary>
namespace Facade {
    class Program {
        static void Main (string[] args) {
            Facade facade = new Facade ();
            facade.MethodB ();
            facade.MethodA ();
            Console.Read();
        }
    }
    /// <summary>
    /// 外观类 
    /// 提供执行一系列其他类的操作行为的方法
    /// </summary>
    class Facade {
        OperationA a;
        OperationB b;
        OperationC c;
        public Facade () {
            a = new OperationA ();
            b = new OperationB ();
            c = new OperationC ();
        }

        public void MethodA () {
            a.MethodA ();
            b.MethodB ();
        }
        public void MethodB () {
            b.MethodB ();
            c.MethodC ();
        }
    }

    #region 子系统类
    public class OperationA {
        public void MethodA () {
            System.Console.WriteLine ("MethodA");
        }
    }
    public class OperationB {
        public void MethodB () {
            System.Console.WriteLine ("MethodB");
        }
    }
    public class OperationC {
        public void MethodC () {
            System.Console.WriteLine ("MethodC");
        }
    }
    #endregion
}