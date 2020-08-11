using System;
/// <summary>
/// 代理模式
/// </summary>
namespace proxy {
    class Program {
        static void Main (string[] args) {
            Porxy proxy = new Porxy ();
            proxy.Gift ();
        }
    }

    /// <summary>
    /// 代理接口
    /// </summary>
    public interface IProxy {
        void Gift ();
    }

    /// <summary>
    /// 代理类
    /// </summary>
    public class Porxy : IProxy {
        Pursirt _pursirt = null;
        public void Gift () {
            _pursirt = new Pursirt ();
            _pursirt.Gift ();
        }
    }
    /// <summary>
    /// 实际请求类
    /// </summary>
    public class Pursirt : IProxy {
        public void Gift () {
            System.Console.WriteLine ("gift");
        }
    }
}