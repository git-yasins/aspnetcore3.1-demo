using System;
using System.Collections.Immutable;
using Microsoft.Extensions.Configuration;

namespace aspnetcore3_demo {
    public class Program {
        public static void Main (string[] args) {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json");
            
        }
    }
}
