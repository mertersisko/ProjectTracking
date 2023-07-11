using Microsoft.EntityFrameworkCore;
using ProjectTracking.Bussiness.Repositories.Classes.ProjectNoteService.Abstract;
using ProjectTracking.DataAccess.Context;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.ProjectNoteClasses;
using ProjectTracking.DataAccess.Entites.Classes.DtoClasses.NoteDto;
using ProjectTracking.DataAccess.Entites.Enums;
using ProjectTracking.DataAccess.Entites.ResponseEntites;

namespace ProjectTracking.Bussiness.Repositories.Classes.ProjectNoteService.Concrete;

public class ProjectNoteService : IProjectNoteService
{
    private readonly ProjectTrackingDataContext _dataContext;

    public ProjectNoteService(ProjectTrackingDataContext dataContext)
    {
            _dataContext = dataContext;
    }
    public async Task<BaseResponse<ProjectNote>> Add(ProjectNote model)
    {
        try
        {
            await _dataContext.Notes.AddAsync(model);
            var result = await _dataContext.SaveChangesAsync();

            if(result > 0)
            {
                return new BaseResponse<ProjectNote>()
                {
                    Message = "Kayıt Başarılı",
                    Status = ResultStatus.Success,
                    Title = "Başarı",
                    Url = string.Empty,
                    Data = model
                };
            }
            return new BaseResponse<ProjectNote>()
            {
                Message = "Kayıt sırasında hata oluştu",
                Status = ResultStatus.Error,
                Title = "Hata",
                Url = string.Empty,
                Data = model
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<ProjectNote>()
            {
                Message = $"Kayıt sırasında {e} hatası oluştu",
                Status = ResultStatus.Error,
                Title = "Hata",
                Url = string.Empty,
                Data = model
            };

        }
    }

    public Task<BaseResponse<ProjectNote>> Delete(ProjectNote model)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<ProjectNote>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public  Task<BaseResponse<ProjectNote>>GetList()
    {
        throw new NotImplementedException();

    }

    public Task<BaseResponse<ProjectNote>> GetList(ProjectNote model)
    {
        throw new NotImplementedException();
    }

    public IQueryable<NotListDto> GetNotes(int projectID)
    {
        try
        {
            return _dataContext.Notes.Where(t => t.ProjectId == projectID).Select(s => new NotListDto() 
            { 
                ID = s.Id, 
                Desc = s.ProjectNoteDescription,
                Name = s.ProjectNoteTitle
            });
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    public BaseResponse<ProjectNote> NonDeleted()
    {
        var noteList =  _dataContext.Notes.ToList();
        return new BaseResponse<ProjectNote>()
        {
            DataList = noteList
        };
    }

    public Task<BaseResponse<ProjectNote>> Update(ProjectNote model)
    {
        throw new NotImplementedException();
    }
}
