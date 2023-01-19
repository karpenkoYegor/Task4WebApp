using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Task4WebApp.Data;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected AppDbContext Context { get; set; }
    public RepositoryBase(AppDbContext context)
    {
        Context = context;
    }

    public T FindByEmail(Expression<Func<T, bool>> expression) =>
        Context.Set<T>().Where(expression).AsNoTracking().FirstOrDefault();
    public T FindById(Expression<Func<T, bool>> expression) =>
        Context.Set<T>().Where(expression).AsNoTracking().FirstOrDefault();
    public void Create(T entity) => Context.Set<T>().Add(entity);
    public void CreateAll(IEnumerable<T> entities)
    {
        Context.Set<T>().AddRange(entities);
    }

    public void Update(T entity) => Context.Set<T>().Update(entity);

    public void Delete(T entity) => Context.Set<T>().Remove(entity);
}