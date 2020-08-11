using System;
/// <summary>
/// 备忘录模式，在不破坏封装性的前提下
/// 捕获一个对象的内部状态，并在该对象之外保存这个状态
/// 这样以后就可以将该对象恢复到原先保存状态
/// </summary>
namespace Memento {
    class Program {
        static void Main (string[] args) {
            System.Console.WriteLine ("开始游戏");
            Game lixiaoyao = new Game ();
            lixiaoyao.InitState ();
            lixiaoyao.StateDisplay ();
            //保存进度
            RoleStateCaretaker caretaker = new RoleStateCaretaker ();
            caretaker.Memento = lixiaoyao.SaveState ();
            //战斗
            System.Console.WriteLine ("开始战斗");
            lixiaoyao.Fight ();
            lixiaoyao.StateDisplay ();
            System.Console.WriteLine ("恢复进度中。。。");
            //恢复值
            lixiaoyao.RecoveryState (caretaker.Memento);
            lixiaoyao.StateDisplay ();
        }
    }
    /// <summary>
    /// 游戏角色
    /// </summary>
    public class Game {
        /// <summary>
        /// 生命力
        /// </summary>
        /// <value></value>
        public int Vitality { get; set; }
        /// <summary>
        /// 攻击力
        /// </summary>
        /// <value></value>
        public int Attack { get; set; }
        /// <summary>
        /// 防御力
        /// </summary>
        /// <value></value>
        public int Defense { get; set; }
        /// <summary>
        /// 保存当前角色状态
        /// </summary>
        /// <returns></returns>
        public RoleMemento SaveState () {
            return new RoleMemento (Vitality, Attack, Defense);
        }
        /// <summary>
        /// 恢复角色状态
        /// </summary>
        /// <param name="memento"></param>
        public void RecoveryState (RoleMemento memento) {
            this.Vitality = memento.Vitality;
            this.Attack = memento.Attack;
            this.Defense = memento.Defense;
        }
        /// <summary>
        /// 显示状态值
        /// </summary>
        public void StateDisplay () {
            System.Console.WriteLine ($"体力：{this.Vitality}");
            System.Console.WriteLine ($"攻击力：{this.Attack}");
            System.Console.WriteLine ($"防御力：{this.Defense}");
        }
        /// <summary>
        /// 初始化值
        /// </summary>
        public void InitState () {
            this.Vitality = 100;
            this.Attack = 100;
            this.Defense = 100;
        }
        /// <summary>
        /// 战斗状态
        /// </summary>
        public void Fight () {
            this.Vitality = 0;
            this.Attack = 0;
            this.Defense = 0;
        }
    }
    /// <summary>
    /// 角色状态存储
    /// </summary>
    public class RoleMemento {
        public RoleMemento (int vit, int atk, int def) {
            this.Vitality = vit;
            this.Attack = atk;
            this.Defense = def;
        }
        /// <summary>
        /// 生命力
        /// </summary>
        /// <value></value>
        public int Vitality { get; set; }
        /// <summary>
        /// 攻击力
        /// </summary>
        /// <value></value>
        public int Attack { get; set; }
        /// <summary>
        /// 防御力
        /// </summary>
        /// <value></value>
        public int Defense { get; set; }
    }

    public class RoleStateCaretaker {
        public RoleMemento Memento { get; set; }
    }
}