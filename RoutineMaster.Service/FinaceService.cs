using RoutineMaster.Data;
namespace RoutineMaster.Service{
    public class FinanceService : IFinanceService{
        private RMDataContext context;
        public FinanceService(RMDataContext context){
            this.context = context;
        }
    }
}