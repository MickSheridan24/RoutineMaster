namespace RoutineMaster.Models.Entities
{
    public class MundaneListItem
    {
        public int Id {get; set;}
        public string Name { get; set; }

        public int MundaneListId { get; set; }

        public bool Complete { get; set; }

        public DateTime Created {get;set;}

        public DateTime CompletedOn { get; set;}
    }
}