using System;
/// <summary>
/// 单例模式
/// </summary>
namespace Singleton {
    class Program {
        static void Main (string[] args) {
            Singleton.GetInstance ().Test ();
            Singleton2.GetInstance().Test2();
        }
    }
    /// <summary>
    /// 懒汉式单例
    /// </summary>
    class Singleton {
        static Singleton instance;
        static readonly object synObj = new object ();
        private Singleton () { }
        public static Singleton GetInstance () {
            if (instance == null) {
                lock (synObj) {
                    if (instance == null) {
                        instance = new Singleton ();
                    }
                }
            }
            return instance;
        }

        public void Test () {
            System.Console.WriteLine ("test success");
        }
    }
    /// <summary>
    /// 饿汉式单例
    /// </summary>
    sealed class Singleton2 {

        /// <summary>
        /// 依赖.NET运行库初始化
        /// 静态初始化在自己被加载时就将自己实例化
        /// </summary>
        /// <returns></returns>
        static readonly Singleton2 instance = new Singleton2 ();
        private Singleton2 () { }
        public static Singleton2 GetInstance () {
            return instance;
        }

        public void Test2 () {
            System.Console.WriteLine ("test2 success");
        }
    }
}