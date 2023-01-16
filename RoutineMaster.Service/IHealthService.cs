using RoutineMaster.Models.Dtos;
using RoutineMaster.Models.Entities;

namespace RoutineMaster.Service
{
    public interface IHealthService
    {
        Task<ICollection<MealRatingDto>> GetMeals(int userId);
        Task LogMealEntry(int userId, LogMealRatingDto meal);
        Task CreateExerciseRoutine(int userId, ExerciseRoutine routine);
        Task UpdateExerciseRoutine(int userId, int id, ExerciseRoutine routine);
        Task CreateExerciseRoutineEntry(int userId, int id, ExerciseRoutineEntry routine);
        Task<ICollection<ExerciseRoutine>> GetExerciseRoutines(int userId);
        Task<ICollection<ExerciseRoutineEntry>> GetExerciseEntries(int userId);
    }
}