using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using Repository_Employee.Dtos;
using System.Threading.Tasks;
using System.Linq.Expressions;
namespace Repository_Employee
{
    /// <summary>
    /// Generic Repository class for Entity Operations
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericRepository<TEntity> where TEntity : class
    {
        #region Private member variables...
        internal Dtos.EmployeeDBContext Context;
        internal DbSet<TEntity> DbSet;
        #endregion
        #region Public Constructor...
        /// <summary>
        /// Public Constructor,initializes privately declared local variables.
        /// </summary>
        /// <param name="context"></param>
        public GenericRepository(Dtos.EmployeeDBContext context)
        {
            this.Context = context;
            this.DbSet = context.Set<TEntity>();
        }
        #endregion
        #region Public member methods...
        /// <summary>
        /// generic Get method for Entities
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> Get()
        {
            IQueryable<TEntity> query = DbSet;
            return await query.ToListAsync();
        }
        /// <summary>
        /// Generic get method on the basis of id for Entities.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetByID(object id)
        {
            return await DbSet.FindAsync(id);
        }
        /// <summary>
        /// generic Insert method for the entities
        /// </summary>
        /// <param name="entity"></param>
        public virtual async void Insert(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }
        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="id"></param>
        public virtual async void Delete(object id)
        {
            TEntity entityToDelete = await DbSet.FindAsync(id);
            Delete(entityToDelete);
        }
        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="entityToDelete"></param>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }
        /// <summary>
        /// Generic update method for the entities
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        /// <summary>
        /// generic method to get many record on the basis of a condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetMany(Func<TEntity, bool> where)
        {
            return DbSet.Where(where).ToList();
        }
        /// <summary>
        /// generic method to get many record on the basis of a condition but query able.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetManyQueryable(Func<TEntity, bool> where)
        {
            return DbSet.Where(where).AsQueryable();
        }
        /// <summary>
        /// generic get method , fetches data for the entities on the basis of condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public TEntity Get(Func<TEntity, Boolean> where)
        {
            return DbSet.Where(where).FirstOrDefault<TEntity>();
        }
        /// <summary>
        /// generic delete method , deletes data for the entities on the basis of condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public void Delete(Func<TEntity, Boolean> where)
        {
            IQueryable<TEntity> objects = DbSet.Where<TEntity>(where).AsQueryable();
            foreach (TEntity obj in objects)
                DbSet.Remove(obj);
        }
        /// <summary>
        /// generic method to fetch all the records from db
        /// </summary>
        /// <returns></returns>
        public async virtual Task<IEnumerable<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }
        public async virtual Task<IEnumerable<TEntity>> EntityWithEagerLoad(Expression<Func<TEntity, bool>> filter, string[] children)
        {
            IQueryable<TEntity> query = this.DbSet;
            foreach (string entity in children)
            {
                query = query.Include(entity);
            }
            return await query.Where(filter).ToListAsync();
        }
        public virtual async Task<IEnumerable<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] properties)
        {
            if (properties == null)
                throw new ArgumentNullException(nameof(properties));
            var query = this.DbSet as IQueryable<TEntity>; // _dbSet = dbContext.Set<TEntity>()
            query = properties
                       .Aggregate(query, (current, property) => current.Include(property));
            return await query.AsNoTracking().ToListAsync(); //readonly
        }
        public virtual async Task<TEntity> GetSingleEntityWithInclude(params Expression<Func<TEntity, object>>[] properties)
        {
            if (properties == null)
                throw new ArgumentNullException(nameof(properties));
            var query = this.DbSet as IQueryable<TEntity>; // _dbSet = dbContext.Set<TEntity>()
            query = properties
                       .Aggregate(query, (current, property) => current.Include(property));
            return await query.AsNoTracking().FirstOrDefaultAsync(); //readonly
        }
        /// <summary>
        /// Inclue multiple
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetWithInclude(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, params string[] include)
        {
            IQueryable<TEntity> query = this.DbSet;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return await query.Where(predicate).FirstOrDefaultAsync();
        }
        /// <summary>
        /// Generic method to check if entity exists
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public async Task<bool> Exists(object primaryKey)
        {
            return await DbSet.FindAsync(primaryKey) != null;
        }
        public async virtual Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.FirstOrDefaultAsync(predicate);
        }
        /// <summary>
        /// Gets a single record by the specified criteria (usually the unique identifier)
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record that matches the specified criteria</returns>
        public TEntity GetSingle(Func<TEntity, bool> predicate)
        {
            return DbSet.Single<TEntity>(predicate);
        }
        /// <summary>
        /// The first record matching the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record containing the first record matching the specified criteria</returns>
        public TEntity GetFirst(Func<TEntity, bool> predicate)
        {
            return DbSet.First<TEntity>(predicate);
        }
        #endregion
    }
}