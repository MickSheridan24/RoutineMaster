namespace RoutineMaster.Models.Entities
{
    public class ReadingEntry 
    {
        public int Id { get; set; }
        public DateTime Date {get; set;}
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int PagesRead {get; set;}

    }
}