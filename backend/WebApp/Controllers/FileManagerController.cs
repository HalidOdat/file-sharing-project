using System;
using System.Net;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace WebApp.Controllers;

[Route("v1/file")]
[ApiController]
public class FileManagerController(IFileManager fileManager) : Controller
{
    [HttpPost]
    [Route("upload")]
    public async Task<IActionResult> UploadFile([FromForm] FileFormModel fileForm)
    {
        var result = await fileManager.UploadFile(fileForm);
        return Ok(result);
    }

    [HttpGet]
    [Route("download")]
    public async Task<IActionResult> DownloadFile(Guid id)
    {
        var result = await fileManager.DownloadFile(id);
        return File(result.Item1, result.Item2, result.Item2);
    }

}
