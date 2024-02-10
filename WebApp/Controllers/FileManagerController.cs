using System.Net;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace WebApp.Controllers;

[ApiController]
public class FileManagerController(IFileManager fileManager) : Controller
{
    [HttpPost]
    [Route("uploadfile")]
    public async Task<IActionResult> UploadFile([FromForm] FileModel file)
    {
        var result = await fileManager.UploadFile(file);
        return Ok(result);
    }

    [HttpGet]
    [Route("downloadfile")]
    public async Task<IActionResult> DownloadFile(string fileName)
    {
        var result = await fileManager.DownloadFile(fileName);
        return File(result.Item1, result.Item2, result.Item2);
    }

}