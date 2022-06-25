namespace RoutineMaster.Models.Entities
{
    public class ExpenseEntry
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int? BudgetId {get; set;}
        public Budget Budget {get; set;}
        public double Amount {get; set;}
    }
}