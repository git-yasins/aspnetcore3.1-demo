using System;
using System.Collections.Generic;

namespace Composite {
    /// <summary>
    /// 组合模式 
    /// 将对象组合成树形结构以表示“部分-整体”的层次结构
    /// 组合模式使用户对单个对象和组合对象的使用具有一致性
    /// </summary>
    class Program {
        static void Main (string[] args) {
            ConcreteCompany root = new ConcreteCompany ("深圳总公司");
            root.Add (new HRDepartment ("深圳总公司人力资源部"));
            root.Add (new FinanceDepartment ("总公司财务部"));

            ConcreteCompany comp = new ConcreteCompany("湖南分公司");
            comp.Add(new HRDepartment("湖南分公司人力资源部"));
            comp.Add (new FinanceDepartment ("湖南分公司财务部"));
            root.Add(comp);

            ConcreteCompany comp1 = new ConcreteCompany("上海分公司");
            comp1.Add(new HRDepartment("上海分公司人力资源部"));
            comp1.Add (new FinanceDepartment ("上海分公司财务部"));
            root.Add(comp1);

            System.Console.WriteLine("结构图：");
            root.Display(1);

            System.Console.WriteLine("职责：");
            root.LineOfDuty();
        }
    }
    /// <summary>
    /// Component 
    /// 公司类
    /// </summary>
    abstract class Company {
        protected string name;
        public Company (string name) {
            this.name = name;
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="company">公司</param>
        public abstract void Add (Company company);
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="company"></param>
        public abstract void Remove (Company company);
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="depth">层级</param>
        public abstract void Display (int depth);
        /// <summary>
        /// 履行职责
        /// </summary>
        public abstract void LineOfDuty ();
    }

    #region 树枝节点
    /// <summary>
    /// 具体公司
    /// </summary>
    class ConcreteCompany : Company {
        private List<Company> children = new List<Company> ();
        public ConcreteCompany (string name) : base (name) {

        }
        public override void Add (Company company) {
            children.Add (company);
        }

        public override void Display (int depth) {
            System.Console.WriteLine (new string ('-', depth) + name);
            foreach (Company component in children) {
                component.Display (depth + 2);
            }
        }

        public override void LineOfDuty () {
            foreach (Company component in children) {
                component.LineOfDuty ();
            }
        }

        public override void Remove (Company company) {
            children.Remove (company);
        }
    }
    #endregion

    #region 树叶节点
    /// <summary>
    /// 人力资源部
    /// </summary>
    class HRDepartment : Company {
        public HRDepartment (string name) : base (name) { }
        public override void Add (Company company) {

        }

        public override void Display (int depth) {
            System.Console.WriteLine (new string ('-', depth) + name);
        }

        public override void LineOfDuty () {
            System.Console.WriteLine ($"{name} 员工招聘培训管理");
        }

        public override void Remove (Company company) {

        }
    }
    /// <summary>
    /// 财务部
    /// </summary>
    class FinanceDepartment : Company {
        public FinanceDepartment (string name) : base (name) { }
        public override void Add (Company company) {

        }

        public override void Display (int depth) {
            System.Console.WriteLine (new string ('-', depth) + name);
        }

        public override void LineOfDuty () {
            System.Console.WriteLine ($"{name} 公司财务收支管理");
        }

        public override void Remove (Company company) {

        }
    }
    #endregion

}