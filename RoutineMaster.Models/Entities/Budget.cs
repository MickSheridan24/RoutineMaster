namespace RoutineMaster.Models.Entities
{
    public class Budget : BaseUserDependentEntity
    {
        public string Name { get; set; }
        public double Amount {get; set;}
        public int? SavingsAccountId { get; set; }
    }
}