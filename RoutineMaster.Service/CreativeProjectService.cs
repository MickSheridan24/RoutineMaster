using RoutineMaster.Data;
namespace RoutineMaster.Service{
    public class CreativeProjectService{
        private RMDataContext context;
        public CreativeProjectService(RMDataContext context){
            this.context = context;
        }
    }
}