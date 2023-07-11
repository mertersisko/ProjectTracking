using ProjectTracking.Bussiness.Repositories.BaseInferfaces;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.ProjectNoteClasses;
using ProjectTracking.DataAccess.Entites.Classes.DtoClasses.NoteDto;
using ProjectTracking.DataAccess.Entites.ResponseEntites;

namespace ProjectTracking.Bussiness.Repositories.Classes.ProjectNoteService.Abstract;

public interface IProjectNoteService : IBaseService<ProjectNote>
{
    public BaseResponse<ProjectNote> NonDeleted();
    /// <summary>
    /// Proje Idsine göre notları getirecek
    /// </summary>
    /// <param name="projectID">ProjeninID'si</param>
    /// <returns></returns>
    IQueryable<NotListDto> GetNotes(int projectID);
}
