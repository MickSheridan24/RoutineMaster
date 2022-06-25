using Microsoft.EntityFrameworkCore;
using RoutineMaster.Data;
using RoutineMaster.Models.Entities;

namespace RoutineMaster.Service{
    public class EducationService : IEducationService{
        private RMDataContext context;
        public EducationService(RMDataContext context){
            this.context = context;
        }

        public async Task<ICollection<Book>> GetBooks(int userId)
        {
            return await context.Books.Where(b => b.UserId == userId).ToListAsync();
        }

        public async Task CreateBook(int userId, string name, int difficulty, int totalPages){
            var book = new Book {
                UserId = userId, 
                Name = name, 
                Difficulty = difficulty,
                TotalPages = totalPages,
                Created = DateTime.UtcNow
            };

            await context.Books.AddAsync(book);

            await context.SaveChangesAsync();
        }

        public async Task CreateReadingEntry(int bookId, int numPages){
            var entry = new ReadingEntry{
                BookId = bookId,
                Date = DateTime.UtcNow,
                PagesRead = numPages
            };

            await context.ReadingEntries.AddAsync(entry);

            await context.SaveChangesAsync();
        }

        public async Task<ICollection<Course>> GetCourses(int userId)
        {
            return await context.Courses.Where(b => b.UserId == userId).ToListAsync();
        }

        public async Task CreateCourse(int userId, string name, int difficulty)
        {
            var course = new Course {
                UserId = userId, 
                Name = name, 
                Difficulty = difficulty,
                Created = DateTime.UtcNow
            };

            await context.Courses.AddAsync(course);

            await context.SaveChangesAsync();
        }

        public async Task CreateCourseEntry(int courseId, double percentCompleted)
        {
            var entry = new CourseEntry{
                CourseId = courseId,
                Date = DateTime.UtcNow,
                PercentCompleted = percentCompleted
            };

            await context.CourseEntries.AddAsync(entry);

            await context.SaveChangesAsync();
        }
    }
}