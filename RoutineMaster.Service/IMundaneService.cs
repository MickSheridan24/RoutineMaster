using RoutineMaster.Models.Dtos;
using RoutineMaster.Models.Entities;

namespace RoutineMaster.Service
{
    public interface IMundaneService
    {
        Task<ICollection<MundaneList>> GetMundaneLists(int userId);
        Task<ICollection<MundaneRoutine>> GetMundaneRoutines(int userId);
        Task CreateMundaneList(int v, CreateMundaneListDto list);
        Task CreateMundaneRoutine(int v, MundaneRoutine routine);
        Task CreateMundaneListItem(int listId, MundaneListItem item);
        Task CompleteListItem(int itemId);
        Task DeleteList(int v, int listId);
    }
}