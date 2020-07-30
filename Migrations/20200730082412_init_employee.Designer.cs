﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using aspnetcore3_demo.Data;

namespace aspnetcore3._1_demo.Migrations
{
    [DbContext(typeof(RoutineDBContext))]
    [Migration("20200730082412_init_employee")]
    partial class init_employee
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6");

            modelBuilder.Entity("aspnetcore3_demo.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Introduction")
                        .HasColumnType("TEXT")
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Companys");

                    b.HasData(
                        new
                        {
                            Id = new Guid("19d42960-7635-4360-b25a-76f65793f352"),
                            Introduction = "Create Company",
                            Name = "Microsoft"
                        },
                        new
                        {
                            Id = new Guid("d3da0df3-6097-40cc-9682-df4650bb34f5"),
                            Introduction = "aaa Company",
                            Name = "Google"
                        },
                        new
                        {
                            Id = new Guid("1475b207-6bf2-4c2e-86cd-a1cbbe9ec630"),
                            Introduction = "Dont be evil",
                            Name = "Alibaba"
                        });
                });

            modelBuilder.Entity("aspnetcore3_demo.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmployeeNo")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(10);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = new Guid("62491684-23c9-4ca7-b558-fe93ce663fc9"),
                            CompanyId = new Guid("1475b207-6bf2-4c2e-86cd-a1cbbe9ec630"),
                            DateOfBirth = new DateTime(1945, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeNo = "B001",
                            FirstName = "Qary",
                            Gender = 2,
                            LastName = "Uing"
                        },
                        new
                        {
                            Id = new Guid("bc801fd6-80e7-49d9-b239-0604fcc71b1e"),
                            CompanyId = new Guid("1475b207-6bf2-4c2e-86cd-a1cbbe9ec630"),
                            DateOfBirth = new DateTime(1937, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeNo = "B002",
                            FirstName = "Yichl",
                            Gender = 1,
                            LastName = "Ikng"
                        },
                        new
                        {
                            Id = new Guid("4f4ea5a8-5d05-41f1-bbcb-67cf14f09472"),
                            CompanyId = new Guid("d3da0df3-6097-40cc-9682-df4650bb34f5"),
                            DateOfBirth = new DateTime(1985, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeNo = "C001",
                            FirstName = "Aary",
                            Gender = 2,
                            LastName = "Fing"
                        },
                        new
                        {
                            Id = new Guid("86fa9cf7-bfe2-46fa-b670-f3601641e689"),
                            CompanyId = new Guid("1475b207-6bf2-4c2e-86cd-a1cbbe9ec630"),
                            DateOfBirth = new DateTime(1995, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeNo = "C002",
                            FirstName = "Aichl",
                            Gender = 1,
                            LastName = "Fang"
                        },
                        new
                        {
                            Id = new Guid("b533a68e-8e64-46c2-afcf-b6b8b6c78982"),
                            CompanyId = new Guid("19d42960-7635-4360-b25a-76f65793f352"),
                            DateOfBirth = new DateTime(1985, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeNo = "A001",
                            FirstName = "Mary",
                            Gender = 2,
                            LastName = "King"
                        },
                        new
                        {
                            Id = new Guid("7aa8d6ec-8088-457b-a547-d68cdabb96a6"),
                            CompanyId = new Guid("19d42960-7635-4360-b25a-76f65793f352"),
                            DateOfBirth = new DateTime(1995, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeNo = "A002",
                            FirstName = "Michl",
                            Gender = 1,
                            LastName = "Wang"
                        });
                });

            modelBuilder.Entity("aspnetcore3_demo.Entities.Employee", b =>
                {
                    b.HasOne("aspnetcore3_demo.Entities.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
