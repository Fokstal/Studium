﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Data;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240531153931_changeEntityInGradeListToHashSet")]
    partial class changeEntityInGradeListToHashSet
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.5");

            modelBuilder.Entity("api.Models.Entities.GradeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GradeModelEntityId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GradeModelId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GradeModelEntityId");

                    b.ToTable("GradeEntity");
                });

            modelBuilder.Entity("api.Models.Entities.GradeTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("GradeTypeEntity");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Practice"
                        },
                        new
                        {
                            Id = 2,
                            Name = "ControlWork"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Lecture"
                        });
                });

            modelBuilder.Entity("api.Models.GradeModelEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("SetDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("SubjectEntityId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SubjectId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TypeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SubjectEntityId");

                    b.HasIndex("TypeId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("api.Models.GroupEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AuditoryName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CuratorId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("api.Models.PassportEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ScanFileName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Passport");
                });

            modelBuilder.Entity("api.Models.PermissionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PermissionEntity");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "ViewGradeList"
                        },
                        new
                        {
                            Id = 2,
                            Name = "ViewGrade"
                        },
                        new
                        {
                            Id = 3,
                            Name = "EditGrade"
                        },
                        new
                        {
                            Id = 4,
                            Name = "ViewGroup"
                        },
                        new
                        {
                            Id = 5,
                            Name = "EditGroup"
                        },
                        new
                        {
                            Id = 6,
                            Name = "ViewPassportList"
                        },
                        new
                        {
                            Id = 7,
                            Name = "ViewPassport"
                        },
                        new
                        {
                            Id = 8,
                            Name = "EditPassport"
                        },
                        new
                        {
                            Id = 9,
                            Name = "ViewPersonList"
                        },
                        new
                        {
                            Id = 10,
                            Name = "ViewPerson"
                        },
                        new
                        {
                            Id = 11,
                            Name = "EditPerson"
                        },
                        new
                        {
                            Id = 12,
                            Name = "ViewStudentList"
                        },
                        new
                        {
                            Id = 13,
                            Name = "ViewStudent"
                        },
                        new
                        {
                            Id = 14,
                            Name = "EditStudent"
                        },
                        new
                        {
                            Id = 15,
                            Name = "ViewSubjectList"
                        },
                        new
                        {
                            Id = 16,
                            Name = "ViewSubject"
                        },
                        new
                        {
                            Id = 17,
                            Name = "EditSubject"
                        },
                        new
                        {
                            Id = 18,
                            Name = "ViewUser"
                        },
                        new
                        {
                            Id = 19,
                            Name = "RegisterUser"
                        },
                        new
                        {
                            Id = 20,
                            Name = "EditUser"
                        });
                });

            modelBuilder.Entity("api.Models.PersonEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AvatarFileName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("PassportId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Sex")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("StudentId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PassportId");

                    b.HasIndex("StudentId");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("api.Models.RoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Secretar"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Curator"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Teacher"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Student"
                        });
                });

            modelBuilder.Entity("api.Models.RolePermissionEntity", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PermissionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermissionEntity");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            PermissionId = 1
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 2
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 3
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 4
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 5
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 6
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 7
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 8
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 9
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 10
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 11
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 12
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 13
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 14
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 15
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 16
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 17
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 18
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 19
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 20
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 1
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 2
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 3
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 4
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 5
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 6
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 7
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 8
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 9
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 10
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 11
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 12
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 13
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 14
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 15
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 16
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 17
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 18
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 19
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 20
                        },
                        new
                        {
                            RoleId = 3,
                            PermissionId = 2
                        },
                        new
                        {
                            RoleId = 3,
                            PermissionId = 4
                        },
                        new
                        {
                            RoleId = 3,
                            PermissionId = 10
                        },
                        new
                        {
                            RoleId = 3,
                            PermissionId = 13
                        },
                        new
                        {
                            RoleId = 3,
                            PermissionId = 16
                        },
                        new
                        {
                            RoleId = 4,
                            PermissionId = 2
                        },
                        new
                        {
                            RoleId = 4,
                            PermissionId = 3
                        },
                        new
                        {
                            RoleId = 4,
                            PermissionId = 4
                        },
                        new
                        {
                            RoleId = 4,
                            PermissionId = 10
                        },
                        new
                        {
                            RoleId = 4,
                            PermissionId = 13
                        },
                        new
                        {
                            RoleId = 4,
                            PermissionId = 16
                        },
                        new
                        {
                            RoleId = 5,
                            PermissionId = 2
                        },
                        new
                        {
                            RoleId = 5,
                            PermissionId = 4
                        },
                        new
                        {
                            RoleId = 5,
                            PermissionId = 7
                        },
                        new
                        {
                            RoleId = 5,
                            PermissionId = 10
                        },
                        new
                        {
                            RoleId = 5,
                            PermissionId = 13
                        },
                        new
                        {
                            RoleId = 5,
                            PermissionId = 16
                        });
                });

            modelBuilder.Entity("api.Models.StudentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("GroupEntityId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("RemovedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GroupEntityId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("api.Models.SubjectEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("GroupEntityId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GroupEntityId");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("api.Models.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a34eff4d-f040-23a8-f15d-3f4f01ab62ea"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "AdminBase",
                            LastName = "",
                            Login = "admin",
                            MiddleName = "",
                            PasswordHash = "wNT47n/qyBSao7WnTCiZurLCZMmiEDmOzNKe0fuAh68syTJE8Yu1/bOhE8OA+KlRzHgc4DbrAwC20oJ2ruAYRg=="
                        },
                        new
                        {
                            Id = new Guid("1144b240-4126-78dd-dd4f-93b6c9190dd4"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "SecretarBase",
                            LastName = "",
                            Login = "secretar",
                            MiddleName = "",
                            PasswordHash = "ldntahdGhOPXDqs16vPWLoGYILz5TLDlTlrqlh2sI56JzrXd6MXx4a4vrD+sHSEoWZ0KdjSczeERVLKl+oY6YQ=="
                        },
                        new
                        {
                            Id = new Guid("08bfaf3b-2a88-102d-1330-93a1b8433f50"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "CuratorBase",
                            LastName = "",
                            Login = "curator",
                            MiddleName = "",
                            PasswordHash = "tiMq2aFb7mOs35MsukDafs+e0fJVAJFcPykiCirdGYGZkqXA4uSzBYCBQk/jWGTfEgBLNCol0UcTAEdnXUEENw=="
                        },
                        new
                        {
                            Id = new Guid("b4d821a3-e305-26ef-0495-9847b36d171e"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "TeacherBase",
                            LastName = "",
                            Login = "teacher",
                            MiddleName = "",
                            PasswordHash = "80n2TDMphhuzv9mfZnEwvnqrLn7R7ffGZt0WNGNSSuunsRpYJDy+d2/1cZDkw6POrZDyWw8Geuek9NMUoL5duw=="
                        },
                        new
                        {
                            Id = new Guid("3705df06-8119-37a2-d0ed-11472fae7c94"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "StudentBase",
                            LastName = "",
                            Login = "student",
                            MiddleName = "",
                            PasswordHash = "jb7vuZcUaY7Tl3gzhaNc880qfvwoCAaGvCGevz+DEI+glffW/gH4fOpQUyJ42/r3QbOQAsqyyS85E1hBoFQG1w=="
                        });
                });

            modelBuilder.Entity("api.Models.UserRoleEntity", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoleEntity");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("a34eff4d-f040-23a8-f15d-3f4f01ab62ea"),
                            RoleId = 1
                        },
                        new
                        {
                            UserId = new Guid("1144b240-4126-78dd-dd4f-93b6c9190dd4"),
                            RoleId = 2
                        },
                        new
                        {
                            UserId = new Guid("08bfaf3b-2a88-102d-1330-93a1b8433f50"),
                            RoleId = 3
                        },
                        new
                        {
                            UserId = new Guid("b4d821a3-e305-26ef-0495-9847b36d171e"),
                            RoleId = 4
                        },
                        new
                        {
                            UserId = new Guid("3705df06-8119-37a2-d0ed-11472fae7c94"),
                            RoleId = 5
                        });
                });

            modelBuilder.Entity("api.Models.Entities.GradeEntity", b =>
                {
                    b.HasOne("api.Models.GradeModelEntity", null)
                        .WithMany("GradeList")
                        .HasForeignKey("GradeModelEntityId");
                });

            modelBuilder.Entity("api.Models.GradeModelEntity", b =>
                {
                    b.HasOne("api.Models.SubjectEntity", null)
                        .WithMany("GradesList")
                        .HasForeignKey("SubjectEntityId");

                    b.HasOne("api.Models.Entities.GradeTypeEntity", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("api.Models.PersonEntity", b =>
                {
                    b.HasOne("api.Models.PassportEntity", "Passport")
                        .WithMany()
                        .HasForeignKey("PassportId");

                    b.HasOne("api.Models.StudentEntity", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId");

                    b.Navigation("Passport");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("api.Models.RolePermissionEntity", b =>
                {
                    b.HasOne("api.Models.PermissionEntity", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.RoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.Models.StudentEntity", b =>
                {
                    b.HasOne("api.Models.GroupEntity", null)
                        .WithMany("StudentList")
                        .HasForeignKey("GroupEntityId");
                });

            modelBuilder.Entity("api.Models.SubjectEntity", b =>
                {
                    b.HasOne("api.Models.GroupEntity", null)
                        .WithMany("SubjectList")
                        .HasForeignKey("GroupEntityId");
                });

            modelBuilder.Entity("api.Models.UserRoleEntity", b =>
                {
                    b.HasOne("api.Models.RoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.Models.GradeModelEntity", b =>
                {
                    b.Navigation("GradeList");
                });

            modelBuilder.Entity("api.Models.GroupEntity", b =>
                {
                    b.Navigation("StudentList");

                    b.Navigation("SubjectList");
                });

            modelBuilder.Entity("api.Models.SubjectEntity", b =>
                {
                    b.Navigation("GradesList");
                });
#pragma warning restore 612, 618
        }
    }
}