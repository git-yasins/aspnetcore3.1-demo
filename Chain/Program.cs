using System;
/// <summary>
/// 职责链模式 
/// 使每个对象都有机会处理请求，从而避免请求的发送者和接收之间的耦合关系
/// 将这个对象连成一条链，并沿着这条链传递该请求，直到有一个对象处理它为止
/// </summary>
namespace Chain {
    class Program {
        static void Main (string[] args) {
            CommonManager jinli = new CommonManager ("经理");
            Majordomo zongjian = new Majordomo ("总监");
            GeneralManager zongjingli = new GeneralManager ("总经理");
            //设置上级
            jinli.SetSuperior (zongjian);
            zongjian.SetSuperior (zongjingli);

            Request request = new Request ();
            request.Number = 1;
            request.RequestType = "请假";
            request.RequestContent = "员工请假";
            jinli.RequestApplication (request);

            Request request1 = new Request ();
            request1.Number = 4;
            request1.RequestType = "请假";
            request1.RequestContent = "员工请假";
            jinli.RequestApplication (request1);

            Request request2 = new Request ();
            request2.Number = 500;
            request2.RequestType = "加薪";
            request2.RequestContent = "员工请求加薪";
            jinli.RequestApplication (request2);

            Request request3 = new Request ();
            request3.Number = 1000;
            request3.RequestType = "加薪";
            request3.RequestContent = "员工请求加薪";
            jinli.RequestApplication (request3);
        }
    }
    /// <summary>
    /// 请求类
    /// </summary>
    class Request {
        /// <summary>
        /// 请求类别
        /// </summary>
        /// <value></value>
        public string RequestType { get; set; }
        /// <summary>
        /// 请求内容
        /// </summary>
        /// <value></value>
        public string RequestContent { get; set; }
        /// <summary>
        /// 请求数量
        /// </summary>
        /// <value></value>
        public int Number { get; set; }
    }
    /// <summary>
    /// 管理者
    /// </summary>
    abstract class Manager {
        protected string name;
        /// <summary>
        /// 管理者上级
        /// </summary>
        protected Manager superior;
        public Manager (string name) {
            this.name = name;
        }
        public void SetSuperior (Manager manager) {
            this.superior = manager;
        }
        abstract public void RequestApplication (Request request);
    }
    /// <summary>
    /// 经理
    /// </summary>
    class CommonManager : Manager {
        public CommonManager (string name) : base (name) { }
        public override void RequestApplication (Request request) {
            if (request.Number <= 2 && request.RequestType == "请假") {
                System.Console.WriteLine ($"{name}:{request.RequestContent} 数量{request.Number} 被批准");
            } else {
                //如果当前没有权限并且上级不为空，将请求转给上级
                if (superior != null) {
                    superior.RequestApplication (request);
                }
            }
        }
    }
    /// <summary>
    /// 总监
    /// </summary>
    class Majordomo : Manager {
        public Majordomo (string name) : base (name) { }
        public override void RequestApplication (Request request) {
            if (request.Number <= 5 && request.RequestType == "请假") {
                System.Console.WriteLine ($"{name}:{request.RequestContent} 数量{request.Number} 被批准");
            } else {
                //如果上级不为空，将请求转给上级
                if (superior != null) {
                    superior.RequestApplication (request);
                }
            }
        }
    }
    /// <summary>
    /// 总经理
    /// </summary>
    class GeneralManager : Manager {
        public GeneralManager (string name) : base (name) { }

        public override void RequestApplication (Request request) {
            if (request.RequestType == "请假") {
                System.Console.WriteLine ($"{name}:{request.RequestContent} 数量{request.Number} 被批准");
            } else if (request.RequestType == "加薪" && request.Number <= 500) {
                System.Console.WriteLine ($"{name}:{request.RequestContent} 数量{request.Number} 被批准");
            } else if (request.RequestType == "加薪" && request.Number > 500) {
                System.Console.WriteLine ($"{name}:{request.RequestContent} 数量{request.Number} 不通过");
            }
        }
    }
}