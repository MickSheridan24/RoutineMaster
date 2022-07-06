using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RoutineMaster.Data;
using RoutineMaster.Models.Dtos;
using RoutineMaster.Models.Entities;

namespace RoutineMaster.Service
{
    public class MundaneService : IMundaneService
    {
        private RMDataContext context;
        private readonly ILogger<RMDataContext> logger;

        public MundaneService(RMDataContext context, ILogger<RMDataContext> logger)
        {
            this.context = context;
            this.logger =logger;
        }

    public async Task CompleteListItem(int itemId)
    {
        var item = await context.MundaneListItems.SingleOrDefaultAsync(i => i.Id == itemId);

        item.Complete = true;
        if (item.CompletedOn == null)
        {
            item.CompletedOn = DateTime.UtcNow;
        }

        await context.SaveChangesAsync();
    }

    public async Task CreateMundaneList(int v, CreateMundaneListDto list)
    {
        context.MundaneLists.Add(new MundaneList
        {
            Name = list.Name,
            UserId = v,
            Created = DateTime.UtcNow
        });
        await context.SaveChangesAsync();
    }

    public async Task CreateMundaneListItem(int listId, MundaneListItem item)
    {
        logger.LogInformation("CREATING LIST ITEM {listId} {itemName}", listId, item.Name);
        item.MundaneListId = listId;
        item.Created = DateTime.UtcNow;
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
        return await context.MundaneLists
        .Include(m => m.Items.Where(i => !i.Complete))
        .Where(l => l.UserId == userId).ToListAsync();
    }

    public async Task<ICollection<MundaneRoutine>> GetMundaneRoutines(int userId)
    {
        return await context.MundaneRoutines.Where(l => l.UserId == userId).ToListAsync();
    }

    public async Task DeleteList(int userId, int listId)
    {
        logger.LogInformation("DELETING LIST " + listId);
        var found = await context.MundaneLists.SingleOrDefaultAsync(l => l.Id == listId && l.UserId == userId);

        if (found != default)
        {
            logger.LogInformation("FOUND" + found.Name);
                context.MundaneLists.Remove(found);
            await context.SaveChangesAsync();
        }
    }
}
}