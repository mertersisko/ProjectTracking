using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Bussiness.Repositories.Classes.RequisitionServices.Abstract;
using ProjectTracking.DataAccess.Context;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.RequestClasses;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.UserClasses;
using ProjectTracking.DataAccess.Entites.Enums;
using ProjectTracking.DataAccess.Entites.ResponseEntites;

namespace ProjectTracking.Bussiness.Repositories.Classes.RequisitionServices.Concrete;

public class RequisitionService : IRequisitionService
{
    private readonly ProjectTrackingDataContext _dataContext;

    public RequisitionService(ProjectTrackingDataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<BaseResponse<Requisition>> Add(Requisition model)
    {
        try
        {

            model.EndDate = model.StartDate.AddDays(7);
            model.Status = RequisitionStatus.Pending;
            await _dataContext.Requisitions.AddAsync(model);
            var result = await _dataContext.SaveChangesAsync();

            if (result > 0)
            {
                return new BaseResponse<Requisition>()
                {
                    Message = "Talep girişi başarılı",
                    Title = "Başarı",
                    Status = DataAccess.Entites.Enums.ResultStatus.Success,
                    Data = model,
                    Url = "/Requisition/Index"

                };
            }
            return new BaseResponse<Requisition>()
            {
                Message = "Talep girişi sırasında hata oluştu",
                Title = "Uyarı",
                Status = DataAccess.Entites.Enums.ResultStatus.Warning,
                Data = model,
                Url = string.Empty
            };
        }
        catch (Exception e)
        {
            return new BaseResponse<Requisition>()
            {
                Message = $"Talep girişi sırasında {e} hatası oluştu",
                Title = "Uyarı",
                Status = DataAccess.Entites.Enums.ResultStatus.Error,
                Data = model,
                Url = string.Empty

            };
        }
    }

    public Task<BaseResponse<Requisition>> Delete(Requisition model)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse<Requisition>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse<Requisition>> GetList(Requisition model)
    {
        var requisitionList = await _dataContext.Requisitions.ToListAsync();

        return new BaseResponse<Requisition>()
        {
            DataList = requisitionList
        };
    }

    public async Task<BaseResponse<Requisition>> Update(Requisition model)
    {
        var currentRequisition = _dataContext.Requisitions.Find(model.Id);

        try
        {
            currentRequisition.UserId = model.UserId;
            currentRequisition.Status = RequisitionStatus.AssignedTo;

            _dataContext.Requisitions.Update(currentRequisition);

            var result = await _dataContext.SaveChangesAsync();

            if (result > 0)
            {
                return new BaseResponse<Requisition>()
                {
                    Message = $"{currentRequisition} başarıyla güncellendi",
                    Status = ResultStatus.Success,
                    Title = "Başarı",
                    Data = currentRequisition,
                    Url = "/User/Index"
                };
            }
            return new BaseResponse<Requisition>
            {
                Message = $"{currentRequisition} güncelleme işlemi sırasında hata oluştu",
                Status = ResultStatus.Error,
                Title = "Hata",
                Data = null,
                Url = string.Empty
            };
        }
        catch (Exception e)
        {

            return new BaseResponse<Requisition>
            {
                Message = $"{currentRequisition} güncelleme işlemi sırasında {e} hatası oluştu",
                Status = ResultStatus.Error,
                Title = "Hata",
                Data = null,
                Url = string.Empty
            };
        }
    }

    public async Task<BaseResponse<Requisition>> GetActiveRequisition()
    {
        var requisition = await _dataContext.Requisitions.Where(t => t.Active && !t.Deleted).ToListAsync();

        return new BaseResponse<Requisition>()
        {
            DataList = requisition
        };
    }
}
