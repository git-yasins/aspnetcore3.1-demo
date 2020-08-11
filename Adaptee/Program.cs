using System;
/// <summary>
/// 适配器模式 
/// 将一个类的接口转换成客户希望的另外一个接口
/// 使得原本不兼容的接口可以兼容
/// </summary>
namespace Adaptee {
    class Program {
        static void Main (string[] args) {
            Player play = new Fowards ();
            play.Attack ();
            play.Defense ();
            
            Player foreginPlay = new TranslatorAdapte ();
            foreginPlay.Attack ();
            foreginPlay.Defense ();
        }
    }
    /// <summary>
    /// 球员抽象
    /// </summary>
    public abstract class Player {
        public abstract void Attack ();
        public abstract void Defense ();
    }

    /// <summary>
    /// 前锋
    /// </summary>
    public class Fowards : Player {
        public override void Attack () {
            System.Console.WriteLine ("前锋进攻");
        }

        public override void Defense () {
            System.Console.WriteLine ("前锋防守");
        }
    }
    /// <summary>
    /// 外籍中锋，需要适配
    /// </summary>
    public class ForeginCenter {

        public void JinGong () {
            System.Console.WriteLine ("外籍中锋进攻");
        }
        public void FanShou () {
            System.Console.WriteLine ("外籍中锋防守");
        }
    }
    /// <summary>
    /// 翻译适配器
    /// </summary>
    public class TranslatorAdapte : Player {
        ForeginCenter foregin = new ForeginCenter ();
        public override void Attack () {
            foregin.JinGong ();
        }

        public override void Defense () {
            foregin.FanShou ();
        }
    }
}