using System;
/// <summary>
/// 装饰模式
/// 动态地给一个对象添加一些额外的职能
/// 就增加功能来说，比生成子类更为灵活
/// </summary>
namespace Decorate {
    class Program {
        static void Main (string[] args) {
            Person person = new Person ();
            Shirt shirt = new Shirt ();
            Shoes shoes = new Shoes ();
            Pants pants = new Pants ();
            Jacket jacket = new Jacket ();
            //装饰过程
            shirt.SetDecorate (person);
            pants.SetDecorate (shirt);
            shoes.SetDecorate (pants);
            jacket.SetDecorate (shoes);
            jacket.Show ();
            Console.Read ();
        }
    }

    /// <summary>
    /// 要被装饰的对象
    /// </summary>
    public class Person {
        public virtual void Show () { }
    }
    /// <summary>
    /// 装饰服装
    /// </summary>
    public class Finery : Person {
        Person _component = null;
        /// <summary>
        /// 装饰步骤
        /// </summary>
        /// <param name="component"></param>
        public void SetDecorate (Person component) {
            _component = component;
        }
        /// <summary>
        /// Finery子类中使用base.Show()实际调用的为SetDecorate方法传入的装饰对象方法
        /// </summary>
        public override void Show () {
            if (_component != null)
                _component.Show ();
        }
    }

    #region 具体要装饰的服饰子类
    /// <summary>
    /// 衬衫
    /// </summary>
    public class Shirt : Finery {
        public override void Show () {
            Console.Write ("衬衫");
            base.Show ();
        }
    }
    /// <summary>
    /// 夹克
    /// </summary>
    public class Jacket : Finery {
        public override void Show () {
            Console.Write ("夹克");
            base.Show ();
        }
    }
    /// <summary>
    /// 裤子
    /// </summary>
    public class Pants : Finery {
        public override void Show () {
            Console.Write ("裤子");
            base.Show ();
        }
    }

    /// <summary>
    /// 鞋子
    /// </summary>
    public class Shoes : Finery {
        public override void Show () {
            Console.Write ("鞋子");
            base.Show ();
        }
    }
    #endregion
}