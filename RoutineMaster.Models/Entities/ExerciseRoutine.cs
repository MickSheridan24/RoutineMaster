using RoutineMaster.Models.Enums;

namespace RoutineMaster.Models.Entities
{
    public class ExerciseRoutine : BaseUserDependentEntity
    {
        public string Name { get; set; }
        
        public int RequiredOccurrences { get; set; }

        public EScheduleType ScheduleType {get; set;}

        
    }
}