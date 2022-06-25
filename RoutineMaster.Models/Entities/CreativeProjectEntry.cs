namespace RoutineMaster.Models.Entities
{
    public class CreativeProjectEntry 
    {
        public int Id { get; set; }
        public int CreativeProjectId { get; set; }
        public DateTime Date { get; set; }
        public double  PercentCompleted { get; set; }
        public string Description { get; set; }
    }
}