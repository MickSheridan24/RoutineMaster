namespace RoutineMaster.Models.Dtos
{
    public class CreateExpenseEntryDto
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public int? BudgetId { get; set; }
        public ICollection<TagDto> Tags { get; set; } = new List<TagDto>();
    }
}