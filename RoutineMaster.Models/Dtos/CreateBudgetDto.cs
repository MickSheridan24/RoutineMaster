namespace RoutineMaster.Models.Dtos
{
    public class CreateBudgetDto
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public int? FundId { get; set; }

    }
}