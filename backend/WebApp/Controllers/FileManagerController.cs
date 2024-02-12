using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using Domain.Identity;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Service.Interface;

namespace WebApp.Controllers;

[Route("v1/api/file")]
[ApiController]
[Produces("application/json")]
public class FileManagerController(IFileManager fileManager, UserManager<ApplicationUser> userManager, ApplicationDbContext context, IMapper mapper) : Controller
{
    
    [HttpPost]
    [Authorize(Roles = null)]
    [Route("getAll")]
    public async Task<IActionResult> GetAll()
    {
        var name = User.Identity?.Name;
        if (name == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        var user = await userManager.FindByNameAsync(name);
        if (user == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        var files = context.FileModels.Where(m => m.Creator.Id == user.Id).Select(m => mapper.Map<FileModelDto>(m)).ToList();
        return Json(files);
    }
    
    [HttpPost]
    [Route("upload")]
    [Authorize(Roles = null)]
    public async Task<IActionResult> UploadFile([FromForm] FileFormModel fileForm)
    {
        var name = User.Identity?.Name;
        if (name == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        var user = await userManager.FindByNameAsync(name);
        if (user == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        var result = await fileManager.UploadFile(fileForm, user);
        return Json(result.Id);
    }

    [HttpGet]
    [Route("download")]
    public async Task<IActionResult> DownloadFile(Guid id)
    {
        var result = await fileManager.DownloadFile(id);
        return Json(result);
    }
    
    [HttpDelete]
    [Route("delete")]
    [Authorize(Roles = null)]
    public async Task<IActionResult> Delete([FromForm] Guid id)
    {
        ArgumentNullException.ThrowIfNull(id);
        var name = User.Identity?.Name;
        if (name == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        var user = await userManager.FindByNameAsync(name);
        if (user == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        var file = context.FileModels.Where(m => m.Id == id).FirstOrDefault(m => m.Creator.Id == user.Id);
        if (file == null)
        {
            return StatusCode(StatusCodes.Status404NotFound);
        }
        
        fileManager.DeleteFile(file);
        return Ok();
    }
}
