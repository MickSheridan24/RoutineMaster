using RoutineMaster.Data;
using Microsoft.EntityFrameworkCore;
using RoutineMaster.Models.Dtos;
using RoutineMaster.Models.Entities;
using RoutineMaster.Models.Enums;

namespace RoutineMaster.Service{
    public class HealthService : IHealthService{
        private RMDataContext context;
        public HealthService(RMDataContext context){
            this.context = context;
        }

        public async Task CreateExerciseRoutine(int userId, ExerciseRoutine routine)
        {
            routine.UserId = userId;
            context.ExerciseRoutines.Add(routine);
            await context.SaveChangesAsync();
        }

        public async Task CreateExerciseRoutineEntry(int userId, int id, ExerciseRoutineEntry routine)
        {
            context.ExerciseRoutineEntries.Add(routine);
            await context.SaveChangesAsync();
        }

        public async Task<ICollection<ExerciseRoutineEntry>> GetExerciseEntries(int userId)
        {
            return await context.ExerciseRoutineEntries.Where(er => er.Routine.UserId == userId).ToListAsync();
        }

        public async Task<ICollection<ExerciseRoutine>> GetExerciseRoutines(int userId)
        {
            return await context.ExerciseRoutines.Where(er => er.UserId == userId).ToListAsync();
        }

        public async Task<ICollection<MealRatingDto>> GetMeals(int userId)
        {
            return await context.MealRatings.Where(er => er.UserId == userId)
            .Where(m => m.Date.Day == DateTime.Today.Day)
            .Select(m => new MealRatingDto{
                Rating = m.Rating,
                Date = m.Date,
                MealType = m.MealType.ToString()
            })
            .ToListAsync();
        }

        public async Task LogMealEntry(int userId, LogMealRatingDto meal)
        {
            var entity = new MealRating{
                UserId = userId,
                Date = DateTime.UtcNow,
                Rating = meal.Rating,
                MealType = Enum.Parse<EMealType>(meal.MealType)
            };

            context.MealRatings.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateExerciseRoutine(int userId, int id, ExerciseRoutine routine)
        {
            var foundRoutine = await context.ExerciseRoutines.SingleOrDefaultAsync(r => r.Id == id);
            if(routine != default){
                foundRoutine.RequiredOccurrences = routine.RequiredOccurrences;
                foundRoutine.ScheduleType = routine.ScheduleType;
                foundRoutine.Name = routine.Name;
                await context.SaveChangesAsync();
            }
        }
    }
}