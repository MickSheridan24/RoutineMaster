namespace RoutineMaster.Models.Entities
{
    public class MundaneList : BaseUserDependentEntity
    {
        public string Name { get; set; }

        public ICollection<MundaneListItem> Items {get; set;} = new List<MundaneListItem>();
    }
}