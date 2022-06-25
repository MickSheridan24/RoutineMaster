namespace RoutineMaster.Models.Entities
{
    public class Course : BaseUserDependentEntity
    {
        public string Name { get; set; }
        public int Difficulty {get;set;}
        public ICollection<CourseEntry> Entries { get; set; }
    }
}