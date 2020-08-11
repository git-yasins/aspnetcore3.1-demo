using System;
/// <summary>
/// 状态模式 
/// 当一个对象的内在状态改变时允许改变其行为
/// 这个对象看起来像是改变了其类
/// </summary>
namespace State {
    class Program {
        static void Main (string[] args) {
            Work work = new Work ();
            work.Hour = 15;
            work.Finish = false;
            work.WriteProgram ();
            work.Hour = 20;
            work.WriteProgram ();
            work.Hour = 23;
            work.WriteProgram ();
        }
    }
    /// <summary>
    /// 抽象状态类
    /// </summary>
    public abstract class State {
        public abstract void WriteProgram (Work work);
    }
    /// <summary>
    /// 工作类
    /// </summary>
    public class Work {
        State currentState;
        public Work () {
            //初始状态为上午状态
            currentState = new ForenoonState ();
        }
        public double Hour { get; set; }
        public bool Finish { get; set; }
        public void SetState (State state) {
            currentState = state;
        }
        public void WriteProgram () {
            currentState.WriteProgram (this);
        }
    }
    /// <summary>
    /// 上午
    /// </summary>
    public class ForenoonState : State {
        public override void WriteProgram (Work work) {
            if (work.Hour < 12) {
                System.Console.WriteLine ($"当前时间：{work.Hour}点，开始工作");
            } else {
                work.SetState (new NonnState ());
                work.WriteProgram ();
            }
        }
    }
    /// <summary>
    /// 中午
    /// </summary>
    public class NonnState : State {
        public override void WriteProgram (Work work) {
            if (work.Hour < 13) {
                System.Console.WriteLine ($"当前时间：{work.Hour}点，午休会");
            } else {
                work.SetState (new AfternoonState ());
                work.WriteProgram ();
            }
        }
    }

    /// <summary>
    /// 下午
    /// </summary>
    public class AfternoonState : State {
        public override void WriteProgram (Work work) {
            if (work.Hour < 17) {
                System.Console.WriteLine ($"当前时间：{work.Hour}点，有点困");
            } else {
                work.SetState (new EveningState ());
                work.WriteProgram ();
            }
        }
    }
    /// <summary>
    /// 晚上
    /// </summary>
    public class EveningState : State {
        public override void WriteProgram (Work work) {
            if (work.Finish) {
                work.SetState (new RestState ());
                work.WriteProgram ();
            } else {
                if (work.Hour < 21) {
                    System.Console.WriteLine ($"当前时间：{work.Hour}点,继续加班");
                } else {
                    work.SetState (new SleepState ());
                    work.WriteProgram ();
                }
            }
        }
    }
    /// <summary>
    /// 休息
    /// </summary>
    public class SleepState : State {
        public override void WriteProgram (Work work) {
            System.Console.WriteLine ($"当前时间：{work.Hour}点，太晚了，休息了");
        }
    }
    /// <summary>
    /// 下班状态
    /// </summary>
    public class RestState : State {
        public override void WriteProgram (Work work) {
            System.Console.WriteLine ($"当前时间：{work.Hour}点，下班休息");
        }
    }
}