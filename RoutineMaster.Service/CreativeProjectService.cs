using Microsoft.EntityFrameworkCore;
using RoutineMaster.Data;
using RoutineMaster.Models.Entities;

namespace RoutineMaster.Service{
    public class CreativeProjectService : ICreativeProjectService{
        private RMDataContext context;
        public CreativeProjectService(RMDataContext context){
            this.context = context;
        }

        public async Task CreateProject(int userId, CreativeProject project)
        {
            project.UserId = userId;
            context.CreativeProjects.Add(project);
            await context.SaveChangesAsync();
        }

        public async Task CreateProjectEntry(int userId, int projectId, CreativeProjectEntry entry)
        {
            context.CreativeProjectEntries.Add(entry);
            await context.SaveChangesAsync();
        }

        public async Task DeleteProject(int userId, int id)
        {
            var foundProject = await context.CreativeProjects.SingleOrDefaultAsync(p => p.UserId == userId && p.Id == id);
            if(foundProject != default){
                context.CreativeProjects.Remove(foundProject);
                await context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<CreativeProject>> GetProjects(int userId)
        {
            return await context.CreativeProjects
            .Include(c => c.Entries)
            .Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task UpdateProject(int userId, int id, CreativeProject project)
        {
             var foundProject = await context.CreativeProjects.SingleOrDefaultAsync(p => p.UserId == userId && p.Id == id);
             if(foundProject != default){
                 foundProject.Description = project.Description;
                 foundProject.Name = project.Name;
                 await context.SaveChangesAsync();
             }
        }
    }
}