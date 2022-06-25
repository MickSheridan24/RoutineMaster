namespace RoutineMaster.Models.Entities
{
    public class UserIncome: BaseUserDependentEntity
    {
        public int Amount { get; set; }
        public int IncidentalBonus { get; set; }
    }
}