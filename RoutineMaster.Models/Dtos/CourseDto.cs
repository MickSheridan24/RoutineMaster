namespace RoutineMaster.Models.Dtos
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Difficulty  { get; set; }
        public ICollection<CourseEntryDto> Entries { get; set; }
    }
}