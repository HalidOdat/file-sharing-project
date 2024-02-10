using System.Threading.Tasks;
using Domain.Model;
using Microsoft.AspNetCore.Http;

namespace Services.Interface;

public interface IFileManager
{
    Task<string> UploadFile(FileModel file);
    Task<(byte[], string, string)> DownloadFile(string fileName);
}