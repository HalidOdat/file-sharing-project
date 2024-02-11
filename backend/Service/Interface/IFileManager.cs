using System;
using System.Threading.Tasks;
using Domain.Model;
using Microsoft.AspNetCore.Http;

namespace Services.Interface;

public interface IFileManager
{
    Task<string> UploadFile(FileFormModel fileForm);
    Task<(byte[], string, string)> DownloadFile(Guid id);
}