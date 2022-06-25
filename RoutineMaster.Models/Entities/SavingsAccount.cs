namespace RoutineMaster.Models.Entities
{
    public class SavingsAccount : BaseUserDependentEntity
    {
        public string Name { get; set; }
        public double Amount {get; set;}
        public double Goal {get; set;}
    }
}