using System;
using System.Threading.Tasks;
using Domain;
using Domain.Identity;
using Domain.Model;

namespace Service.Interface;

public interface IFileManager
{
    Task<FileModel> UploadFile(FileFormModel fileForm, ApplicationUser user);
    Task<FileModel> DownloadFile(Guid id);
    void DeleteFile(FileModel file);
}