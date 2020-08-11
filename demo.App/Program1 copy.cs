using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using demo.Domain;
namespace demo.App {
    /// <summary>
    /// 演练Linq to object 查询原理
    /// </summary>
    class Program1 {
        static void Main1 (string[] args) {
            List<student> students = new List<student> ();
            students.AddRange (new student[] {
                new student { ID = 1, Name = "aa" }, new student { ID = 2, Name = "bb" }
            });
            student s = students.Find (c => c.Name == "aa");
            System.Console.WriteLine (s.ID.ToString ());

            var s2 = students.Where1 (c => c.Name.Contains ("b"));
            foreach (var b in s2) {
                System.Console.WriteLine (b.Name);
            }

            var result = from res in students where res.Name == "aa"
            select res;
            foreach (var a in result) {
                System.Console.WriteLine (a.Name);
            }

            MyList<string> m = new MyList<string> ();
            m += "aaa";
            m += "bbb";
            m += "ccc";
            m.Insert ("bbb");
            m.Insert ("21222");
            m.Insert ("1333");
            m.Insert ("444");
            MyList<string> r = m.FindAll ((x) => { return x.Contains ("1"); });
            System.Console.WriteLine (m.ToString ());
            System.Console.WriteLine (r.ToString ());
        }
    }

    class student {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    static class MyLinq {
        public static IEnumerable<TSource> Where1<TSource> (this IEnumerable<TSource> sources, Func<TSource, bool> predicate) {
            foreach (var item in sources) {
                if (predicate (item)) {
                    yield return item;
                }
            }
        }
        public static void Insert (this MyList<string> source, string value) {
            source += value;
        }
    }
    /// <summary>
    /// 模拟系统List<T>().FindAll()方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class MyList<T> {
        private List<T> list = new List<T> ();
        public static MyList<T> operator + (MyList<T> a, T b) {
            a.list.Add (b);
            return a;
        }

        public override string ToString () {
            StringBuilder builder = new StringBuilder ();
            foreach (T a in list) {
                builder.Append (a.ToString ());
                builder.Append (",");
            }
            string ret = builder.Remove (builder.Length - 1, 1).ToString ();
            return ret;
        }

        public MyList<T> FindAll (Predicate<T> act) {
            MyList<T> t2 = new MyList<T> ();
            foreach (T i in list) {
                if (act (i)) {
                    t2.list.Add (i);
                }
            }
            return t2;
        }
    }
}