using System;
/// <summary>
/// 原型模式 浅|深复制
/// </summary>
namespace Prototype {
    class Program {
        static void Main (string[] args) {
            Resume resume = new Resume ("张三", 22);
            resume.Works.Company = "腾讯";
            resume.Works.WorkYear = 1;
            resume.Works.WorkDate = "1990";

            Resume resume2 = (Resume) resume.Clone ();
            resume2.Age = 24;
            resume2.Works.Company = "华为";
            resume2.Works.WorkYear = 3;
            resume2.Works.WorkDate = "1992";

            Resume resume3 = (Resume) resume.Clone ();
            resume3.Age = 27;
            resume3.Works.Company = "百度";
            resume3.Works.WorkYear = 5;
            resume3.Works.WorkDate = "1995";

            resume.Display ();
            resume2.Display ();
            resume3.Display ();
            Console.Read ();
        }
    }
    /// <summary>
    /// 简历类
    /// </summary>
    public class Resume : ICloneable {
        public int Age { get; set; }
        public string Name { get; set; }
        public Work Works { get; set; }
        public Resume (string name, int Age) {
            this.Name = name;
            this.Age = Age;
            Works = new Work ();
        }
        //私有构造进行深复制
        private Resume (Work work) {
            this.Works = (Work) work.Clone ();
        }
        /// <summary>
        /// 打印
        /// </summary>
        public void Display () {
            Console.WriteLine ($"Name:{this.Name},Age:{this.Age}");
            Console.WriteLine ($"works[company:{this.Works.Company},year:{this.Works.WorkYear},date:{this.Works.WorkDate}]");
        }

        //深克隆
        public object Clone () {
            Resume obj = new Resume (this.Works);
            obj.Name = this.Name;
            this.Age = this.Age;
            return obj;
        }
    }

    /// <summary>
    /// 工作类
    /// </summary>
    public class Work : ICloneable {
        public string WorkDate { get; set; }
        public int WorkYear { get; set; }
        public string Company { get; set; }
        public object Clone () {
            return (object) this.MemberwiseClone ();
        }
    }
}