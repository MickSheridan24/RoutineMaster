using RoutineMaster.Data;
namespace RoutineMaster.Service{
    public class HealthService{
        private RMDataContext context;
        public HealthService(RMDataContext context){
            this.context = context;
        }
    }
}