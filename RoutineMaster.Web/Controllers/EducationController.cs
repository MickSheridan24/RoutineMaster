using Microsoft.AspNetCore.Mvc;
using RoutineMaster.Models.Entities;
using RoutineMaster.Service;

namespace RoutineMaster.Web.Controllers
{
    public class EducationController
    {

        private IEducationService service; 

        public EducationController(IEducationService service){
            this.service = service;
        }

        [HttpGet("books")]
        public async Task<IActionResult> GetBooks(){
            var result = await service.GetBooks(1);

            return new JsonResult(result);
        }


        [HttpPost("books")]
        public async Task<IActionResult> CreateBook([FromBody] Book book){
            await service.CreateBook(1, book.Name, book.Difficulty, book.TotalPages);

            return new OkResult();
        }


        [HttpPost("books/{id}/entries")]
        public async Task<IActionResult> CreateReadingEntry([FromRoute] int id, [FromBody] ReadingEntry entry){
            await service.CreateReadingEntry(id, entry.PagesRead);

            return new OkResult();
        }


        [HttpGet("courses")]
        public async Task<IActionResult> GetCourses(){
            var result = await service.GetCourses(1);

            return new JsonResult(result);
        }


        [HttpPost("courses")]
        public async Task<IActionResult> CreateCourse([FromBody] Course course){
            await service.CreateCourse(1, course.Name, course.Difficulty);

            return new OkResult();
        }


        [HttpPost("courses/{id}/entries")]
        public async Task<IActionResult> CreateCourseEntry([FromRoute] int id, [FromBody] CourseEntry entry){
            await service.CreateCourseEntry(id, entry.PercentCompleted);

            return new OkResult();
        }

        [HttpDelete("books/{bookId}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int bookId){
            await service.DeleteBook(1, bookId);
            return new OkResult();
        }

        [HttpDelete("books/{bookId}/entries/{entryId}")]
        public async Task<IActionResult> DeleteBookEntry([FromRoute] int bookId, [FromRoute] int entryId){
            await service.DeleteBookEntry(1, bookId, entryId);
            return new OkResult();
        }

        [HttpDelete("courses/{courseId}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] int courseId){
            await service.DeleteCourse(1, courseId);
            return new OkResult();
        }

        [HttpDelete("courses/{courseId}/entries/{entryId}")]
        public async Task<IActionResult> DeleteCourseEntry([FromRoute] int courseId, [FromRoute] int entryId){
            await service.DeleteCourseEntry(1, courseId, entryId);
            return new OkResult();
        }
    }


}