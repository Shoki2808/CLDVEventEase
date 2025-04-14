
using EventEaseAPI.Data;
using EventEaseBookings.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventEaseAPI.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly ApplicationDbContext db_context;
        private readonly DbSet<T> dbSet;

        public GenericService(ApplicationDbContext context)
        {
            db_context = context;
            dbSet = db_context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T).IsSubclassOf(typeof(BaseModel)))
            {
                return await dbSet.Where(e => EF.Property<bool>(e, "IsActive")).ToListAsync();
            }
            return await dbSet.ToListAsync();
        }


        public async Task<T> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await db_context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await db_context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await dbSet.FindAsync(id);
            if (entity == null) return false;

            if (entity is BaseModel baseModel)
            {
                baseModel.IsActive = false; // Perform soft delete
                dbSet.Update(entity);
            }
            else
            {
                dbSet.Remove(entity); // Hard delete if IsActive is not available
            }

            await db_context.SaveChangesAsync();
            return true;
        }

    }
}
