using ProjectTracking.DataAccess.Entites.Classes.DbClasses.BaseClasses;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.ProjectNoteClasses;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.RequestClasses;
using System.ComponentModel;

namespace ProjectTracking.DataAccess.Entites.Classes.DbClasses.ProjectClasses;

public class Project : BaseEntity
{
    [DisplayName("Proje Adı")]
    public string ProjectName { get; set; }
    [DisplayName("Proje Açıklaması")]
    public string ProjectDescription { get; set; }
    public ICollection<ProjectNote> ProjectNotes { get; set; }
    public ICollection<Requisition> Requisitions { get; set; }
}
