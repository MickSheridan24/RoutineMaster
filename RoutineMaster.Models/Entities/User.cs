namespace RoutineMaster.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }

        public ICollection<Budget> Budgets { get; set; } = new List<Budget>();
        public ICollection<ExpenseEntry> ExpenseEntries { get; set; } = new List<ExpenseEntry>();

        public UserIncome UserIncome {get; set;}
    }
}