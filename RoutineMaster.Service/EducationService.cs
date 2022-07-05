using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RoutineMaster.Data;
using RoutineMaster.Models.Dtos;
using RoutineMaster.Models.Entities;

namespace RoutineMaster.Service{
    public class EducationService : IEducationService{
        private RMDataContext context;
        private ILogger<EducationService> logger;

        public EducationService(RMDataContext context, ILogger<EducationService> logger){
            this.context = context;
            this.logger = logger;
        }

        public async Task<ICollection<BookDto>> GetBooks(int userId)
        {
            return await context.Books
            .Include(b => b.Entries)
            .Where(b => b.UserId == userId)
            .Select(b => new BookDto(){
                Id = b.Id,
                Name = b.Name,
                Difficulty = b.Difficulty,
                TotalPages = b.TotalPages,
                Entries = b.Entries.Select(e => new ReadingEntryDto{
                    Date = e.Date,
                    Id = e.Id,
                    PagesRead = e.PagesRead
                }).ToList()
            })
            .ToListAsync();
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

        public async Task<ICollection<CourseDto>> GetCourses(int userId)
        {
            return await context.Courses
            .Include(c => c.Entries)
            .Where(b => b.UserId == userId)
            .Select(c => new CourseDto{
                Id = c.Id,
                Name = c.Name,
                Difficulty = c.Difficulty,
                Entries = c.Entries.Select(e => new CourseEntryDto{
                    Id= e.Id,
                    PercentCompleted = e.PercentCompleted,
                    Date = e.Date
                }).ToList()
            })
            .ToListAsync();
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

        public async Task DeleteBook(int userId, int bookId){
            var book = context.Books.SingleOrDefault(b => b.Id == bookId && b.UserId == userId );
            logger.LogInformation("DELETING " + bookId);
            if(book != default){
                logger.LogInformation("FOUND " + book.Name);
                context.Books.Remove(book);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteCourse(int userId, int courseId){
            var course = context.Courses.SingleOrDefault(b => b.Id == courseId && b.UserId == userId );
            logger.LogInformation("DELETING " + courseId);
            if(course != default){
                logger.LogInformation("FOUND " + course.Name);
                context.Courses.Remove(course);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteBookEntry(int v, int bookId, int entryId)
        {
            var foundEntry = context.ReadingEntries.SingleOrDefault(e => e.Id == entryId && e.BookId == bookId);

            if(foundEntry != default){
                logger.LogInformation("FOUND READING ENTRY TO DELETE" + foundEntry.Id);

                context.ReadingEntries.Remove(foundEntry);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteCourseEntry(int v, int courseId, int entryId)
        {
            var foundEntry = context.CourseEntries.SingleOrDefault(e => e.Id == entryId && e.CourseId == courseId);

            if(foundEntry != default){
                logger.LogInformation("FOUND Course ENTRY TO DELETE" + foundEntry.Id);

                context.CourseEntries.Remove(foundEntry);
                await context.SaveChangesAsync();
            }
        }
    }
}