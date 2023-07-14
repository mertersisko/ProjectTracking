using ProjectTracking.Bussiness.Repositories.BaseInferfaces;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.ProjectClasses;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.RequestClasses;
using ProjectTracking.DataAccess.Entites.ResponseEntites;

namespace ProjectTracking.Bussiness.Repositories.Classes.ProjectServices.Abstract;

public interface IProjectService : IBaseService<Project>
{
    public Task<BaseResponse<Project>> ActiveProject();
    public Task<BaseResponse<Project>> DeleteById(int id);

}
