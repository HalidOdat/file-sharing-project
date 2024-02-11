using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Domain;
using Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Repository;
using Repository.Interface;
using Services.Interface;

namespace Services.Implementation;

public static class Common
{
    public static string GetCurrentDirectory()
    {
        var result = Directory.GetCurrentDirectory();
        return result;
    }
    public static string GetStaticContentDirectory()
    {
        var result = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/StaticContent/");
        if (!Directory.Exists(result))
        {
            Directory.CreateDirectory(result);
        }
        return result;
    }
    public static string GetFilePath(string fileName)
    {
        var contentDirectory = GetStaticContentDirectory();
        var result = Path.Combine(contentDirectory, fileName);
        return result;
    }
}

public class FileManager(IFileModelRepository repository) : IFileManager
{
    public async Task<string> UploadFile(FileFormModel fileForm)
    {
        FileInfo fileInfo = new FileInfo(fileForm.FileName);
        var fileName = fileForm.FileName + "_" + DateTime.Now.Ticks + fileInfo.Extension;
        var filePath = Common.GetFilePath(fileName);
        await using var fileStream = new FileStream(filePath, FileMode.Create);
        await fileForm.FormFile.CopyToAsync(fileStream);
        
        var length = fileForm.FormFile.Length;
        var bytes = new byte[length];
        await using (var stream = fileForm.FormFile.OpenReadStream())
        {
            await stream.ReadExactlyAsync(bytes, 0, bytes.Length);
        }

        var model = new FileModel
        {
            Name = fileForm.FileName,
            Description = "The description",
            ContentType = fileForm.FormFile.ContentType,
            Content = bytes
        };

        repository.Insert(model);
        
        return model.Id.ToString();
    }

    public async Task<FileModel> DownloadFile(Guid id)
    {
        var model = repository.GetById(id);
        if (model == null)
        {
            throw new NoNullAllowedException();
        }
        
        
        /*
        var filePath = Common.GetFilePath(fileName);
        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(filePath, out var contentType))
        {
            contentType = "application/octet-stream";
        }
        var bytes = await File.ReadAllBytesAsync(filePath);*/
        return model;
    }
}