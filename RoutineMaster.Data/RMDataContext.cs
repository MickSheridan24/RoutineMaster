
using Microsoft.EntityFrameworkCore;
using RoutineMaster.Models.Entities;

namespace RoutineMaster.Data
{
    public class RMDataContext : DbContext
    {
        public RMDataContext(DbContextOptions<RMDataContext> options) : base(options){

        }

        public DbSet<Book> Books {get; set;}
        public DbSet<Budget> Budgets {get; set;}
        public DbSet<Course> Courses {get; set;}
        public DbSet<CourseEntry> CourseEntries {get; set;}
        public DbSet<CreativeProject> CreativeProjects {get; set;}
        public DbSet<CreativeProjectEntry> CreativeProjectEntries {get; set;}
        public DbSet<DailyScore> DailyScores {get; set;}
        public DbSet<ExerciseRoutine> ExerciseRoutines {get; set;}
        public DbSet<ExerciseRoutineEntry> ExerciseRoutineEntries {get; set;}
        public DbSet<ExpenseEntry> ExpenseEntries {get; set;}
        public DbSet<MealRating> MealRatings {get; set;}
        public DbSet<MundaneList> MundaneLists {get; set;}
        public DbSet<MundaneListItem> MundaneListItems {get; set;}
        public DbSet<MundaneRoutine> MundaneRoutines { get; set; }
        public DbSet<MundaneRoutineEntry> MundaneRoutineEntries {get; set;}
        public DbSet<ReadingEntry> ReadingEntries {get; set;}
        public DbSet<SavingsAccount> SavingsAccounts {get; set;}
        public DbSet<User> Users {get; set;}
        public DbSet<UserIncome> UserIncomes { get; set; }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<ExpenseTag> ExpenseTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<ExpenseTag>().HasKey(e => new{e.TagId, e.ExpenseEntryId});

            modelBuilder.Entity<ExpenseEntry>().HasMany(e => e.ExpenseTags).WithOne(t => t.ExpenseEntry);
            modelBuilder.Entity<Tag>().HasMany(t => t.ExpenseTags).WithOne(e => e.Tag);
        
        }
    }
}