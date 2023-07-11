using ProjectTracking.DataAccess.Entites.ResponseEntites;

namespace ProjectTracking.Bussiness.Repositories.BaseInferfaces;

public interface IBaseService<T>
{
    public Task<BaseResponse<T>> Add(T model);
    public Task<BaseResponse<T>> Update(T model);
    public Task<BaseResponse<T>> Delete(T model);
    public Task<BaseResponse<T>> GetById(int id);
    public Task<BaseResponse<T>> GetList(T model);
}
