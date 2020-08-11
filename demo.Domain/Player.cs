using System;
using System.Collections.Generic;
namespace demo.Domain {

    /// <summary>
    /// 球员
    /// </summary>
    public class Player {
        public Player () {
            GamePlayers = new List<GamePlayer> ();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<GamePlayer> GamePlayers { get; set; }
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
}