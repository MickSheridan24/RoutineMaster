namespace RoutineMaster.Models.Entities
{
    public class BaseUserDependentEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; }
    }
}