using FakeBankAPI.BaseData;
using FakeBankAPI.Repo.RepoFunctionBase;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FakeBankAPI.Repo
{
    public class Repo<T> : IRepo<T> where T : class //contains basic functionality to be shared by multiple repos
    {
        private readonly DBContext _data;
        internal DbSet<T> dbSet;
        public Repo(DBContext data)
        {
            _data = data;
            this.dbSet = _data.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await _data.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            //checks if there is a filter and includes it
            if (filter != null)
            {
                query.Where(filter);
            }

            //checks if there are any properties to include and includes them
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            //returns the list of entities
            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            //checks if tracked and sets it accordingly
            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            //checks if there is a filter and includes it
            if (filter != null)
            {
                query.Where(filter);
            }

            //checks if there are any properties to include and includes them
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            //returns the entity
            return await query.FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            await _data.SaveChangesAsync();
        }
    }
}
