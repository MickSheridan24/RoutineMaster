using System.Data.Entity;
using RoutineMaster.Data;
using RoutineMaster.Models.Entities;

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

        public async Task<ICollection<ExerciseRoutine>> GetExerciseRoutines(int userId)
        {
            return await context.ExerciseRoutines.Where(er => er.UserId == userId).ToListAsync();
        }

        public async Task<ICollection<MealRating>> GetMeals(int userId)
        {
            return await context.MealRatings.Where(er => er.UserId == userId).ToListAsync();
        }

        public async Task LogMealEntry(int userId, MealRating meal)
        {
            meal.UserId = userId;
            context.MealRatings.Add(meal);
            await context.SaveChangesAsync();
        }

        public async Task UpdateExerciseRoutine(int userId, int id, ExerciseRoutine routine)
        {
            var foundRoutine = await context.ExerciseRoutines.SingleOrDefaultAsync(r => r.Id == id);
            if(routine != default){
                foundRoutine.Scale = routine.Scale;
                foundRoutine.Name = routine.Name;
                await context.SaveChangesAsync();
            }
        }
    }
}