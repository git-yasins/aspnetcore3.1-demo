using System;
using System.Collections.Generic;
/// <summary>
/// 命令模式 
/// 将一个请求封闭为一个对象
/// 使你可用不同的请求对客户进行参数化
/// 对请求排除或记录请求日志，以及支持可撤销的操作
/// </summary>
namespace Command {
    class Program {
        static void Main (string[] args) {
            Barbecuer barbecuer = new Barbecuer ();
            Command bakeMutton = new BakeMuttonCommand (barbecuer);
            Command bakeChickenWing = new BakeChickenWingCommand (barbecuer);
            Waiter waiter = new Waiter ();

            waiter.SetOrder (bakeMutton);
            waiter.SetOrder (bakeChickenWing);

            waiter.Notify ();
        }
    }
    /// <summary>
    /// 烧烤
    /// </summary>
    class Barbecuer {
        public void BakeMutton () {
            System.Console.WriteLine ("烤羊肉串");
        }

        public void BakeChickenWing () {
            System.Console.WriteLine ("烤鸡肉串");
        }
    }
    /// <summary>
    /// 抽象命令
    /// </summary>
    abstract class Command {
        protected Barbecuer reciver;
        public Command (Barbecuer barbecuer) {
            this.reciver = barbecuer;
        }
        abstract public void ExecuteCommand ();
    }
    /// <summary>
    /// 烤羊肉串命令
    /// </summary>
    class BakeMuttonCommand : Command {
        public BakeMuttonCommand (Barbecuer barbecuer) : base (barbecuer) { }
        public override void ExecuteCommand () {
            reciver.BakeMutton ();
        }
    }
    /// <summary>
    /// 烤鸡翅命令
    /// </summary>
    class BakeChickenWingCommand : Command {
        public BakeChickenWingCommand (Barbecuer barbecuer) : base (barbecuer) { }
        public override void ExecuteCommand () {
            reciver.BakeChickenWing ();
        }
    }
    /// <summary>
    /// 服务员
    /// </summary>
    class Waiter {
        IList<Command> commands = new List<Command> ();

        //增加订单
        public void SetOrder (Command cmd) {
            if (cmd.ToString () == "Command.BakeChickenWingCommand") {
                System.Console.WriteLine ("鸡翅没有了，请点其他烧烤");
            } else {
                commands.Add (cmd);
                System.Console.WriteLine ($"增加订单：{cmd.ToString()} 时间{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            }
        }
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="command"></param>
        public void CancelOrder (Command command) {
            commands.Remove (command);
            System.Console.WriteLine ($"取消订单{command.ToString()} 时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }
        /// <summary>
        /// 执行全部命令
        /// </summary>
        public void Notify () {
            foreach (Command cmd in commands) {
                cmd.ExecuteCommand ();
            }
        }
    }
}