using RoutineMaster.Models.Enums;

namespace RoutineMaster.Models.Entities
{
    public class MealRating 
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public int Date { get; set; }
        public EMealType MealType {get; set;}
        public int Rating {get; set;}

    }
}