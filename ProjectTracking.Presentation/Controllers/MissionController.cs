using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Bussiness.Repositories.Classes.MissionServices.Abstract;
using ProjectTracking.Bussiness.Repositories.Classes.UserServices.Abstract;
using ProjectTracking.DataAccess.Context;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.MissionClasses;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.RequestClasses;
using ProjectTracking.DataAccess.Entites.Enums;
using System.Linq;

namespace ProjectTracking.Presentation.Controllers
{
    public class MissionController : Controller
    {
        private readonly ProjectTrackingDataContext _dataContext;
        private readonly IMissionService _missionService;
        private readonly IUserService _userService;

        public MissionController(ProjectTrackingDataContext dataContext, IMissionService missionService, IUserService userService)
        {
            _dataContext = dataContext;
            _missionService = missionService;
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Status = new SelectList(Enum.GetValues(typeof(RequisitionStatus)));
            var missionList = await _missionService.GetMissionAsync();
            return View(missionList.DataList);
        }
        public IActionResult Add()
        {
            ViewBag.Users = new SelectList(_dataContext.Users.ToList(), "Id", "Name");
            ViewBag.Project = new SelectList(_dataContext.Projects.ToList(), "Id", "Name");
            ViewBag.Status = new SelectList(Enum.GetValues(typeof(RequisitionStatus)));
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Mission model)
        {
            if (!ModelState.IsValid)
                return View(model);

             await _missionService.Add(model);
            return View(model);
        }
    }
}
