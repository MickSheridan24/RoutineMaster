namespace RoutineMaster.Models.Entities
{
    public class MundaneRoutineEntry 
    {
        public int Id { get; set; }
        public int RoutineId { get; set; }
        public MundaneRoutine Routine { get; set; }
        public DateTime Date {get; set;}

    }
}