using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository.Implementation;

public class Repository<T>(ApplicationDbContext context) : IRepository<T>
    where T : BaseEntity
{
    private readonly ApplicationDbContext _context = context;
    private readonly DbSet<T> _entities = context.Set<T>();

    public IEnumerable<T> GetAll() => _entities.AsEnumerable();

    public T GetById(Guid id)
    {
        ArgumentNullException.ThrowIfNull(id);
        return this._entities.FirstOrDefault(z => z.Id.Equals(id));
    }

    public async void Insert(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        this._entities.Add(entity);
        await this._context.SaveChangesAsync();
    }

    public async void Update(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        this._entities.Update(entity);
        await this._context.SaveChangesAsync();
    }

    public async void Delete(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        this._entities.Remove(entity);
        await this._context.SaveChangesAsync();
    }
}
