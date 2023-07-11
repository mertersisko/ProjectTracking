using Microsoft.EntityFrameworkCore;
using ProjectTracking.Bussiness.Repositories.Classes.UserServices.Abstract;
using ProjectTracking.DataAccess.Context;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.UserClasses;
using ProjectTracking.DataAccess.Entites.Classes.DtoClasses.LoginDto;
using ProjectTracking.DataAccess.Entites.Enums;
using ProjectTracking.DataAccess.Entites.ResponseEntites;

namespace ProjectTracking.Bussiness.Repositories.Classes.UserServices.Concrete;

public class UserService : IUserService
{
    private readonly ProjectTrackingDataContext _dataContext;

    public UserService(ProjectTrackingDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<BaseResponse<User>> Add(User model)
    {
        try
        {
            model.CreatedDate = DateTime.Now;
            await _dataContext.Users.AddAsync(model);
            var result = await _dataContext.SaveChangesAsync();

            if (result > 0)
            {
                return new BaseResponse<User>()
                {
                    Status = DataAccess.Entites.Enums.ResultStatus.Success,
                    Message = "Kullanıcı ekleme başarılı",
                    Data = model,
                    Title = "Başarılı",
                    Url = "/User/Index"
                };
            }
            return new BaseResponse<User>()
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
            return new BaseResponse<User>()
            {
                Status = DataAccess.Entites.Enums.ResultStatus.Error,
                Message = "Kullanıcı ekleme sırasında hata oluştu",
                Data = model,
                Title = "Hata",
                Url = string.Empty
            };
        }
    }

    public async Task<BaseResponse<User>> Delete(User model)
    {
        var currentUser = _dataContext.Users.Find(model.Id);

        try
        {
            currentUser.Active = false;
            currentUser.Deleted = true;
            _dataContext.Users.Update(currentUser);

            var result = await _dataContext.SaveChangesAsync();

            if (result > 0)
            {
                return new BaseResponse<User>()
                {
                    Message = $"{currentUser} başarıyla silindi",
                    Status = ResultStatus.Success,
                    Title = "Başarı",
                    Data = currentUser,
                    Url = "/User/Index"
                };
            }
            return new BaseResponse<User>
            {
                Message = $"{currentUser} silme işlemi sırasında hata oluştu",
                Status = ResultStatus.Error,
                Title = "Hata",
                Data = null,
                Url = string.Empty
            };
        }
        catch (Exception e)
        {

            return new BaseResponse<User>
            {
                Message = $"{currentUser} silme işlemi sırasında {e} hatası oluştu",
                Status = ResultStatus.Error,
                Title = "Hata",
                Data = null,
                Url = string.Empty
            };
        }
    }

    public async Task<BaseResponse<User>> GetById(int id)
    {
        var currentUser = await _dataContext.Users.FindAsync(id);

        return new BaseResponse<User>()
        {
            Data = currentUser
        };

    }
    public async Task<BaseResponse<User>> GetList(User model)
    {
        var UserList = await _dataContext.Users.ToListAsync();

        return new BaseResponse<User>()
        {
            DataList = UserList,
        };
    }

    public async Task<BaseResponse<User>> Update(User model)
    {
        var currentUser = _dataContext.Users.Find(model.Id);

        try
        {
            currentUser.Name = model.Name;
            currentUser.Surname = model.Surname;
            currentUser.Email = model.Email;
            currentUser.Password = model.Password;
            currentUser.Active = model.Active;
            _dataContext.Users.Update(currentUser);

            var result = await _dataContext.SaveChangesAsync();

            if (result > 0)
            {
                return new BaseResponse<User>()
                {
                    Message = $"{currentUser} başarıyla güncellendi",
                    Status = ResultStatus.Success,
                    Title = "Başarı",
                    Data = currentUser,
                    Url = "/User/Index"
                };
            }
            return new BaseResponse<User>
            {
                Message = $"{currentUser} güncelleme işlemi sırasında hata oluştu",
                Status = ResultStatus.Error,
                Title = "Hata",
                Data = null,
                Url = string.Empty
            };
        }
        catch (Exception e)
        {

            return new BaseResponse<User>
            {
                Message = $"{currentUser} güncelleme işlemi sırasında {e} hatası oluştu",
                Status = ResultStatus.Error,
                Title = "Hata",
                Data = null,
                Url = string.Empty
            };
        }
    }
    public async Task<BaseResponse<User>> ActiveUsers()
    {
        var UserList = await _dataContext.Users.Where(t => t.Active && !t.Deleted).ToListAsync();

        return new BaseResponse<User>
        {
            DataList = UserList
        };
    }
    public bool IsTheUserPresent(string UserMail, int? id)
    {
        if (id == null)
            return _dataContext.Users.Any(t => t.Email == UserMail);

        return _dataContext.Users.Any(t => t.Email == UserMail && t.Id != id);

    }

    public async Task<BaseResponse<User>> DeleteById(int id)
    {
        var user = await GetById(id);

        if (user.Data != null)
        {
            _dataContext.Users.Remove(user.Data);

            var result = await _dataContext.SaveChangesAsync();

            if (result > 0)
            {
                return new BaseResponse<User>
                {
                    Status = ResultStatus.Success,
                    Message = "Silme işlemi başarılı",
                    Data = null,
                    Title = "Başarı",
                    Url = "/User/Index"
                };
            }
            return new BaseResponse<User>
            {
                Status = ResultStatus.Error,
                Message = "Silme işlemi sırasında hata oluştu",
                Data = null,
                Title = "Uyarı",
                Url = string.Empty
            };
        }
        return new BaseResponse<User>
        {
            Status = ResultStatus.Warning,
            Message = "Kullanıcı bulunamadı",
            Data = null,
            Title = "Uyarı",
            Url = string.Empty
        };
    }

    public User Login(LoginDto model)
    {
        var user = _dataContext.Users.FirstOrDefault(t => t.Email == model.Mail && t.Password == model.Password && t.Active && !t.Deleted);

        return user;

    }
}
