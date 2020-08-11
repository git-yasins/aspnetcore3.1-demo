using System;
using System.Collections.Generic;
/// <summary>
/// 访问者模式 
/// 表示一个作用于某对象结构中的各元素的操作
/// 它使你可以在不改变各元素的类的前提下定义作用于这些元素的新操作
/// </summary>
namespace Visitor {
    class Program {
        static void Main (string[] args) {
            ObjectStructure o = new ObjectStructure();
            o.Attach(new Man());
            o.Attach(new WoMan());

            Success v1 = new Success();
            o.Display(v1);

            Faliing v2 = new Faliing();
            o.Display(v2);

            Amativeness v3 = new Amativeness();
            o.Display(v3);
        }
    }
    #region 人（Element）
    abstract class Person {
        public abstract void Accept (Action visitor);
    }

    class Man : Person {
        public override void Accept (Action visitor) {
            visitor.GetManConclusion (this);
        }
    }

    class WoMan : Person {
        public override void Accept (Action visitor) {
            visitor.GetWoManConclusion (this);
        }
    }
    #endregion
    #region 对象结构 
    class ObjectStructure {
        IList<Person> elements = new List<Person> ();
        public void Attach (Person element) {
            elements.Add (element);
        }
        public void Detach (Person element) {
            elements.Remove (element);
        }
        /// <summary>
        /// 显示查看
        /// </summary>
        /// <param name="visitor"></param>
        public void Display (Action visitor) {
            foreach (Person e in elements) {
                e.Accept (visitor);
            }
        }
    }
    #endregion
    #region 状态 (访问者) visitor
    /// <summary>
    /// 状态抽象
    /// </summary>
    abstract class Action {
        public abstract void GetManConclusion (Man concreteElementA);
        public abstract void GetWoManConclusion (WoMan concreteElementB);
    }
    /// <summary>
    /// 成功
    /// </summary>
    class Success : Action {
        public override void GetManConclusion (Man concreteElementA) {
            System.Console.WriteLine ($"{concreteElementA.GetType().Name} {this.GetType().Name}时，背后多半有一个伟大的女人");
        }

        public override void GetWoManConclusion (WoMan concreteElementB) {
            System.Console.WriteLine ($"{concreteElementB.GetType().Name} {this.GetType().Name}时，背后多半有一个不成功的男人");
        }
    }
    /// <summary>
    /// 失败
    /// </summary>
    class Faliing : Action {
        public override void GetManConclusion (Man concreteElementA) {
            System.Console.WriteLine ($"{concreteElementA.GetType().Name} {this.GetType().Name}时，闷头喝酒，谁也不用劝");
        }

        public override void GetWoManConclusion (WoMan concreteElementB) {
            System.Console.WriteLine ($"{concreteElementB.GetType().Name} {this.GetType().Name}时，眼泪汪汪，谁也劝不了");
        }
    }
    /// <summary>
    /// 恋爱
    /// </summary>
    class Amativeness : Action {
        public override void GetManConclusion (Man concreteElementA) {
            System.Console.WriteLine ($"{concreteElementA.GetType().Name} {this.GetType().Name}时，凡事不懂也要装懂");
        }

        public override void GetWoManConclusion (WoMan concreteElementB) {
            System.Console.WriteLine ($"{concreteElementB.GetType().Name} {this.GetType().Name}时，遇事懂也装作不懂");
        }
    }
    #endregion
}