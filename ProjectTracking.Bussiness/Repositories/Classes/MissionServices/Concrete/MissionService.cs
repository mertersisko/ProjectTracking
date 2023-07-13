using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Bussiness.Repositories.Classes.MissionServices.Abstract;
using ProjectTracking.Bussiness.Session;
using ProjectTracking.DataAccess.Context;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.MissionClasses;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.RequestClasses;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.UserClasses;
using ProjectTracking.DataAccess.Entites.Enums;
using ProjectTracking.DataAccess.Entites.ResponseEntites;

namespace ProjectTracking.Bussiness.Repositories.Classes.MissionServices.Concrete;

public class MissionService : IMissionService
{
    private readonly ProjectTrackingDataContext _dataContext;

    public MissionService(ProjectTrackingDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<BaseResponse<Mission>> Add(Mission model)
    {
        try
        {
            await _dataContext.Missions.AddAsync(model);
            var result = await _dataContext.SaveChangesAsync();

            if (result > 0)
            {
                return new BaseResponse<Mission>()
                {
                    Status = DataAccess.Entites.Enums.ResultStatus.Success,
                    Message = "Kullanıcı ekleme başarılı",
                    Data = model,
                    Title = "Başarılı",
                    Url = "/User/Index"
                };
            }
            return new BaseResponse<Mission>()
            {
                Status = DataAccess.Entites.Enums.ResultStatus.Error,
                Message = "Kullanıcı ekleme sırasında hata oluştu",
                Data = model,
                Title = "Hata",
                Url = string.Empty
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<Mission>()
            {
                Status = DataAccess.Entites.Enums.ResultStatus.Error,
                Message = "Kullanıcı ekleme sırasında hata oluştu",
                Data = model,
                Title = "Hata",
                Url = string.Empty
            };
        }
    }

    public Task<BaseResponse<Mission>> Delete(Mission model)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<Mission>> DeleteById(int id)
    {
        var mission = await GetById(id);

        try
        {
            if (mission.Data != null)
            {
                _dataContext.Missions.Remove(mission.Data);

                var result = await _dataContext.SaveChangesAsync();

                if (result > 0)
                {
                    return new BaseResponse<Mission>()
                    {
                        Message = "Silme başarılı",
                        Title = "Başarı",
                        Status = ResultStatus.Success,
                        Data = null,
                        Url = string.Empty
                    };
                }

            }
            return new BaseResponse<Mission>()
            {
                Message = "Silme ilemi sırasında hata oluştu",
                Title = "Hata",
                Status = ResultStatus.Error,
                Data = null,
                Url = string.Empty
            };
        }
        catch (Exception ex)
        {

            return new BaseResponse<Mission>()
            {
                Message = $"Silme ilemi sırasında {ex} hatası oluştu",
                Title = "Hata",
                Status = ResultStatus.Error,
                Data = null,
                Url = string.Empty
            };
        }
    }

    public async Task<BaseResponse<Mission>> GetById(int id)
    {
        var currentMission = await _dataContext.Missions.FindAsync(id);
        return new BaseResponse<Mission>()
        {
            Data = currentMission,
        };
    }

    public async Task<BaseResponse<Mission>> GetList(Mission model)
    {
        var missionList = await _dataContext.Missions.ToListAsync();

        return new BaseResponse<Mission>()
        {
            DataList = missionList,
        };
    }

    public async Task<BaseResponse<Mission>> GetMissionAsync()
    {
        var missionList = await _dataContext.Missions.Where(t => t.Active && !t.Deleted).ToListAsync();

        return new BaseResponse<Mission>()
        {
            DataList = missionList
        };
    }

    public Task<BaseResponse<Mission>> Update(Mission model)
    {
        throw new NotImplementedException();
    }
}
