using Microsoft.AspNetCore.Mvc;
using RoutineMaster.Models.Entities;
using RoutineMaster.Service;

namespace RoutineMaster.Web.Controllers
{
    public class HealthController
    {
        
        private IHealthService  service;
    
        public HealthController(IHealthService service){
            this.service = service;
        }

        [HttpGet("meals")]
        public async Task<IActionResult> GetMeals(){
            return new JsonResult(await service.GetMeals(1));
        }

        [HttpGet("exerciseRoutines")]
        public async Task<IActionResult> GetExerciseRoutines(){
            return new JsonResult(await service.GetExerciseRoutines(1));
        }

        [HttpPost("meals")]
        public async Task<IActionResult> LogMealEntry([FromBody] MealRating meal){
            await service.LogMealEntry(1, meal);
            return new OkResult();
        }

        [HttpPost("exerciseRoutines")]
        public async Task<IActionResult> CreateExerciseRoutine([FromBody] ExerciseRoutine routine){
            await service.CreateExerciseRoutine(1, routine);
            return new OkResult();
        }

        [HttpPut("exerciseRoutines/{id}")]
        public async Task<IActionResult> UpdateExerciseRoutine([FromRoute] int id, [FromBody] ExerciseRoutine routine){
            await service.UpdateExerciseRoutine(1, id, routine);
            return new OkResult();
        }


        [HttpPost("exerciseRoutines/{id}/entries")]
        public async Task<IActionResult> CreateExerciseRoutineEntry([FromRoute] int id, [FromBody] ExerciseRoutineEntry routine){
            await service.CreateExerciseRoutineEntry(1, id, routine);
            return new OkResult();
        }
    }
}