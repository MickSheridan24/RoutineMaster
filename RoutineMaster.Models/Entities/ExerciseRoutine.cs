namespace RoutineMaster.Models.Entities
{
    public class ExerciseRoutine : BaseUserDependentEntity
    {
        public string Name { get; set; }
        public int BaseAmount {get; set;}
        public double Scale { get; set; }
    }
}