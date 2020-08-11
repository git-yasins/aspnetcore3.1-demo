using System;
using System.IO;
using demo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace demo.Data {
    public class DemoContext : DbContext {
        IConfiguration configuration;
        public DbSet<League> Leagues { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        /// <summary>
        /// 无主键表
        /// </summary>
        /// <value></value>
        //public DbSet<PlayerClub> PlayerClubs { get; set; }
        public DemoContext () {
            string s = Directory.GetCurrentDirectory ();
            //获取路径下的配置文件，生成文件处理对象
            configuration = new ConfigurationBuilder ().SetBasePath (Directory.GetCurrentDirectory ()).AddJsonFile ("appsettings.json").Build ();
        }
        /// <summary>
        /// 根据配置节点，读取配置文件
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder
                .UseLoggerFactory (ConsoleLoggerFactory)
                .EnableSensitiveDataLogging ()
                .UseMySql (configuration.GetConnectionString ("Default"));
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.Entity<GamePlayer> ().HasKey (c => new { c.PlayerId, c.GameId });
            modelBuilder.Entity<Resume> ().HasOne (c => c.Player).
            WithOne (c => c.Resume).HasForeignKey<Resume> (x => x.PlayerId);
            //设置无主键表
          // modelBuilder.Entity<PlayerClub> ().
           // HasNoKey ().ToView ("PlayerClubView"); //Migration 自定义的视图
        }

        /// <summary>
        /// 控制台日志记录
        /// </summary>
        /// <returns></returns>
        public static readonly ILoggerFactory ConsoleLoggerFactory =
            LoggerFactory.Create (builder => {
                builder.AddFilter (
                    (category, level) =>
                    category == DbLoggerCategory.Database.Command.Name &&
                    level == LogLevel.Information).AddConsole ();
            });
    }
}