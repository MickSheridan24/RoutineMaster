using RoutineMaster.Models.Dtos;
using RoutineMaster.Models.Entities;
using RoutineMaster.Models.Enums;

namespace RoutineMaster.Service
{
    public interface IScoreService
    {
        public int GetScore(EScoreType? scoreType);

        public Task SetScore(EScoreType scoreType, int points);
    }
}