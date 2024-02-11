using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository.Implementation;

public class FileModelRepository(ApplicationDbContext context) : IFileModelRepository
{
    private readonly DbSet<FileModel> _entities = context.Set<FileModel>();

    public IEnumerable<FileModel> GetAll()
    {
        return _entities.AsEnumerable();
    }

    public FileModel GetById(Guid id)
    {
        return _entities.FirstOrDefault(z => z.Id.Equals(id));
    }

    public void Insert(FileModel entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        _entities.Add(entity);
        context.SaveChanges();
    }

    public void Update(FileModel entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        _entities.Update(entity);
        context.SaveChanges();
    }

    public void Delete(FileModel entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        _entities.Remove(entity);
        context.SaveChanges();
    }
}
