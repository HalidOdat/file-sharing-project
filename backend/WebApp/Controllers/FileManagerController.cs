using System;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WebApp.Controllers;

[Route("v1/api/file")]
[ApiController]
[Produces("application/json")]
public class FileManagerController(IFileManager fileManager) : Controller
{
    [HttpPost]
    [Route("upload")]
    [Authorize(Roles = null)]
    public async Task<IActionResult> UploadFile([FromForm] FileFormModel fileForm)
    {
        var result = await fileManager.UploadFile(fileForm);
        return Json(result);
    }

    [HttpGet]
    [Route("download")]
    public async Task<IActionResult> DownloadFile(Guid id)
    {
        var result = await fileManager.DownloadFile(id);
        return Json(result);
    }

}
