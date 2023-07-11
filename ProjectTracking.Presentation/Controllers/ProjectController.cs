using Microsoft.AspNetCore.Mvc;
using ProjectTracking.Bussiness.Repositories.Classes.ProjectServices.Abstract;
using ProjectTracking.DataAccess.Context;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.ProjectClasses;
using ProjectTracking.DataAccess.Entites.Classes.ViewModel;

namespace ProjectTracking.Presentation.Controllers;

public class ProjectController : Controller
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }
    public async Task<IActionResult> Index()
    {
        var projectList = await _projectService.ActiveProject();
        return View(projectList.DataList);
    }
    public IActionResult Add()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Add(Project model)
    {
        if(!ModelState.IsValid)
            return View(model);

        await _projectService.Add(model);
        return View(model);
    }

}
