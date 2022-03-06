using Domain;
using System.Linq.Expressions;

namespace Data.IRepositories;

public interface IGenericRepository<T>
{
    public Task<List<T>> GetAll();

    public Task<T?> GetById(Id<T> id);

    public Task<T?> Get(Expression<Func<T, bool>> expression);

    public void Create(T entity);

    public void Delete(T entity);
}
