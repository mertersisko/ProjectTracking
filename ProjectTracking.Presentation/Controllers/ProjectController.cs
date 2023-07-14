using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectTracking.Bussiness.Repositories.Classes.ProjectServices.Abstract;
using ProjectTracking.Bussiness.Repositories.Classes.RequisitionServices.Abstract;
using ProjectTracking.DataAccess.Context;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.ProjectClasses;
using ProjectTracking.DataAccess.Entites.Classes.ViewModel;

namespace ProjectTracking.Presentation.Controllers;

public class ProjectController : Controller
{
    private readonly ProjectTrackingDataContext _dataContext;
    private readonly IProjectService _projectService;
    private readonly IRequisitionService _requisitionService;

    public ProjectController(IProjectService projectService, ProjectTrackingDataContext dataContext)
    {
        _dataContext = dataContext;
        _projectService = projectService;
    }
    public async Task<IActionResult> Index()
    {
        var projectList = await _projectService.ActiveProject();
        //ViewBag.RequisitionList = new SelectList(_dataContext.Requisitions.ToList(), "Id", "RequisitionsName");
        return View(projectList.DataList);
    }
    public IActionResult Add()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Add(Project model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _projectService.Add(model);
        return View(model);
    }

}
