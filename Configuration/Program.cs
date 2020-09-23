using System;
using Microsoft.Extensions.Configuration;

namespace Configuration {
    class Program {
        static void Main (string[] args) {
            IConfiguration configuration = new ConfigurationBuilder ().SetBasePath (Environment.CurrentDirectory).AddJsonFile ("appsettings.json").Build ();
            System.Console.WriteLine(configuration["name"]);

        }
    }
}