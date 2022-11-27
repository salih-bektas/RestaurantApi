using System.Linq.Expressions;

namespace DataAccess.GenericRepository;

public interface IRepository<T> where T : class
{
    T Add(T entity);
    T Update(T entity);
    int Delete(T entity);

    T? Get(Expression<Func<T, bool>>? filter=null);

    IEnumerable<T> GetList(Expression<Func<T, bool>>? filter=null);
    IQueryable<T> Table { get; }
/*    T Get(Expression<Func<T, bool>> filter = null);
    List<T> GetList(Expression<Func<T, bool>> filter = null);
    */
}