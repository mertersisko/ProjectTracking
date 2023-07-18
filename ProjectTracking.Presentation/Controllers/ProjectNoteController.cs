using Microsoft.AspNetCore.Mvc;
using ProjectTracking.Bussiness.Repositories.Classes.ProjectNoteService.Abstract;
using ProjectTracking.DataAccess.Entites.Classes.DbClasses.ProjectNoteClasses;

namespace ProjectTracking.Presentation.Controllers;

public class ProjectNoteController : Controller
{
    private readonly IProjectNoteService _projectNoteService;

    public ProjectNoteController(IProjectNoteService projectNoteService)
    {
        _projectNoteService = projectNoteService;
    }
    public IActionResult Index()
    {
        var noteList = _projectNoteService.NonDeleted();

        return View(noteList.DataList);
    }
    [HttpPost]
    public async Task<IActionResult> Add([FromBody]ProjectNote model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _projectNoteService.Add(model);
        return Ok(result);
    }
    [HttpGet]
    public IActionResult GetProjectNote(int ID)
    {
        var projectNoteList = _projectNoteService.GetNotes(ID).ToList();
        return Ok(projectNoteList);
    }
}
