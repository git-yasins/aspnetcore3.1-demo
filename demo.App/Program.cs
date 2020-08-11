using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using demo.Data;
using demo.Domain;
using Microsoft.EntityFrameworkCore;
namespace demo.App {
    class Program {
        static void Main (string[] args) {
            using var context = new DemoContext ();
            //   var italy = "Italy";
            //     var leagues = context.Leagues.Where (x => x.Country == italy).ToList ();
            //     foreach (var league in leagues) {
            //         Console.WriteLine (league.Name);
            //     }

            //    var f = context.Leagues.Find(2);

            //    System.Console.WriteLine(f?.Id);
            #region demo2 insert
            // var serieA = context.Leagues.FirstOrDefault (x => x.Name == "Serie A");
            // var juventus = new Club {
            //     League = serieA,
            //     Name = "Juventus",
            //     City = "Torino",
            //     DateOfEstablishment = new DateTime (1987, 11, 1),
            //     Players = new List<Player> {
            //     new Player {
            //         Name = "C. Rnaldo",
            //         DateOfBirth = new DateTime (1985, 2, 5)
            //     }
            //     }
            // };
            //  context.Clubs.Add (juventus);
            // int count = context.SaveChanges ();
            // System.Console.WriteLine (count);
            #endregion

            #region demo3 多表关联查询
            // var clubs = context.Clubs.Where (c => c.Id > 0)
            //     .Include (c => c.League)
            //     .Include (c => c.Players).ThenInclude (p => p.Resume)
            //     .Include (c => c.Players)
            //     .ThenInclude (p => p.GamePlayers).ThenInclude (g => g.Game)
            //     .FirstOrDefault ();
            #endregion

            #region demo4 多表关联查询，子查询 
            // var result = context.Clubs.Where (c => c.Id > 0)
            //     .Select (c => new {//context不能追踪匿名类
            //         Id = c.Id,
            //             LeagueName = c.League.Name,
            //             Name = c.Name,
            //             players = c.Players.Where (p => p.DateOfBirth > new DateTime (1990, 1, 1))
            //     }).ToList();
            #endregion

            //demo 5 修改关联表数据
            // var player = context.Players.First ();
            // //新增关联表数据
            // player.Resume = new Resume {
            //     Description = "99999"
            // };
            // // var resume = context.Resumes.Where (x => x.PlayerId == player.Id).First ();
            // resume.Description = "333";
            // 对关联表数据进行修改
            // var player = context.Players.Include (x => x.Resume).First ();
            // player.Resume = new Resume {
            //     Description = "777"
            // };
            //  context.SaveChanges ();

            // var player = context.PlayerClubs.First();

            var leagus = context.Leagues.FromSqlRaw($"select * from leagues").ToList();
            foreach(var i in leagus){
                System.Console.WriteLine(i.Name);
            }
        }
    }
}