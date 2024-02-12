using System;
using System.Data;
using System.Threading.Tasks;
using Domain;
using Domain.Model;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class FileManager(IFileModelRepository repository) : IFileManager
{
    public async Task<string> UploadFile(FileFormModel fileForm)
    {
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
        return model;
    }
}