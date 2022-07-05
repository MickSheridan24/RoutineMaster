namespace RoutineMaster.Models.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Difficulty  { get; set; }
        public int TotalPages { get; set; }

        public ICollection<ReadingEntryDto> Entries { get; set; }
    }
}