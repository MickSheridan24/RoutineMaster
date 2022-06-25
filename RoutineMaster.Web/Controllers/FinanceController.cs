using RoutineMaster.Service;

namespace RoutineMaster.Web.Controllers
{
    public class FinanceController
    {
        private IFinanceService service;

        public FinanceController(IFinanceService service){
            this.service = service;
        }
    }

    
}