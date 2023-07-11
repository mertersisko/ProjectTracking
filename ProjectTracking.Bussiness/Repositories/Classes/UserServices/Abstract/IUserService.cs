using ProjectTracking.Bussiness.Repositories.BaseInferfaces;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.UserClasses;
using ProjectTracking.DataAccess.Entites.Classes.DtoClasses.LoginDto;
using ProjectTracking.DataAccess.Entites.ResponseEntites;

namespace ProjectTracking.Bussiness.Repositories.Classes.UserServices.Abstract;

public interface IUserService : IBaseService<User>
{
    public Task<BaseResponse<User>>ActiveUsers();
    public bool IsTheUserPresent(string UserMail, int? id);
    public Task<BaseResponse<User>>DeleteById(int id);
    public User Login(LoginDto model);
}
