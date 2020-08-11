using System;
/// <summary>
/// 桥接模式
/// 将抽象部分与它的实现部分分离，使它们都可能独立地变化
/// 并非让抽象和派生分享，实现指的是抽象类和它的派生类来实现自己的对象
/// ps:手机品牌和软件分开分类，而不是具体手机和具体软件捆绑
/// 手机品牌关联抽象软件
/// </summary>
namespace Bridge {
    class Program {
        static void Main (string[] args) {
            Abstraction abs = new RefindAbstraction ();
            abs.SetImplementor (new ConcreteImplementorA ());
            abs.Operation ();

            abs.SetImplementor (new ConcreteImplementorB ());
            abs.Operation ();
        }
    }

    abstract class Implementor {
        public abstract void Operation ();
    }
    class ConcreteImplementorA : Implementor {
        public override void Operation () {
            System.Console.WriteLine ("A");
        }
    }
    class ConcreteImplementorB : Implementor {
        public override void Operation () {
            System.Console.WriteLine ("B");
        }
    }

    class Abstraction {
        protected Implementor implementor;
        public void SetImplementor (Implementor implementor) {
            this.implementor = implementor;
        }
        public virtual void Operation () {
            implementor.Operation ();
        }
    }

    class RefindAbstraction : Abstraction {
        public override void Operation () {
            implementor.Operation ();
        }
    }
}