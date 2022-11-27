using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.GenericRepository;

public abstract class Repository<T> : IRepository<T> where T : class
{
    private readonly DbContext _tContext;

    public Repository(DbContext context)
    {
        _tContext = context;
    }
    
    public T Add(T entity)
    {
        var addedEntity = _tContext.Entry(entity);
        addedEntity.State = EntityState.Added;
        _tContext.SaveChanges();
        return addedEntity.Entity;
    }

    public T Update(T entity)
    {
        var updatedEntity = _tContext.Entry(entity);
        updatedEntity.State = EntityState.Modified;
        _tContext.SaveChanges();
        return updatedEntity.Entity;
    }

    public int Delete(T entity)
    {
        var deletedEntity = _tContext.Entry(entity);
        deletedEntity.State = EntityState.Deleted;
        return _tContext.SaveChanges();
    }

    public T? Get(Expression<Func<T, bool>>? filter=null)
    {
        return filter == null ? _tContext.Set<T>().SingleOrDefault() : _tContext.Set<T>().SingleOrDefault(filter);
    }

    public IEnumerable<T> GetList(Expression<Func<T, bool>>? filter=null)
    {
        return filter == null
            ? _tContext.Set<T>().ToList()
            : _tContext.Set<T>().Where(filter).ToList();
    }

    public IQueryable<T> Table => _tContext.Set<T>();
}