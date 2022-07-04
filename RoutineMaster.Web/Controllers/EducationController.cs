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


        [HttpGet("books/{id}/entries")]
        public async Task<IActionResult> CreateReadingEntry([FromRoute] int bookId, [FromBody] ReadingEntry entry){
            await service.CreateReadingEntry(bookId, entry.PagesRead);

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


        [HttpGet("courses/{id}/entries")]
        public async Task<IActionResult> CreateCourseEntry([FromRoute] int courseId, [FromBody] CourseEntry entry){
            await service.CreateCourseEntry(courseId, entry.PercentCompleted);

            return new OkResult();
        }

        [HttpDelete("books/{bookId}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int bookId){
            await service.DeleteBook(1, bookId);
            return new OkResult();
        }
    }


}