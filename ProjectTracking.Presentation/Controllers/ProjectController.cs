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
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Project model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _projectService.Add(model);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> ProjectGet()
    {
        var project = await _projectService.ActiveProject();

        return PartialView("LayoutPartials/ProjectPartials/_ProjectListPartial", project.DataList);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var result = await _projectService.DeleteById(id);

        return Ok(result);
    }

}
