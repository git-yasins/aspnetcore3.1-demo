using System;
/// <summary>
/// 观察者模式（发布-订阅模式 类似委托事件）
/// 定义一种一对多的依赖关系
/// 让多个观察者对象监视同一个主题对象
/// 当主题发生改变时通知观察者对象，使观察者能自动更新自己
/// </summary>
namespace Observer {
    class Program {
        static void Main (string[] args) {
            ObserverEvent e = new ObserverEvent ();
            e.SubjectState = "TOM猫来了";

            Tom cat = new Tom ();
            Jerry jerry = new Jerry ("JERRY");
            cat.Update += jerry.Message;
            cat.Notify (e);
        }
    }
    /// <summary>
    /// 通知发布接口
    /// </summary>
    interface ISubject {
        void Notify (ObserverEvent e);
    }

    /// <summary>
    /// 发布|通知
    /// </summary>
    class Tom : ISubject {
        public delegate void EventSubjectHandler (ObserverEvent e);
        public event EventSubjectHandler Update;
        public void Notify (ObserverEvent e) {
            if (Update != null)
                Update (e);
        }
    }
    /// <summary>
    /// 事件数据
    /// </summary>
    class ObserverEvent : EventArgs {
        public string SubjectState { get; set; }
    }
    /// <summary>
    /// 订阅|观察
    /// </summary>
    class Jerry {
        string _name;
        public Jerry (string name) {
            this._name = name;
        }
        public void Message (ObserverEvent e) {
            System.Console.WriteLine ($"{e.SubjectState},{_name}快跑");
        }
    }
}