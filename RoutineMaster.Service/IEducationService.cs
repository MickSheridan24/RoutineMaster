using RoutineMaster.Models.Entities;

namespace RoutineMaster.Service
{
    public interface IEducationService
    {
         Task<ICollection<Book>> GetBooks(int userId);
         Task CreateBook(int userId, string name, int difficulty, int totalPages);

         Task CreateReadingEntry(int bookId, int numPages);
        Task<ICollection<Course>> GetCourses(int userId);
        Task CreateCourse(int userId, string name, int difficulty);
        Task CreateCourseEntry(int courseId, double percentCompleted);
    }
}