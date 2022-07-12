namespace RoutineMaster.Models.Entities
{
    public class ExpenseTag
    {
        public int ExpenseEntryId { get; set; }
        public ExpenseEntry ExpenseEntry { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}