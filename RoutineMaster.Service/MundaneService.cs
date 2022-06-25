using System.Data.Entity;
using RoutineMaster.Data;
using RoutineMaster.Models.Entities;

namespace RoutineMaster.Service{
    public class MundaneService : IMundaneService{
        private RMDataContext context;
        public MundaneService(RMDataContext context){
            this.context = context;
        }

        public async Task CompleteListItem(int itemId)
        {
            var item = await context.MundaneListItems.SingleOrDefaultAsync(i => i.Id == itemId);

            item.Complete = true; 
            if(item.CompletedOn == null){
                item.CompletedOn = DateTime.UtcNow;
            }

            await context.SaveChangesAsync();
        }

        public async Task CreateMundaneList(int v, MundaneList list)
        {
            list.UserId = v;
            context.MundaneLists.Add(list);
            await context.SaveChangesAsync();
        }

        public async Task CreateMundaneListItem(int listId, MundaneListItem item)
        {
           item.MundaneListId = listId;
           context.MundaneListItems.Add(item);
           await context.SaveChangesAsync();
        }

        public async Task CreateMundaneRoutine(int v, MundaneRoutine routine)
        {
            routine.UserId = v; 
            context.MundaneRoutines.Add(routine);
            await context.SaveChangesAsync();
        }

        public async Task<ICollection<MundaneList>> GetMundaneLists(int userId)
        {
            return await context.MundaneLists.Where(l => l.UserId == userId).ToListAsync();
        }

        public async Task<ICollection<MundaneRoutine>> GetMundaneRoutines(int userId)
        {
            return await context.MundaneRoutines.Where(l => l.UserId == userId).ToListAsync();
        }
    }
}