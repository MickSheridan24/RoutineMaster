using Microsoft.AspNetCore.Mvc;
using RoutineMaster.Models.Entities;
using RoutineMaster.Service;

namespace RoutineMaster.Web.Controllers
{
    public class MundaneController
    {
        private IMundaneService service;

        public MundaneController(IMundaneService service){
            this.service = service;
        }
    

        [HttpGet ("lists")]
        public async Task<IActionResult> GetMundaneLists(){
            return new JsonResult(await service.GetMundaneLists(1));
        }


        [HttpGet ("routines")]
        public async Task<IActionResult> GetMundaneRoutines(){
            return new JsonResult(await service.GetMundaneRoutines(1));
        }


        [HttpPost("lists")]
        public async Task<IActionResult> CreateMundaneList([FromBody] MundaneList list){
            await service.CreateMundaneList(1, list);
            return new OkResult();
        }

        [HttpPost("routines")]
        public async Task<IActionResult> CreateMundaneRoutine([FromBody] MundaneRoutine routine){
            await service.CreateMundaneRoutine(1, routine);
            return new OkResult();
        }

        [HttpPost("lists/{id}/items")]
        public async Task<IActionResult> CreateMundaneListItem([FromRoute] int listId, [FromBody] MundaneListItem item){
            await service.CreateMundaneListItem(listId, item);
            return new OkResult();
        }

        [HttpPut("lists/items/{itemId}/complete")]
        public async Task<IActionResult> CompleteListItem([FromRoute] int itemId){
            await service.CompleteListItem(itemId);
            return new OkResult();
        }
    }

}