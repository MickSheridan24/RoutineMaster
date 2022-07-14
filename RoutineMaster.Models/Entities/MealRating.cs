using System.ComponentModel.DataAnnotations.Schema;
using RoutineMaster.Models.Enums;

namespace RoutineMaster.Models.Entities
{
    public class MealRating 
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        
        
        public DateTime Date { get; set; }
        public EMealType MealType {get; set;}
        public int Rating {get; set;}

    }
}