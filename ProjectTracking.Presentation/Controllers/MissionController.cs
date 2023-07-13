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
        private readonly IMissionService _missionService;
        public MissionController(IMissionService missionService)
        {
            _missionService = missionService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Status = new SelectList(Enum.GetValues(typeof(RequisitionStatus)));
            var missionList = await _missionService.GetMissionAsync();
            return View(missionList.DataList);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _missionService.DeleteById(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> MissionGet()
        {
            var mission = await _missionService.GetMissionAsync();

            return PartialView("LayoutPartials/MissionPartials/_MissionListPartial", mission.DataList);
        }

    }
}
