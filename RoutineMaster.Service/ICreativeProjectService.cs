using RoutineMaster.Models.Entities;

namespace RoutineMaster.Service
{
    public interface ICreativeProjectService
    {
        Task<ICollection<CreativeProject>> GetProjects(int userId);
        Task CreateProject(int userId, CreativeProject project);
        Task UpdateProject(int userId, int id, CreativeProject project);
        Task DeleteProject(int userId, int id);
        Task CreateProjectEntry(int userId, int projectId, CreativeProjectEntry entry);
        Task DeleteProjectEntry(int v, int id, int entryId);
    }
}