﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RoutineMaster.Data;

#nullable disable

namespace RoutineMaster.Data.Migrations
{
    [DbContext(typeof(RMDataContext))]
    [Migration("20220712135213_DateRemove")]
    partial class DateRemove
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RoutineMaster.Models.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Difficulty")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TotalPages")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.Budget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("OverflowBudgetId")
                        .HasColumnType("integer");

                    b.Property<int?>("SavingsAccountId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OverflowBudgetId");

                    b.HasIndex("SavingsAccountId");

                    b.HasIndex("UserId");

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Difficulty")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.CourseEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("PercentCompleted")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseEntries");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.CreativeProject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("CreativeProjects");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.CreativeProjectEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CreativeProjectId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("PercentCompleted")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("CreativeProjectId");

                    b.ToTable("CreativeProjectEntries");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.DailyScore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Score")
                        .HasColumnType("double precision");

                    b.Property<int>("ScoreType")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("DailyScores");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.ExerciseRoutine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BaseAmount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Scale")
                        .HasColumnType("double precision");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("ExerciseRoutines");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.ExerciseRoutineEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ExerciseRoutineId")
                        .HasColumnType("integer");

                    b.Property<int>("Score")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseRoutineId");

                    b.ToTable("ExerciseRoutineEntries");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.ExpenseEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<int?>("BudgetId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BudgetId");

                    b.HasIndex("UserId");

                    b.ToTable("ExpenseEntries");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.ExpenseTag", b =>
                {
                    b.Property<int>("TagId")
                        .HasColumnType("integer");

                    b.Property<int>("ExpenseEntryId")
                        .HasColumnType("integer");

                    b.HasKey("TagId", "ExpenseEntryId");

                    b.HasIndex("ExpenseEntryId");

                    b.ToTable("ExpenseTags");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.MealRating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("MealType")
                        .HasColumnType("integer");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("MealRatings");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.MundaneList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("MundaneLists");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.MundaneListItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Complete")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CompletedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MundaneListId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MundaneListId");

                    b.ToTable("MundaneListItems");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.MundaneRoutine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Difficulty")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("MundaneRoutines");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.MundaneRoutineEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("RoutineId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoutineId");

                    b.ToTable("MundaneRoutineEntries");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.ReadingEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PagesRead")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("ReadingEntries");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.SavingsAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Goal")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("SavingsAccounts");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.UserIncome", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("IncidentalBonus")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserIncomes");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.Budget", b =>
                {
                    b.HasOne("RoutineMaster.Models.Entities.Budget", "OverflowBudget")
                        .WithMany()
                        .HasForeignKey("OverflowBudgetId");

                    b.HasOne("RoutineMaster.Models.Entities.SavingsAccount", "SavingsAccount")
                        .WithMany()
                        .HasForeignKey("SavingsAccountId");

                    b.HasOne("RoutineMaster.Models.Entities.User", null)
                        .WithMany("Budgets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OverflowBudget");

                    b.Navigation("SavingsAccount");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.CourseEntry", b =>
                {
                    b.HasOne("RoutineMaster.Models.Entities.Course", "Course")
                        .WithMany("Entries")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.CreativeProjectEntry", b =>
                {
                    b.HasOne("RoutineMaster.Models.Entities.CreativeProject", null)
                        .WithMany("Entries")
                        .HasForeignKey("CreativeProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.ExerciseRoutineEntry", b =>
                {
                    b.HasOne("RoutineMaster.Models.Entities.ExerciseRoutine", "Routine")
                        .WithMany()
                        .HasForeignKey("ExerciseRoutineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Routine");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.ExpenseEntry", b =>
                {
                    b.HasOne("RoutineMaster.Models.Entities.Budget", "Budget")
                        .WithMany("Entries")
                        .HasForeignKey("BudgetId");

                    b.HasOne("RoutineMaster.Models.Entities.User", null)
                        .WithMany("ExpenseEntries")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Budget");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.ExpenseTag", b =>
                {
                    b.HasOne("RoutineMaster.Models.Entities.ExpenseEntry", "ExpenseEntry")
                        .WithMany("ExpenseTags")
                        .HasForeignKey("ExpenseEntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RoutineMaster.Models.Entities.Tag", "Tag")
                        .WithMany("ExpenseTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExpenseEntry");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.MundaneListItem", b =>
                {
                    b.HasOne("RoutineMaster.Models.Entities.MundaneList", null)
                        .WithMany("Items")
                        .HasForeignKey("MundaneListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.MundaneRoutineEntry", b =>
                {
                    b.HasOne("RoutineMaster.Models.Entities.MundaneRoutine", "Routine")
                        .WithMany()
                        .HasForeignKey("RoutineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Routine");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.ReadingEntry", b =>
                {
                    b.HasOne("RoutineMaster.Models.Entities.Book", "Book")
                        .WithMany("Entries")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.UserIncome", b =>
                {
                    b.HasOne("RoutineMaster.Models.Entities.User", null)
                        .WithOne("UserIncome")
                        .HasForeignKey("RoutineMaster.Models.Entities.UserIncome", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.Book", b =>
                {
                    b.Navigation("Entries");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.Budget", b =>
                {
                    b.Navigation("Entries");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.Course", b =>
                {
                    b.Navigation("Entries");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.CreativeProject", b =>
                {
                    b.Navigation("Entries");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.ExpenseEntry", b =>
                {
                    b.Navigation("ExpenseTags");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.MundaneList", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.Tag", b =>
                {
                    b.Navigation("ExpenseTags");
                });

            modelBuilder.Entity("RoutineMaster.Models.Entities.User", b =>
                {
                    b.Navigation("Budgets");

                    b.Navigation("ExpenseEntries");

                    b.Navigation("UserIncome")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
