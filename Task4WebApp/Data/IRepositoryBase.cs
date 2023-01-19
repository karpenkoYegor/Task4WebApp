using System.Linq.Expressions;

namespace Task4WebApp.Data;

public interface IRepositoryBase<T>
{
    public T FindByEmail(Expression<Func<T, bool>> expression);
    public T FindById(Expression<Func<T, bool>> expression);
    void Create(T entity);
    void CreateAll(IEnumerable<T> entities);
    void Update(T entity);
    void Delete(T entity);
}