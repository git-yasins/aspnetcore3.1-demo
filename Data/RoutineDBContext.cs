using System;
using System.Collections.Generic;
using aspnetcore3_demo.Entities;
using Microsoft.EntityFrameworkCore;
namespace aspnetcore3_demo.Data {
    public class RoutineDBContext : DbContext {
        public RoutineDBContext (DbContextOptions<RoutineDBContext> options) : base (options) {

        }

        public DbSet<Company> Companys { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.Entity<Company> ().Property (x => x.Name).IsRequired ().HasMaxLength (100);
            modelBuilder.Entity<Company> ().Property (x => x.Introduction).HasMaxLength (500);

            modelBuilder.Entity<Employee> ().Property (x => x.EmployeeNo).IsRequired ().HasMaxLength (10);
            modelBuilder.Entity<Employee> ().Property (x => x.FirstName).IsRequired ().HasMaxLength (50);
            modelBuilder.Entity<Employee> ().Property (x => x.LastName).IsRequired ().HasMaxLength (50);

            modelBuilder.Entity<Employee> ().HasOne (x => x.Company).WithMany (x => x.Employees).HasForeignKey (x => x.CompanyId).OnDelete (DeleteBehavior.Restrict);

            //创建测试种子数据
            modelBuilder.Entity<Company> ().HasData (
                new Company { Id = Guid.Parse ("19d42960-7635-4360-b25a-76f65793f352"), Name = "Microsoft", Introduction = "Create Company" },
                new Company { Id = Guid.Parse ("d3da0df3-6097-40cc-9682-df4650bb34f5"), Name = "Google", Introduction = "aaa Company" },
                new Company { Id = Guid.Parse ("1475b207-6bf2-4c2e-86cd-a1cbbe9ec630"), Name = "Alibaba", Introduction = "Dont be evil" }
            );

            modelBuilder.Entity<Employee> ().HasData (
                new Employee {
                    Id = Guid.Parse ("62491684-23c9-4ca7-b558-fe93ce663fc9"),
                        DateOfBirth = new DateTime (1945, 6, 5),
                        CompanyId = Guid.Parse ("1475b207-6bf2-4c2e-86cd-a1cbbe9ec630"),
                        EmployeeNo = "B001",
                        FirstName = "Qary",
                        LastName = "Uing",
                        Gender = Gender.女
                },
                new Employee {
                    Id = Guid.Parse ("bc801fd6-80e7-49d9-b239-0604fcc71b1e"),
                        DateOfBirth = new DateTime (1937, 3, 2),
                        CompanyId = Guid.Parse ("1475b207-6bf2-4c2e-86cd-a1cbbe9ec630"),
                        EmployeeNo = "B002",
                        FirstName = "Yichl",
                        LastName = "Ikng",
                        Gender = Gender.男
                },
                new Employee {
                    Id = Guid.Parse ("4f4ea5a8-5d05-41f1-bbcb-67cf14f09472"),
                        DateOfBirth = new DateTime (1985, 1, 5),
                        CompanyId = Guid.Parse ("d3da0df3-6097-40cc-9682-df4650bb34f5"),
                        EmployeeNo = "C001",
                        FirstName = "Aary",
                        LastName = "Fing",
                        Gender = Gender.女
                },
                new Employee {
                    Id = Guid.Parse ("86fa9cf7-bfe2-46fa-b670-f3601641e689"),
                        DateOfBirth = new DateTime (1995, 12, 9),
                        CompanyId = Guid.Parse ("1475b207-6bf2-4c2e-86cd-a1cbbe9ec630"),
                        EmployeeNo = "C002",
                        FirstName = "Aichl",
                        LastName = "Fang",
                        Gender = Gender.男
                },
                new Employee {
                    Id = Guid.Parse ("b533a68e-8e64-46c2-afcf-b6b8b6c78982"),
                        DateOfBirth = new DateTime (1985, 4, 4),
                        CompanyId = Guid.Parse ("19d42960-7635-4360-b25a-76f65793f352"),
                        EmployeeNo = "A001",
                        FirstName = "Mary",
                        LastName = "King",
                        Gender = Gender.女
                },
                new Employee {
                    Id = Guid.Parse ("7aa8d6ec-8088-457b-a547-d68cdabb96a6"),
                        DateOfBirth = new DateTime (1995, 5, 6),
                        CompanyId = Guid.Parse ("19d42960-7635-4360-b25a-76f65793f352"),
                        EmployeeNo = "A002",
                        FirstName = "Michl",
                        LastName = "Wang",
                        Gender = Gender.男
                }
            );
        }
    }
}
