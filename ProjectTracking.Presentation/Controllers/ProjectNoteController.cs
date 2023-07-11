using Microsoft.AspNetCore.Mvc;
using ProjectTracking.Bussiness.Repositories.Classes.ProjectNoteService.Abstract;
using ProjectTracking.Bussiness.Repositories.Classes.ProjectServices.Abstract;
using ProjectTracking.DataAccess.Context;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.ProjectNoteClasses;

namespace ProjectTracking.Presentation.Controllers;

public class ProjectNoteController : Controller
{
    private readonly IProjectNoteService _projectNoteService;

    public ProjectNoteController(IProjectNoteService projectNoteService)
    {
        _projectNoteService = projectNoteService;
    }
    public  IActionResult Index()
    {
        var noteList =  _projectNoteService.NonDeleted();

        return View(noteList.DataList);
    }
    public IActionResult Add()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Add(ProjectNote model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _projectNoteService.Add(model);

        return View(model);
    }
    [HttpGet]
    public IActionResult GetProjectNote(int ID)
    {
        return Ok(_projectNoteService.GetNotes(ID).ToList());
    }
}
