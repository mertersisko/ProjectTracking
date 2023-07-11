using Microsoft.EntityFrameworkCore;
using ProjectTracking.Bussiness.Repositories.Classes.ProjectServices.Abstract;
using ProjectTracking.DataAccess.Context;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.ProjectClasses;
using ProjectTracking.DataAccess.Entites.ResponseEntites;

namespace ProjectTracking.Bussiness.Repositories.Classes.ProjectServices.Concrete;

public class ProjectService : IProjectService
{
    private readonly ProjectTrackingDataContext _dataContext;

    public ProjectService(ProjectTrackingDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<BaseResponse<Project>> ActiveProject()
    {
        var projectList = await _dataContext.Projects.Where(t => t.Active && !t.Deleted).ToListAsync();

        return new BaseResponse<Project>()
        {
            DataList = projectList
        };
    }

    public async Task<BaseResponse<Project>> Add(Project model)
    {
        try
        {
            await _dataContext.Projects.AddAsync(model);

            var result = await _dataContext.SaveChangesAsync();

            if(result > 0)
            {
                return new BaseResponse<Project>
                {
                    Message = "Kayıt başarılı",
                    Title = "Başarı",
                    Data = model,
                    Status = DataAccess.Entites.Enums.ResultStatus.Success,
                    Url = "/Project/Index"
                };
            }
            return new BaseResponse<Project>
            {
                Message = "Kayıt başarısız",
                Title = "Hata",
                Data = model,
                Status = DataAccess.Entites.Enums.ResultStatus.Error,
                Url = string.Empty
            };
        }
        catch (Exception e)
        {

            return new BaseResponse<Project>
            {
                Message = $"Kayıt sırasında {e} hatasıyla karşılaşıldı",
                Title = "Hata",
                Data = model,
                Status = DataAccess.Entites.Enums.ResultStatus.Error,
                Url = string.Empty
            };
        }
    }

    public Task<BaseResponse<Project>> Delete(Project model)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<Project>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<Project>> GetList(Project model)
    {
        var projectList = await _dataContext.Projects.ToListAsync();

        return new BaseResponse<Project>()
        {
            DataList = projectList
        };
    }

    public async Task<BaseResponse<Project>> Update(Project model)
    {
        var currentProject = await _dataContext.Projects.FindAsync(model.Id);

        try
        {
            currentProject.ProjectDescription = model.ProjectDescription;
            currentProject.ProjectName = model.ProjectName;

            _dataContext.Projects.Update(currentProject);

            var result = await _dataContext.SaveChangesAsync();

            if(result > 0)
            {
                return new BaseResponse<Project>
                {
                    Message = "Proje kaydı başarılı",
                    Title = "Başarı",
                    Status = DataAccess.Entites.Enums.ResultStatus.Success,
                    Url = "/Project/Index",
                    Data = currentProject
                };
            }
            return new BaseResponse<Project>
            {
                Message = "Proje kaydı sırasında hata oluştu",
                Title = "Hata",
                Status = DataAccess.Entites.Enums.ResultStatus.Error,
                Url = string.Empty,
                Data = null
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<Project>
            {
                Message = $"Proje kaydı sırasında {e} hatası oluştu",
                Title = "Hata",
                Status = DataAccess.Entites.Enums.ResultStatus.Error,
                Url = string.Empty,
                Data = null
            };
        }
    }
}
