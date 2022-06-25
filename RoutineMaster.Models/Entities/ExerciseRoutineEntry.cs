namespace RoutineMaster.Models.Entities
{
    public class ExerciseRoutineEntry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ExerciseRoutineId { get; set; }
        public ExerciseRoutine Routine { get; set; }
        public int Score { get; set; }
    }
}