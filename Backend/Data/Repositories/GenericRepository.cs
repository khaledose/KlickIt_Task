using Data.IRepositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly KlickItContext _context;
    protected DbSet<T> _entities;

    public GenericRepository(KlickItContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    public virtual void Create(T entity)
    {
        _entities.Add(entity);
    }

    public virtual void Delete(T entity)
    {
        _entities.Remove(entity);
    }

    public virtual async Task<T?> GetById(Id<T> id)
    {
        return await _entities.FindAsync(id);
    }

    public virtual async Task<T?> Get(Expression<Func<T, bool>> expression)
    {
        return await _entities.Where(expression).FirstOrDefaultAsync();
    }

    public virtual async Task<List<T>> GetAll()
    {
        return await _entities.ToListAsync() ?? new(); ;
    }
}
