namespace RoutineMaster.Models.Dtos
{
    public class BudgetDto
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public double Amount { get; set; }
        public double Spent { get; set; }
        
        public int? FundId {get; set;}

        public string? FundName { get; set; }

    }
}