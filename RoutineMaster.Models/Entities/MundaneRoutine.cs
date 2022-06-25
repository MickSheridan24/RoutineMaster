namespace RoutineMaster.Models.Entities
{
    public class MundaneRoutine : BaseUserDependentEntity
    {
        public string Name { get; set; }
        public int Difficulty { get; set; }

    }
}