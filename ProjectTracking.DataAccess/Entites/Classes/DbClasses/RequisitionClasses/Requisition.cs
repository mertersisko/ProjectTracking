using ProjectTracking.DataAccess.Entites.Classes.DbClasses.BaseClasses;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.MissionClasses;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.ProjectClasses;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.UserClasses;
using System.ComponentModel;

namespace ProjectTracking.DataAccess.Entites.Classes.DbClasses.RequestClasses;

public class Requisition : BaseEntity
{
    [DisplayName("Talep Adı")]
    public string RequisitionName { get; set; }
    [DisplayName("Başlangıç Tarihi")]
    public DateTime StartDate { get; set; }
    [DisplayName("Bitiş Tarihi")]
    public DateTime EndDate { get; set; }
    [DisplayName("Talep Açıklaması")]
    public string RequisitionDescription { get; set; }
    [DisplayName("Proje")]
    public int? ProjectId { get; set; }
    public Project Project { get; set; }
    [DisplayName("Durum")]
    public Enums.RequisitionStatus Status { get; set; }
    public int UserId { get; set; }
    [DisplayName("Kullanıcı")]
    public User User { get; set; }
    public ICollection<Mission> Missions { get; set; }
}
