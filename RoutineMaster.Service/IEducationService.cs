using RoutineMaster.Models.Dtos;
using RoutineMaster.Models.Entities;

namespace RoutineMaster.Service
{
    public interface IEducationService
    {
         Task<ICollection<BookDto>> GetBooks(int userId);
         Task CreateBook(int userId, string name, int difficulty, int totalPages);

         Task CreateReadingEntry(int bookId, int numPages);
        Task<ICollection<CourseDto>> GetCourses(int userId);
        Task CreateCourse(int userId, string name, int difficulty);
        Task CreateCourseEntry(int courseId, double percentCompleted);
        Task DeleteBook(int userId, int bookId);
        Task DeleteBookEntry(int v, int bookId, int entryId);
        Task DeleteCourse(int userId, int courseId);
        Task DeleteCourseEntry(int v, int courseId, int entryId);
        Task<IEnumerable<ReadingStatsDto>> GetReadingSummary();
        Task<IEnumerable<CourseStatsDto>> GetCourseSummary();
    }
}