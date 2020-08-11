using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
/// <summary>
/// 抽像工厂
/// </summary>
namespace AbtractFactory {
    class Program {
        static void Main (string[] args) {
            //IFactory factory = new SqlServerFacotory ();

            //IUser user = factory.CreateUser ();
            IUser user = DataAccess.CreateUser ();
            user.GetUsers (0);
            user.Insert (new Users ());

            //IDepartment department = factory.CreateDepartment ();
            IDepartment department = DataAccess.CreateDepartment ();
            department.Insert (new Department ());
            department.GetDepartment (0);

            System.Console.Read ();
        }
    }

    class DataAccess {
        //读取NETCORE 配置文件暂时有问题
        // static IConfigurationRoot config = new ConfigurationBuilder ()
        //     .SetBasePath (Directory.GetCurrentDirectory ())
        //     .AddJsonFile ("AbtractFactory/appsettings.json", optional : true, reloadOnChange : false)
        //     .AddEnvironmentVariables ()
        //     .Build ();

        static readonly string assemblyName = "AbtractFactory";
        /// <summary>
        /// 暂时写死类型
        /// </summary>
        static string className = assemblyName + "." + "SqlServer"; // config.GetSection ("db:dbName").Value;
        /// <summary>
        /// 获取程序集根目录
        /// </summary>
        /// <returns></returns>
        static string binPath = Path.GetDirectoryName (Assembly.GetEntryAssembly ().Location);
        public static IUser CreateUser () {
            return (IUser) CreateCommon ("User");
        }
        public static IDepartment CreateDepartment () {
            return (IDepartment) CreateCommon ("Department");
        }
        static IBase CreateCommon (string objName) {
            Assembly ass = Assembly.Load (new AssemblyName (assemblyName));
            Type type = ass.GetType (className + objName);
            return (IBase) Activator.CreateInstance (type);
        }
    }

    #region  Interface
    public interface IFactory {
        IUser CreateUser ();
        IDepartment CreateDepartment ();
    }

    public interface IBase {}

    public interface IDepartment : IBase {
        void Insert (Department department);
        Department GetDepartment (int id);
    }
    public interface IUser : IBase {
        void Insert (Users user);
        Users GetUsers (int id);
    }
    #endregion

    #region Sqlserver

    public class SqlServerDepartment : IDepartment {
        public Department GetDepartment (int id) {
            System.Console.WriteLine ("sqlserver get department by id");
            return null;
        }

        public void Insert (Department department) {
            System.Console.WriteLine ("sqlserver insert department");
        }
    }
    public class SqlServerUser : IUser {
        public Users GetUsers (int id) {
            System.Console.WriteLine ("sqlserver get Users by id");
            return null;
        }

        public void Insert (Users user) {
            System.Console.WriteLine ("sqlserver insert users");
        }
    }
    public class SqlServerFacotory : IFactory {
        public IDepartment CreateDepartment () {
            return new SqlServerDepartment ();
        }

        public IUser CreateUser () {
            return new SqlServerUser ();
        }
    }
    #endregion

    #region Access
    public class AccessDepartment : IDepartment {
        public Department GetDepartment (int id) {
            System.Console.WriteLine ("access get department by id");
            return null;
        }

        public void Insert (Department department) {
            System.Console.WriteLine ("access insert department");
        }
    }
    public class AccessUser : IUser {
        public Users GetUsers (int id) {
            System.Console.WriteLine ("access get Users by id");
            return null;
        }

        public void Insert (Users user) {
            System.Console.WriteLine ("access insert users");
        }
    }
    public class AccessFacotyr : IFactory {
        public IDepartment CreateDepartment () {
            return new AccessDepartment ();
        }
        public IUser CreateUser () {
            return new AccessUser ();
        }
    }
    #endregion

    #region Model
    public class Department {
        public int ID { get; set; }
        public string DepartmentName { get; set; }
        public List<Users> User { get; set; }
    }

    public class Users {
        public int ID { get; set; }
        public string UserName { get; set; }
    }
    #endregion
}