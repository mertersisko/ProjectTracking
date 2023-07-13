using ProjectTracking.Bussiness.Repositories.BaseInferfaces;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.MissionClasses;
using ProjectTracking.DataAccess.Entites.ResponseEntites;

namespace ProjectTracking.Bussiness.Repositories.Classes.MissionServices.Abstract;

public interface IMissionService : IBaseService<Mission>
{
    public Task<BaseResponse<Mission>> GetMissionAsync();

    public Task<BaseResponse<Mission>> DeleteById(int id);
}
