using ProjectTracking.DataAccess.Entites.Classes.DbClasses.BaseClasses;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.ProjectClasses;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.RequestClasses;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.UserClasses;
using System.ComponentModel;

namespace ProjectTracking.DataAccess.Entites.Classes.DbClasses.MissionClasses;

public class Mission : BaseEntity
{
    [DisplayName("Başlık")]
    public string Title { get; set; }
	[DisplayName("Başlangıç Tarihi")]
	public DateTime StartDate { get; set; }
	[DisplayName("Bitiş Tarihi")]
	public DateTime EndDate { get; set; }
	[DisplayName("Açıklama")]
	public string Description { get; set; }
    [DisplayName("Atanan")]
    public int UserId { get; set; }
	public User User { get; set; }
    [DisplayName("Talebi")]
    public int RequisitionId { get; set; }
	public Requisition Requisition { get; set; }
    [DisplayName("Durum")]
    public Enums.MissionStatus Status { get; set; }


}
