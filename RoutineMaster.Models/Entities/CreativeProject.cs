namespace RoutineMaster.Models.Entities
{
    public class CreativeProject : BaseUserDependentEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<CreativeProjectEntry> Entries {get; set;}
    }
}