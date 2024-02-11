using Microsoft.AspNetCore.Http;

namespace Domain.Model;

public class FileFormModel
{
    public string FileName { get; set; }
    public IFormFile FormFile { get; set; }
}
