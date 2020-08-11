using System;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 建造者模式
/// 将一个复杂对象的构建与表示分离
/// 使得同样的构建过程可以创建不同的表示
/// </summary>
namespace Builder {
    class Program {
        static void Main (string[] args) {
            Director d = new Director ();

            Builder b1 = new ProductA ();
            Builder b2 = new ProductB ();

            //执行构建产品部件，增加到产品列表
            d.CreatePart (b1);
            //获取执行后的产品
            Product p1 = b1.GetResult ();
            p1.Show ();

            //构建产品二的部件
            d.CreatePart (b2);
            Product p2 = b2.GetResult ();
            p2.Show ();

            Console.Read ();
        }
    }
    /// <summary>
    /// 指挥者，统一构建部件
    /// </summary>
    class Director {
        public void CreatePart (Builder builder) {
            builder.CreatePartA ();
            builder.CreatePartB ();
        }
    }

    class Product {
        IList<string> list = new List<string> ();
        public void Add (string part) => list.Add (part);
        public void Show () {
            foreach (string item in list)
                System.Console.WriteLine (item);
        }
    }

    abstract class Builder {
        public abstract void CreatePartA ();
        public abstract void CreatePartB ();
        public abstract Product GetResult ();
    }

    class ProductA : Builder {
        Product product = new Product ();
        public override void CreatePartA () => product.Add ("part A");
        public override void CreatePartB () {
            product.Add ("part B");
        }
        public override Product GetResult () { return product; }

    }

    class ProductB : Builder {
        Product product = new Product ();
        public override void CreatePartA () => product.Add ("part C");
        public override void CreatePartB () => product.Add ("part D");

        public override Product GetResult () {
            return product;
        }
    }
}