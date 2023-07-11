using ProjectTracking.DataAccess.Entites.Classes.DbClasses.BaseClasses;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.ProjectClasses;
using System.ComponentModel;

namespace ProjectTracking.DataAccess.Entites.Classes.DbClasses.ProjectNoteClasses;

public class ProjectNote : BaseEntity
{
    [DisplayName("Not Başlığı")]
    public string ProjectNoteTitle { get; set; }
    [DisplayName("Not Açıklaması")]
    public string ProjectNoteDescription { get; set; }
    public int? ProjectId { get; set; }
    public Project Project { get; set; }
}
