using System;
using System.IO;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
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
    public static string GetFilePath(string FileName)
    {
        var _GetStaticContentDirectory = GetStaticContentDirectory();
        var result = Path.Combine(_GetStaticContentDirectory, FileName);
        return result;
    }
}

public class FileManager : IFileManager
{
    public async Task<string> UploadFile(FileModel file)
    {
        FileInfo fileInfo = new FileInfo(file.FileName);
        var fileName = file.FileName + "_" + DateTime.Now.Ticks + fileInfo.Extension;
        var filePath = Common.GetFilePath(fileName);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.FormFile.CopyToAsync(fileStream);
        }
        return fileName;
    }

    public async Task<(byte[], string, string)> DownloadFile(string fileName)
    {
        try
        {
            var _GetFilePath = Common.GetFilePath(fileName);
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(_GetFilePath, out var _ContentType))
            {
                _ContentType = "application/octet-stream";
            }
            var _ReadAllBytesAsync = await File.ReadAllBytesAsync(_GetFilePath);
            return (_ReadAllBytesAsync, _ContentType, Path.GetFileName(_GetFilePath));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}