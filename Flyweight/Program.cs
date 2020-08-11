using System;
using System.Collections;
/// <summary>
/// 享元模式
/// 运用共享技术有效地支持大量细粒度的对象
/// </summary>
namespace Flyweight {
    class Program {
        static void Main (string[] args) {
            WebSiteFactory f = new WebSiteFactory ();

            WebSite f1 = f.GetWebSiteCategory ("产品展示");
            f1.Use (new User ("诸葛亮"));

            WebSite f2 = f.GetWebSiteCategory ("产品展示");
            f2.Use (new User ("曹操"));

            WebSite f3 = f.GetWebSiteCategory ("博客");
            f3.Use (new User ("刘备"));

            WebSite f4 = f.GetWebSiteCategory ("博客");
            f4.Use (new User ("周瑜"));

            System.Console.WriteLine($"网站分类总数为{f.GetWebSiteCount()}");
        }
    }
    /// <summary>
    /// 用户
    /// </summary>
    class User {
        public string Name { get; }
        public User (string name) {
            this.Name = name;
        }
    }
    /// <summary>
    /// 网站抽象
    /// </summary>
    abstract class WebSite {
        public abstract void Use (User user);
    }
    /// <summary>
    /// 具体网站
    /// </summary>
    class ConcreteWebsite : WebSite {
        string _name = string.Empty;
        public ConcreteWebsite (string name) {
            _name = name;
        }
        public override void Use (User user) {
            System.Console.WriteLine ($"网站分类： {_name} 用户：{user.Name}");
        }
    }
    /// <summary>
    /// 网站工厂
    /// </summary>
    class WebSiteFactory {
        Hashtable flyweights = new Hashtable ();
        /// <summary>
        /// 获得网站分类，存在则直接根据KEY获取
        /// </summary>
        /// <param name="key">集合键名</param>
        /// <returns></returns>
        public WebSite GetWebSiteCategory (string key) {
            if (!flyweights.ContainsKey (key)) {
                flyweights.Add (key, new ConcreteWebsite (key));
            }
            return (WebSite) flyweights[key];
        }
        /// <summary>
        /// 获取网站分类总数
        /// </summary>
        /// <returns></returns>
        public int GetWebSiteCount () {
            return flyweights.Count;
        }
    }
}