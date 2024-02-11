using System;
using System.Threading.Tasks;
using Domain;
using Domain.Model;

namespace Services.Interface;

public interface IFileManager
{
    Task<string> UploadFile(FileFormModel fileForm);
    Task<FileModel> DownloadFile(Guid id);
}