using RoutineMaster.Models.Enums;

namespace RoutineMaster.Models.Entities
{
    public class DailyScore
    {
        public int Id { get; set; }
        public EScoreType ScoreType { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public double Score { get; set; }
    }
}