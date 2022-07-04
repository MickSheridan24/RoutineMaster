namespace RoutineMaster.Models.Entities
{
    public class Budget : BaseUserDependentEntity
    {
        public string Name { get; set; }
        public double Amount {get; set;}
        public int? SavingsAccountId { get; set; }

        public SavingsAccount SavingsAccount { get; set; }

        public ICollection<ExpenseEntry> Entries { get; set; } = new List<ExpenseEntry>();
    }
}