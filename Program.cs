using System;
using System.Collections.Immutable;
using Microsoft.Extensions.Configuration;

namespace aspnetcore3_demo {
    public class Program {
        public static void Main (string[] args) {
            var builder = new ConfigurationBuilder ();
            builder.AddEnvironmentVariables ();

            var configurationRoot = builder.Build ();
            System.Console.WriteLine ($"key1:{configurationRoot["key1"]}");

            #region 分层键获取
            var section = configurationRoot.GetSection ("SECTION1");
            System.Console.WriteLine ($"KEY3:{section["KEY3"]}");

            var section2 = configurationRoot.GetSection ("SECTION1:SECTION2");
            System.Console.WriteLine ($"KEY4:{section2["KEY4"]}");
            #endregion

            #region 前缀过滤
            builder.AddEnvironmentVariables ("XIAO_");
            configurationRoot = builder.Build ();
            System.Console.WriteLine ($"key1:{configurationRoot["key1"]}");
            #endregion
            Console.ReadKey ();
        }
    }
}
