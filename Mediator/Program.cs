using System;
/// <summary>
/// 中介者模式
/// 用一个中介对象来封闭一系列的对象交互
/// 中介者使各对象不需要显式地相互引用，从而使其耦合松散
/// 而且可以独立地改变它们之间的交互
/// </summary>
namespace Mediator {
    class Program {
        static void Main (string[] args) {
            UNSCnitedationsecurityCouncil countcil = new UNSCnitedationsecurityCouncil();
            USA usa = new USA(countcil);
            Iraq iraq = new Iraq(countcil);

            countcil.colleague1 = usa;
            countcil.colleague2=iraq;

            usa.Declare("不准研制核武器");
            iraq.Declare("不怕侵略");
        }
    }
    /// <summary>
    /// 联合国机构
    /// </summary>
    abstract class UnitedNations {
        public abstract void Declare (string name, Country colleague);
    }
    /// <summary>
    /// 安理会
    /// </summary>
    class UNSCnitedationsecurityCouncil : UnitedNations {
        public USA colleague1 { get; set; }
        public Iraq colleague2 { get; set; }
        public override void Declare (string message, Country colleague) {
            if (colleague == colleague1) {
                colleague2.GetMessage (message);
            } else {
                colleague1.GetMessage (message);
            }
        }
    }

    /// <summary>
    /// 国家
    /// </summary>
    abstract class Country {
        protected UnitedNations mediator;
        public Country (UnitedNations unitedNations) {
            this.mediator = unitedNations;
        }
    }
    /// <summary>
    /// 美国
    /// </summary>
    class USA : Country {
        public USA (UnitedNations unitedNations) : base (unitedNations) { }
        public void Declare (string message) {
            mediator.Declare (message, this);
        }

        public void GetMessage (string message) {
            System.Console.WriteLine ($"美国获得对方消息：{message}");
        }
    }

    /// <summary>
    /// 伊拉克
    /// </summary>
    class Iraq : Country {
        public Iraq (UnitedNations unitedNations) : base (unitedNations) { }
        public void Declare (string message) {
            mediator.Declare (message, this);
        }

        public void GetMessage (string message) {
            System.Console.WriteLine ($"伊拉克获得对方消息：{message}");
        }
    }

}