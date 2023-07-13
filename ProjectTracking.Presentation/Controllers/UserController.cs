using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjectTracking.Bussiness.Repositories.Classes.UserServices.Abstract;
using ProjectTracking.Bussiness.Session;
using ProjectTracking.DataAccess.Context;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.UserClasses;
using ProjectTracking.DataAccess.Entites.Classes.DtoClasses.LoginDto;
using ProjectTracking.Presentation.Filter;

namespace ProjectTracking.Presentation.Controllers;
public class UserController : Controller
{

    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {

        _userService = userService;
    }
    public async Task<IActionResult> Index()
    {
        var UserList = await _userService.ActiveUsers();
        return View(UserList.DataList);
    }
    [Auth]
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] User model)
    {
        if (_userService.IsTheUserPresent(model.Email, null))
            ModelState.AddModelError("Mail Adresi", $"{model.Email} kayıtlıdır");

        if (!ModelState.IsValid)
            return View(model);

        var result = await _userService.Add(model);

        return Ok(result);
    }
    [Auth]
    public async Task<IActionResult> Update(int id)
    {
        var queryResult = await _userService.GetById(id);
        if (queryResult.Data == null)
        {
            TempData["Result"] = "Kullanıcı bulunamadı";
            return RedirectToAction(nameof(Index));
        }

        return PartialView("LayoutPartials/UserPartials/_UserAddPartial", queryResult.Data);
    }
    [HttpPost]
    public async Task<IActionResult> Update(User model)
    {
        if (_userService.IsTheUserPresent(model.Email, model.Id))
            ModelState.AddModelError("Kullanıcı Email", $"{model.Email} daha önce eklenmiş");

        if (!ModelState.IsValid)
            return View(model);

        ViewBag.Result = await _userService.Update(model);

        return RedirectToAction(nameof(Index));
    }
    [Auth]
    public async Task<IActionResult> Delete(int id)
    {

        var result = await _userService.DeleteById(id);

        return Ok(result);
    }
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Login(LoginDto model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = _userService.Login(model);

        if (user == null)
        {
            ViewBag.Result = "Kullanıcı ad yada parola hatalı";
            return View(model);

        }

        HttpContext.Session.SessionAssignData("user", user);

        return RedirectToAction(nameof(Index));
    }
    public IActionResult Logout()
    {

        HttpContext.Session.Clear();

        return RedirectToAction(nameof(Login));
    }
    [HttpGet]
    public async Task<IActionResult> UserGet()
    {
        var currentUser = await _userService.ActiveUsers();

        return PartialView("LayoutPartials/UserPartials/_UserListPartial", currentUser.DataList);
    }

}


