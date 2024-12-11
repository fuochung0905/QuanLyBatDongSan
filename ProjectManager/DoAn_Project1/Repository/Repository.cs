using AutoDependencyRegistration.Attributes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;


namespace Repository
{
  
    public class Repository<T> : IRepository<T>, IDisposable where T : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;
        private object context;

        public Repository(DbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = this._dbContext.Set<T>();
        }
        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return _dbSet.SingleOrDefault(match);
        }

        public virtual IQueryable<T> FindAll()
        {
            return _dbSet;
        }

        public virtual IQueryable<T> FindAllAsync()
        {
            return _dbSet;
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await EntityFrameworkQueryableExtensions.SingleOrDefaultAsync(_dbSet, match);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            IQueryable<T> source2;
            if (includes != null && includes.Count() > 0)
            {
                IQueryable<T> source = EntityFrameworkQueryableExtensions.Include(_dbSet, includes.First());
                foreach (string item in includes.Skip(1))
                {
                    source = EntityFrameworkQueryableExtensions.Include(source, item);
                }

                source2 = ((predicate != null) ? source.Where(predicate).AsQueryable() : source.AsQueryable());
            }
            else
            {
                source2 = ((predicate != null) ? _dbSet.Where(predicate).AsQueryable() : Queryable.AsQueryable(_dbSet));
            }

            return source2.AsQueryable();
        }

        public IQueryable<T> GetMultiPaging(Expression<Func<T, bool>> predicate, out int total, int index = 0, int size = 50, string[] includes = null)
        {
            int num = index * size;
            IQueryable<T> source2;
            if (includes != null && includes.Count() > 0)
            {
                IQueryable<T> source = EntityFrameworkQueryableExtensions.Include(_dbSet, includes.First());
                foreach (string item in includes.Skip(1))
                {
                    source = EntityFrameworkQueryableExtensions.Include(source, item);
                }

                source2 = ((predicate != null) ? source.Where(predicate).AsQueryable() : source.AsQueryable());
            }
            else
            {
                source2 = ((predicate != null) ? _dbSet.Where(predicate).AsQueryable() : Queryable.AsQueryable(_dbSet));
            }

            source2 = ((num == 0) ? source2.Take(size) : source2.Skip(num).Take(size));
            total = source2.Count();
            return source2.AsQueryable();
        }

        public virtual T GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public virtual async Task<T> GetAsyncById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual T GetByIdAsDetached(Guid id)
        {
            T val = _dbSet.Find(id);
            EntityEntry entityEntry = _dbContext.Entry(val);
            entityEntry.State = EntityState.Detached;
            return val;
        }

        public virtual void Add(T entity)
        {
            EntityEntry entityEntry = _dbContext.Entry(entity);
            if (entityEntry.State != 0)
            {
                entityEntry.State = EntityState.Added;
            }
            else
            {
                _dbSet.Add(entity);
            }
        }

        public virtual void AddRange(IEnumerable<T> entity)
        {
            _dbSet.AddRange(entity);
        }

        public virtual void Update(T entity)
        {
            EntityEntry entityEntry = _dbContext.Entry(entity);
            if (entityEntry.State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            entityEntry.State = EntityState.Modified;
        }

        public virtual void UpdateRange(IEnumerable<T> entity)
        {
            _dbSet.UpdateRange(entity);
        }

        public virtual int Count()
        {
            return _dbSet.Count();
        }

        public virtual Task<int> CountAsync()
        {
            return EntityFrameworkQueryableExtensions.CountAsync(_dbSet);
        }

        public virtual void Delete(T entity)
        {
            EntityEntry entityEntry = _dbContext.Entry(entity);
            if (entityEntry.State != EntityState.Deleted)
            {
                entityEntry.State = EntityState.Deleted;
                return;
            }

            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }

        public virtual void Delete(Guid id)
        {
            T byId = GetById(id);
            if (byId != null)
            {
                Delete(byId);
            }
        }

   

        public virtual void DeleteRange(IEnumerable<T> entity)
        {
            _dbSet.RemoveRange(entity);
        }

        public bool Exists(Guid id)
        {
            return _dbSet.Find(id) != null;
        }

        public virtual int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<T>> ListAsync()
        {
            IQueryable<T> query = EntityFrameworkQueryableExtensions.AsNoTracking(_dbSet);
            return await EntityFrameworkQueryableExtensions.ToListAsync(query);
        }

        public virtual async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = EntityFrameworkQueryableExtensions.AsNoTracking(_dbSet);
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return await EntityFrameworkQueryableExtensions.ToListAsync(orderBy(query));
            }

            return await EntityFrameworkQueryableExtensions.ToListAsync(query);
        }

        public virtual async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> includeProperties = null)
        {
            IQueryable<T> query = EntityFrameworkQueryableExtensions.AsNoTracking(_dbSet);
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                query = includeProperties(query);
            }

            if (orderBy != null)
            {
                return await EntityFrameworkQueryableExtensions.ToListAsync(orderBy(query));
            }

            return await EntityFrameworkQueryableExtensions.ToListAsync(query);
        }

        public IEnumerable<T> ExcuteStoredProcedure(string nameofStored, object model)
        {
            if (_dbContext.Database.GetDbConnection().State != ConnectionState.Open)
            {
                _dbContext.Database.OpenConnection();
            }

            List<SqlParameter> list = new List<SqlParameter>();
            using DbCommand dbCommand = _dbContext.Database.GetDbConnection().CreateCommand();
            PropertyInfo[] properties = model.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                list.Add(new SqlParameter(propertyInfo.Name, propertyInfo.GetValue(model)));
            }

            dbCommand.CommandType = CommandType.StoredProcedure;
            dbCommand.CommandText = nameofStored;
            dbCommand.Parameters.AddRange(list.ToArray());
            DataTable dataTable = new DataTable();
            using (DbDataReader reader = dbCommand.ExecuteReader())
            {
                dataTable.Load(reader);
            }

            dbCommand.Parameters.Clear();
            return dataTable.ToList<T>();
        }

        public IEnumerable<T> ExcuteStoredProcedure(string nameofStored, SqlParameter[]? parameters)
        {
            if (_dbContext.Database.GetDbConnection().State != ConnectionState.Open)
            {
                _dbContext.Database.OpenConnection();
            }

            using DbCommand dbCommand = _dbContext.Database.GetDbConnection().CreateCommand();
            dbCommand.CommandTimeout = 180;
            dbCommand.CommandType = CommandType.StoredProcedure;
            dbCommand.CommandText = nameofStored;
            if (parameters != null)
            {
                dbCommand.Parameters.AddRange(parameters);
            }

            DataTable dataTable = new DataTable();
            using (DbDataReader reader = dbCommand.ExecuteReader())
            {
                dataTable.Load(reader);
            }

            dbCommand.Parameters.Clear();
            return dataTable.ToList<T>();
        }

        public IQueryable<T> ExcuteStoredQuery(string query)
        {
            return EntityFrameworkQueryableExtensions.IgnoreQueryFilters(_dbSet.FromSqlRaw(query));
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        
    }
}
