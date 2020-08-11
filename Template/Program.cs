using System;
/// <summary>
/// 模板方法模式
/// 定义一个操作中的算法的骨架，将一些步骤延迟到子类中
/// 模板方法使得子类可以不改变一个算法的结构即可重定义该算法的某些特定步骤
/// </summary>
namespace Template {
    class Program {
        static void Main (string[] args) {
            AbtractClass c = new AbtractClassA ();
            c.TemplateMethod ();
            Console.Read ();
        }
    }

    abstract class AbtractClass {
        public abstract void Operation ();
        /// <summary>
        /// 抽象类的模板方法中调用子类要实现的抽象方法，子类实例对象调用该模板方法，实现统一程序模板
        /// </summary>
        public void TemplateMethod () {
            Operation ();
            System.Console.WriteLine ("template abtractClass method test");
        }
    }

    class AbtractClassA : AbtractClass {
        public override void Operation () {
            System.Console.WriteLine ("child methold test");
        }
    }
}