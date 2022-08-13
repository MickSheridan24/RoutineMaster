using Microsoft.Extensions.Logging;
using RoutineMaster.Data;
using RoutineMaster.Models.Entities;
using RoutineMaster.Models.Enums;

namespace RoutineMaster.Service
{
    public class ScoreService : IScoreService
    {

        private readonly ILogger<ScoreService> logger;
        private RMDataContext context;
        public ScoreService(RMDataContext context, ILogger<ScoreService> logger)
        {
            this.context = context;
            this.logger=logger;
        }
        public async Task SetScore(EScoreType scoreType, int points)
        {
            var td = DateTime.Today; 
            var score = context.DailyScores.FirstOrDefault(s => s.ScoreType == scoreType && s.Date.Day == td.Day && s.Date.Month == td.Month && s.Date.Year == td.Year);
            logger.LogWarning("UPDATING SCORE {type} {score}", scoreType, points);
            if(score != null){
                logger.LogWarning("CURRENT SCORE {score}", score.Score);
                score.Score = points;
                await context.SaveChangesAsync();
            }
            else{
                logger.LogWarning("CREATING SCORE");
                var newScore = new DailyScore{
                    Date = DateTime.UtcNow,
                    Score = points,
                    ScoreType = scoreType,
                    UserId = 1
                };
                context.DailyScores.Add(newScore);
                await context.SaveChangesAsync();
            }
            
        }

        public int GetScore(EScoreType? scoreType)
        {
            return (int)(context.DailyScores
            .Where(s => scoreType == null || s.ScoreType == scoreType)
            .Sum(s => s.Score));
        }
    }
}