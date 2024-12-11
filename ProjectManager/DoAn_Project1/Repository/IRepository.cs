using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
namespace Repository
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IQueryable<T> FindAllAsync();
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        T Find(Expression<Func<T, bool>> match);
        IQueryable<T> FindAll();
        IQueryable<T> GetAll();
        T GetById(Guid id);
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, string[] includes = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Guid id);
        bool Exists(Guid id);
        int SaveChanges();
        int Count();
        IEnumerable<T> ExcuteStoredProcedure(string nameOfStored, object model);
        public IEnumerable<T> ExcuteStoredProcedure(string nameofStored, SqlParameter[]? parameters);
        IQueryable<T> ExcuteStoredQuery(string query);
    }
}
