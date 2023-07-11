using ProjectTracking.DataAccess.Entites.Classes.DbClasses.ProjectClasses;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.ProjectNoteClasses;

namespace ProjectTracking.DataAccess.Entites.Classes.ViewModel
{
    public class ProjectAndProjectNoteViewModel
    {
        public ICollection<Project> Projects { get; set; }

        public ICollection<ProjectNote> ProjectNotes { get; set; }
    }
}
