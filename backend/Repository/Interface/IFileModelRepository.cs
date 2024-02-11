using System;
using System.Collections.Generic;
using Domain;

namespace Repository.Interface;

public interface IFileModelRepository
{
    IEnumerable<FileModel> GetAll();
    FileModel GetById(Guid id);
    
    void Insert(FileModel entity);
    void Update(FileModel entity);
    void Delete(FileModel entity);
}