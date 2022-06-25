using Microsoft.AspNetCore.Mvc;
using RoutineMaster.Models.Entities;
using RoutineMaster.Service;

namespace RoutineMaster.Web.Controllers
{
    public class CreativeProjectController
    {
        private ICreativeProjectService service; 

        public CreativeProjectController(ICreativeProjectService service){
            this.service = service;
        }

        [HttpGet("projects")]
        public async Task<IActionResult>  GetProjects(){
            return new JsonResult(await service.GetProjects(1));
        }


        [HttpPost("projects")]
        public async Task<IActionResult> CreateProject([FromBody] CreativeProject project){
            await service.CreateProject(1, project);
            return new OkResult();
        }
        
        [HttpPut("projects/{id}")]
        public async Task<IActionResult> UpdateProject([FromRoute] int id, [FromBody] CreativeProject project){
            await service.UpdateProject(1, id, project);
            return new OkResult();
        }

        [HttpDelete("projects/{id}")]
        public async Task<IActionResult> DeleteProject([FromRoute] int id){
            await service.DeleteProject(1, id);
            return new OkResult();
        }

        [HttpPost("projects/{id}/entries")]
        public async Task<IActionResult> CreateProjectEntry([FromRoute] int projectId, [FromBody] CreativeProjectEntry entry){
            await service.CreateProjectEntry(1, projectId, entry);
            return new OkResult();
        }
    }
}