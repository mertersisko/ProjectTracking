using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectTracking.Bussiness.Repositories.BaseInferfaces;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.RequestClasses;
using ProjectTracking.DataAccess.Entites.ResponseEntites;

namespace ProjectTracking.Bussiness.Repositories.Classes.RequisitionServices.Abstract;

public interface IRequisitionService : IBaseService<Requisition>
{
    public Task<BaseResponse<Requisition>> GetActiveRequisition();

    public Task<BaseResponse<Requisition>> DeleteById(int id);

}
