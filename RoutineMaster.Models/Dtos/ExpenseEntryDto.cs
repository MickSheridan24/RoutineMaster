namespace RoutineMaster.Models.Dtos
{
    public class ExpenseEntryDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int? BudgetId {get; set;}
        public string? BudgetName {get; set;}

        public ICollection<TagDto> Tags {get; set;}
        public double Amount {get; set;}
        public string Name { get; set; }
        
    }
}