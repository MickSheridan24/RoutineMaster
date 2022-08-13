using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RoutineMaster.Data;
using RoutineMaster.Models.Dtos;
using RoutineMaster.Models.Entities;

namespace RoutineMaster.Service{
    public class EducationService : IEducationService{
        private RMDataContext context;
        private ILogger<EducationService> logger;
        private IScoreService scoreService;

        public EducationService(RMDataContext context, IScoreService scoreService, ILogger<EducationService> logger){
            this.context = context;
            this.logger = logger;
            this.scoreService = scoreService;
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
                })
                .OrderByDescending(e => e.Date)
                .ToList()
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
            await UpdateScore();

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
                })
                .OrderByDescending(e => e.Date)
                .ToList()
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
            await UpdateScore();

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
            await UpdateScore();

        }

        public async Task DeleteCourseEntry(int v, int courseId, int entryId)
        {
            var foundEntry = context.CourseEntries.SingleOrDefault(e => e.Id == entryId && e.CourseId == courseId);

            if(foundEntry != default){
                logger.LogInformation("FOUND Course ENTRY TO DELETE" + foundEntry.Id);

                context.CourseEntries.Remove(foundEntry);
                await context.SaveChangesAsync();
            }

            await UpdateScore();
        }

        public async Task<IEnumerable<ReadingStatsDto>> GetReadingSummary(){
           
            var ret = new List<ReadingStatsDto>();
            for(var i = 0; i < DateTime.UtcNow.Day; i++){

                var daySummary = new ReadingStatsDto{
                    Present = context.ReadingEntries.Where(e => e.Date.Day <= i + 1 && e.Date.Month == DateTime.UtcNow.Month && e.Date.Year == DateTime.UtcNow.Year)
                    .Sum(e => e.PagesRead),
                    LastMonth = context.ReadingEntries.Where(e => e.Date.Day <= i + 1 && e.Date.Month == DateTime.UtcNow.AddMonths(-1).Month && e.Date.Year == DateTime.UtcNow.Year)
                    .Sum(e => e.PagesRead),
                    Average = CalculateAvgPagesReadToDay(i + 1, DateTime.Parse("07-01-2022"))
                };
                ret.Add(daySummary);
            }

            return ret;
           
        }

        private double CalculateAvgPagesReadToDay(int day, DateTime? startDate = null){
            var groupedByMonth = context.ReadingEntries
            .Where(e => e.Date.Year == DateTime.UtcNow.Year && e.Date.Day <= day) 
            .ToList()
            .GroupBy(e => e.Date.Month + " " + e.Date.Year, e => e);

            var sums = groupedByMonth.Select(g => g.Sum(e => e.PagesRead));

            if (startDate == null) startDate = DateTime.Parse(DateTime.UtcNow.Year.ToString());

            var emptyMonths = 0;

            for(var i = startDate.Value.Month; i <= DateTime.UtcNow.Month; i++){
                if(!context.ReadingEntries.Any(e => e.Date.Day <= day && e.Date.Month == i && e.Date.Year == startDate.Value.Year)){
                    logger.LogCritical("EMPTY MONTH {m}", i);
                    emptyMonths ++;
                }
            }


            return  groupedByMonth.Count() > 0 ? sums.Sum() / (groupedByMonth.Count() + emptyMonths) : 0;
        }    

        private bool IsCurrentMonth(DateTime date)    {
            return date.Month == DateTime.UtcNow.Month && date.Year == DateTime.UtcNow.Year;
        }

        public async Task<IEnumerable<CourseStatsDto>> GetCourseSummary()
        {
            var ret = new List<CourseStatsDto>();
            for(var i = 0; i < DateTime.UtcNow.Day; i++){

                var daySummary = new CourseStatsDto{
                    Present = context.CourseEntries.Where(e => e.Date.Day <= i + 1 && e.Date.Month == DateTime.UtcNow.Month && e.Date.Year == DateTime.UtcNow.Year)
                    .Sum(e => e.PercentCompleted),
                    LastMonth = context.CourseEntries.Where(e => e.Date.Day <= i + 1 && e.Date.Month == DateTime.UtcNow.AddMonths(-1).Month && e.Date.Year == DateTime.UtcNow.Year)
                    .Sum(e => e.PercentCompleted),
                    Average = CalculateAvgCourseProgressToDay(i + 1, DateTime.Parse("07-01-2022"))
                };
                ret.Add(daySummary);
            }

            return ret;
        }

        private double CalculateAvgCourseProgressToDay(int day, DateTime? startDate = null){
            var groupedByMonth = context.CourseEntries
            .Where(e => e.Date.Year == DateTime.UtcNow.Year && e.Date.Day <= day) 
            .ToList()
            .GroupBy(e => e.Date.Month + " " + e.Date.Year, e => e);

            var sums = groupedByMonth.Select(g => g.Sum(e => e.PercentCompleted));

            if (startDate == null) startDate = DateTime.Parse(DateTime.UtcNow.Year.ToString());

            var emptyMonths = 0;

            for(var i = startDate.Value.Month; i <= DateTime.UtcNow.Month; i++){
                if(!context.CourseEntries.Any(e => e.Date.Day <= day && e.Date.Month == i && e.Date.Year == startDate.Value.Year)){
                    logger.LogCritical("EMPTY MONTH {m}", i);
                    emptyMonths ++;
                }
            }


            return  groupedByMonth.Count() > 0 ? sums.Sum() / (groupedByMonth.Count() + emptyMonths) : 0;
        }    

        private async Task UpdateScore(){
            var today = DateTime.Now;
            var pageCount = context.ReadingEntries
            .Where(e => e.Date.Year == today.Year 
            && e.Date.Month == today.Month 
            && e.Date.Day == today.Day)
            .Sum(r => r.PagesRead);

            var readingCount = pageCount > 0 ? 1 + pageCount / 15 : 0;

            var courseCount = await context.CourseEntries
            .Where(e => e.Date.Year == today.Year 
            && e.Date.Month == today.Month 
            && e.Date.Day == today.Day).CountAsync();


            var score = readingCount + courseCount + (readingCount * courseCount);         



            await scoreService.SetScore(Models.Enums.EScoreType.LEARNING, score);
            
        }

    }
}