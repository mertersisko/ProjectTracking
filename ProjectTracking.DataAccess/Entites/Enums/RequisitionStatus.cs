using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectTracking.DataAccess.Entites.Enums;

public enum RequisitionStatus
{
    [Display(Name = "Bekliyor")]
    Pending = 1,
    [Display(Name = "Atandı")]
    AssignedTo = 2,
   
}
