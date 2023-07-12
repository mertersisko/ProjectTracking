using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Bussiness.Repositories.Classes.MissionServices.Abstract;
using ProjectTracking.Bussiness.Repositories.Classes.RequisitionServices.Abstract;
using ProjectTracking.Bussiness.Repositories.Classes.UserServices.Abstract;
using ProjectTracking.Bussiness.Session;
using ProjectTracking.DataAccess.Context;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.MissionClasses;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.RequestClasses;
using ProjectTracking.DataAccess.Entites.Enums;

namespace ProjectTracking.Presentation.Controllers
{
    public class RequisitionController : Controller
    {
        private readonly ProjectTrackingDataContext _dataContext;
        private readonly IRequisitionService _requisitionService;
        private readonly IUserService _userService;
        private readonly IMissionService _missionService;
        public RequisitionController(ProjectTrackingDataContext dataContext, IRequisitionService requisitionService, IUserService userService, IMissionService missionService)
        {
            _dataContext = dataContext;
            _requisitionService = requisitionService;
            _userService = userService;
            _missionService = missionService;
        }
        public async Task<IActionResult> Index()
        {

            var requisitionList = await _requisitionService.GetActiveRequisition();
            ViewBag.Users = new SelectList(_dataContext.Users.Where(t => t.Active & !t.Deleted).ToList(), "Id", "Name");
            return View(requisitionList.DataList);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Requisition model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _requisitionService.Add(model);
            return Ok(result);
        }
        public async Task<IActionResult> Update(int id)
        {
            var result = await _requisitionService.GetById(id);
            if (result.Data == null)
                return RedirectToAction(nameof(Index));
            return PartialView("LayoutPartials/RequisitionPartials/_RequisitionUpdatePartial", result.Data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Requisition model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var req = await _requisitionService.Update(model);

            await _missionService.Add(new Mission()
            {
                Title = req.Data.RequisitionName,
                Description = req.Data.RequisitionDescription,
                StartDate = req.Data.StartDate,
                EndDate = req.Data.EndDate,
                RequisitionId = model.Id,
                UserId = model.UserId,
                Status = MissionStatus.Pending
            });
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> PendingRequest()
        {
            var pendingList = await _dataContext.Requisitions.Where(t => t.Status == RequisitionStatus.Pending).ToListAsync();
            ViewBag.Users = new SelectList(_dataContext.Users.Where(t => t.Active & !t.Deleted).ToList(), "Id", "Name");
            return View(pendingList);
        }
        [HttpGet]
        public async Task<IActionResult> RequisitionGet()
        {
            var currentRequest = await _requisitionService.GetActiveRequisition();

            return PartialView("LayoutPartials/RequisitionPartials/_RequisitionListPartial", currentRequest.DataList);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _requisitionService.DeleteById(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
