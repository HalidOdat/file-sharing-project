using System;
using System.Threading.Tasks;
using Domain;
using Domain.Model;

namespace Service.Interface;

public interface IFileManager
{
    Task<string> UploadFile(FileFormModel fileForm);
    Task<FileModel> DownloadFile(Guid id);
}