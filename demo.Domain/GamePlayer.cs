using System.Collections.Generic;
namespace demo.Domain {
    /// <summary>
    /// 每场赛事 中间表，关联队员+赛事
    ///  1个队员对应多场赛事 一场赛事对应多个队员 间接形成多对多
    /// </summary>
    public class GamePlayer {
        public int PlayerId { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public Player Player { get; set; }
    }
}