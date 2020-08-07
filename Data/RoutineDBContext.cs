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
            modelBuilder.Entity<Company> ().Property (x => x.Country).HasMaxLength (50);
            modelBuilder.Entity<Company> ().Property (x => x.Industry).HasMaxLength (50);
            modelBuilder.Entity<Company> ().Property (x => x.Product).HasMaxLength (100);

            modelBuilder.Entity<Employee> ().Property (x => x.EmployeeNo).IsRequired ().HasMaxLength (10);
            modelBuilder.Entity<Employee> ().Property (x => x.FirstName).IsRequired ().HasMaxLength (50);
            modelBuilder.Entity<Employee> ().Property (x => x.LastName).IsRequired ().HasMaxLength (50);

            modelBuilder.Entity<Employee> ().HasOne (x => x.Company).WithMany (x => x.Employees)
                .HasForeignKey (x => x.CompanyId)
                .OnDelete (DeleteBehavior.Cascade); //级联删除 删除父表数据时,同时删除关联的子表数据
            //.OnDelete (DeleteBehavior.Restrict);//不级联删除

            //创建测试种子数据
            modelBuilder.Entity<Company> ().HasData (
                new Company { Id = Guid.Parse ("19d42960-7635-4360-b25a-76f65793f352"), Name = "Microsoft", Introduction = "Create Company", Product = "SoftWare", Country = "USA", Industry = "SoftWare" },
                new Company { Id = Guid.Parse ("0f5c57b3-1635-4666-9bb8-64171c0fc6ec"), Name = "Tencent", Introduction = "Tencent Company", Product = "electrophile,game", Country = "CN", Industry = "SoftWare" },
                new Company { Id = Guid.Parse ("f72d336e-514d-41a1-a91e-f2eb0f4ddc46"), Name = "WFT", Introduction = "WFT Company", Product = "web", Country = "CN", Industry = "SoftWare" },
                new Company { Id = Guid.Parse ("fb4d85e5-7b77-47f6-b223-e237706eb59a"), Name = "HQ", Introduction = "HQ Company", Product = "electrophile", Country = "CN", Industry = "electrophile" },
                new Company { Id = Guid.Parse ("e85c5afe-389d-4eb1-9d8d-e8bd110a8092"), Name = "Gaotong", Introduction = "Gaotong Company", Product = "USA", Country = "TW", Industry = "SoftWare" },
                new Company { Id = Guid.Parse ("dc9377f0-ef11-42a2-89df-69279fd7f81d"), Name = "Tongyong", Introduction = "Tongyong Company", Product = "USA", Country = "FR", Industry = "electrophile" },
                new Company { Id = Guid.Parse ("2aa4335e-bfa1-4820-94f9-f7bb717a4997"), Name = "HP", Introduction = "HP Company", Product = "print", Country = "USA", Industry = "electrophile" },
                new Company { Id = Guid.Parse ("201a3e4a-83bf-4e9c-ac82-8c45d6a57dfb"), Name = "NOKIA", Introduction = "NOKIA Company", Product = "phone", Country = "NL", Industry = "SoftWare" },
                new Company { Id = Guid.Parse ("43a757aa-f01c-4f4c-ab6e-2f4a24647c9b"), Name = "APPLIE", Introduction = "APPLIE Company", Product = "electrophile,software", Country = "USA", Industry = "SoftWare" },
                new Company { Id = Guid.Parse ("eb0744ea-d315-4f7f-8a3c-14ae4cf5e3e7"), Name = "ORACLE", Introduction = "ORACLE Company", Product = "db", Country = "USA", Industry = "SoftWare" },
                new Company { Id = Guid.Parse ("939a8356-62ab-4701-8ca8-b34778c20a43"), Name = "BAIDU", Introduction = "BAIDU Company", Product = "search enging", Country = "CN", Industry = "web" },
                new Company { Id = Guid.Parse ("45f8183b-2258-4589-b37c-a4d7939bf4df"), Name = "BILIBILI", Introduction = "BILIBILI Company", Product = "web", Country = "CN", Industry = "web" },
                new Company { Id = Guid.Parse ("4b56e067-c5de-4846-948d-0917f79501d7"), Name = "FACEBOOK", Introduction = "FACEBOOK Company", Product = "social contact", Country = "USA", Industry = "web" },
                new Company { Id = Guid.Parse ("7bfa2bc8-9748-4950-876e-9c733c4f5656"), Name = "YOUTUBE", Introduction = "YOUTUBE Company", Product = "social contact", Country = "USA", Industry = "web" },
                new Company { Id = Guid.Parse ("947c05b7-b566-41b9-a466-e071f9c6c8e0"), Name = "AMAZON", Introduction = "AMAZON Company", Product = "web", Country = "USA", Industry = "software" },
                new Company { Id = Guid.Parse ("74f4655e-7206-4e85-98ff-695841ecd8b2"), Name = "DELL", Introduction = "DELL Company", Product = "computer", Country = "USA", Industry = "hardware" },
                new Company { Id = Guid.Parse ("5fece8c6-afae-407a-96b6-c97aca57e4c2"), Name = "SONY", Introduction = "SONY Company", Product = "electrophile", Country = "JP", Industry = "hardware" },
                new Company { Id = Guid.Parse ("fee4429e-4ed0-4b4b-9ad0-829e6d7dd496"), Name = "SAMSUN", Introduction = "SAMSUN Company", Product = "phone", Country = "KP", Industry = "hardware" },
                new Company { Id = Guid.Parse ("0d5c19d5-a289-402b-96a3-3135b6b03a52"), Name = "Google", Introduction = "Google Company", Product = "search enging", Country = "USA", Industry = "web" },
                new Company { Id = Guid.Parse ("1475b207-6bf2-4c2e-86cd-a1cbbe9ec630"), Name = "Alibaba", Introduction = "Dont be evil", Product = "electrophile", Country = "CN", Industry = "web" }
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
                    Id = Guid.Parse ("82491684-23c9-4ca7-b558-fe93ce663f59"),
                        DateOfBirth = new DateTime (1945, 6, 5),
                        CompanyId = Guid.Parse ("0d5c19d5-a289-402b-96a3-3135b6b03a52"),
                        EmployeeNo = "f001",
                        FirstName = "yary",
                        LastName = "ssng",
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
                        CompanyId = Guid.Parse ("7bfa2bc8-9748-4950-876e-9c733c4f5656"),
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
                        CompanyId = Guid.Parse ("2aa4335e-bfa1-4820-94f9-f7bb717a4997"),
                        EmployeeNo = "A001",
                        FirstName = "Mary",
                        LastName = "King",
                        Gender = Gender.女
                },
                new Employee {
                    Id = Guid.Parse ("7aa8d6ec-8088-457b-a547-d68cdabb96a6"),
                        DateOfBirth = new DateTime (1995, 5, 6),
                        CompanyId = Guid.Parse ("2aa4335e-bfa1-4820-94f9-f7bb717a4997"),
                        EmployeeNo = "A002",
                        FirstName = "Michl",
                        LastName = "Wang",
                        Gender = Gender.男
                }
            );
        }
    }
}
