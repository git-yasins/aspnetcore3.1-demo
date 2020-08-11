using System;
/// <summary>
/// 解释器模式 
/// 给定一个语言，定义它的文法的一种表示
/// 并定义一个解释器，这个解释器使用该表示来解释语言中的句子
/// </summary>
namespace Interpreter {
    class Program {
        static void Main (string[] args) {
            PlayContext context = new PlayContext ();
            context.PlayText = "M 100 O 2 E 0.5 G 0.5 A 3 E 0.5 G 0.5 D 3 E 0.5 G 0.5 A 0.5 O 3 C 1 O 2 A 0.5 G 1 C 0.5 E 0.5 D 3 ";
            Expression expression = null;
            try {
                while (context.PlayText.Length > 0) {
                    string str = context.PlayText.Substring (0, 1);
                    switch (str) {
                        case "O":
                            expression = new Scale ();
                            break;
                        case "M":
                            expression = new Speed ();
                            break;
                        case "C":
                        case "D":
                        case "E":
                        case "F":
                        case "G":
                        case "A":
                        case "B":
                        case "P":
                            expression = new Note ();
                            break;
                    }
                    expression.Interpret (context);
                }
            } catch (Exception ex) {
                System.Console.WriteLine (ex.Message);
            }
            Console.Read ();
        }
    }
    /// <summary>
    /// 演奏内容
    /// </summary>
    class PlayContext {
        /// <summary>
        /// 演奏文本
        /// </summary>
        /// <value></value>
        public string PlayText { get; set; }
    }
    /// <summary>
    /// 表达式
    /// </summary>
    abstract class Expression {
        public void Interpret (PlayContext context) {
            if (context.PlayText.Length == 0) {
                return;
            } else {
                //取得首字符：音阶
                //context: O 2 E 0.5 G 0.5 A 3 E 0.5 G 0.5 D 3 E 0.5 G 0.5 A 0.5 O 3 C 1 O 2 A 0.5 G 1 C 0.5 E 0.5 D 3
                string playKey = context.PlayText.Substring (0, 1);
                context.PlayText = context.PlayText.Substring (2);
                //获得音阶 1：低音 2：中音 3：高音
                double playValue = Convert.ToDouble (context.PlayText.Substring (0, context.PlayText.IndexOf (" ")));
                //获得PlayKey和playValue后将其从演奏文本中移除 变成:E 0.5 G 0.5 A 3 E 0.5 G 0.5 D 3 E 0.5 G 0.5 A 0.5 O 3 C 1 O 2 A 0.5 G 1 C 0.5 E 0.5 D 3
                context.PlayText = context.PlayText.Substring (context.PlayText.IndexOf (" ") + 1);
                //执行演奏
                Execute (playKey, playValue);
            }
        }
        /// <summary>
        /// 演奏执行
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public abstract void Execute (string key, double value);
    }
    /// <summary>
    /// 音符类
    /// </summary>
    class Note : Expression {
        public override void Execute (string key, double value) {
            string note = "";
            switch (key) {
                case "C":
                    note = "1";
                    break;
                case "D":
                    note = "2";
                    break;
                case "E":
                    note = "3";
                    break;
                case "F":
                    note = "4";
                    break;
                case "G":
                    note = "5";
                    break;
                case "A":
                    note = "6";
                    break;
                case "B":
                    note = "7";
                    break;
            }
            System.Console.Write ($"{note} ");
        }
    }
    /// <summary>
    /// 音阶类
    /// </summary>
    class Scale : Expression {
        public override void Execute (string key, double value) {
            string scale = string.Empty;
            switch (Convert.ToInt32 (value)) {
                case 1:
                    scale = "低音";
                    break;
                case 2:
                    scale = "中音";
                    break;
                case 3:
                    scale = "高音";
                    break;
            }
            System.Console.Write ($"{scale} ");
        }
    }
    /// <summary>
    ///  速度
    /// </summary>
    class Speed : Expression {
        public override void Execute (string key, double value) {
            string speed;
            if (value > 500)
                speed = "快速";
            else if (value >= 1000)
                speed = "慢速";
            else speed = "中速";
            System.Console.Write ($"{speed} ");
        }
    }
}