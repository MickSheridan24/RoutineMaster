namespace RoutineMaster.Models.Entities
{
    public class CourseEntry 
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public double PercentCompleted { get; set; }
    }
}