﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using StudentSystem.Data;
using StudentSystem.EntityDataModels;
using System;

namespace StudentSystem.Migrations
{
    [DbContext(typeof(SystemDbContext))]
    [Migration("20171008233739_AddLicense")]
    partial class AddLicense
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StudentSystem.EntityDataModels.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("Price");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("StudentSystem.EntityDataModels.Homework", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<int>("ContentType");

                    b.Property<int>("CourseId");

                    b.Property<int>("StudentId");

                    b.Property<DateTime>("SubmissionDate");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("Homeworks");
                });

            modelBuilder.Entity("StudentSystem.EntityDataModels.License", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("ResourseId");

                    b.HasKey("Id");

                    b.HasIndex("ResourseId");

                    b.ToTable("Licenses");
                });

            modelBuilder.Entity("StudentSystem.EntityDataModels.Resource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CourseId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("ResourceType");

                    b.Property<string>("URL")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("StudentSystem.EntityDataModels.Student", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Birthday");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("PhoneNumber");

                    b.Property<DateTime>("RegistrationDate");

                    b.HasKey("id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("StudentSystem.EntityDataModels.StudentsCoursces", b =>
                {
                    b.Property<int>("StudentId");

                    b.Property<int>("CourseId");

                    b.HasKey("StudentId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("StudentsCoursces");
                });

            modelBuilder.Entity("StudentSystem.EntityDataModels.Homework", b =>
                {
                    b.HasOne("StudentSystem.EntityDataModels.Course", "Course")
                        .WithMany("Homeworks")
                        .HasForeignKey("CourseId")
                        .HasConstraintName("FK_Homework_Course_CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StudentSystem.EntityDataModels.Student", "Student")
                        .WithMany("Homeworks")
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK_Homework_Student_StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StudentSystem.EntityDataModels.License", b =>
                {
                    b.HasOne("StudentSystem.EntityDataModels.Resource", "Resource")
                        .WithMany("Licenses")
                        .HasForeignKey("ResourseId")
                        .HasConstraintName("FK_License_Resourse_ResourseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StudentSystem.EntityDataModels.Resource", b =>
                {
                    b.HasOne("StudentSystem.EntityDataModels.Course", "Course")
                        .WithMany("Resources")
                        .HasForeignKey("CourseId")
                        .HasConstraintName("FK_Resources_Course_CourseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StudentSystem.EntityDataModels.StudentsCoursces", b =>
                {
                    b.HasOne("StudentSystem.EntityDataModels.Course", "Course")
                        .WithMany("Students")
                        .HasForeignKey("CourseId")
                        .HasConstraintName("FK_StudentsCourses_Course_CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StudentSystem.EntityDataModels.Student", "Student")
                        .WithMany("Cources")
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK_StudentCourses_Student_StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
