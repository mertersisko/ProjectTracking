using ProjectTracking.DataAccess.Entites.Classes.DbClasses.MissionClasses;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.RequestClasses;

namespace ProjectTracking.Presentation.Models
{
    public class ViewModel
    {
        public List<Requisition> Requisitions { get; set; }
        public List<Mission> Missions { get; set; }
    }
}
