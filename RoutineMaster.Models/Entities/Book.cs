namespace RoutineMaster.Models.Entities
{
    public class Book : BaseUserDependentEntity
    { 
        public string Name { get; set; }
        public int Difficulty  { get; set; }
        public int TotalPages { get; set; }
    }
}