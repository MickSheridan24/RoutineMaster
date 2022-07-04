namespace RoutineMaster.Models.Dtos
{
    public class UserIncomeDto
    {
        public double Amount { get; set; }
        public double IncidentalBonus { get; set; }

        public double Remaining { get; set; }
        public double Budgeted { get; set; }
    }
}