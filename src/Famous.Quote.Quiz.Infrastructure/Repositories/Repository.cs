using Famous.Quote.Quiz.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Famous.Quote.Quiz.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        private readonly FamousQuoteQuizDbContext _context;

        public Repository(FamousQuoteQuizDbContext context)
        {
            _context = context;
        }

        protected IQueryable<TEntity> Query()
        {
            return _context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task<TEntity> FindByIdAsync(int id)
        {
            return await _context.FindAsync<TEntity>(id);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            foreach (var item in _context.ChangeTracker.Entries<Entity>())
            {
                if (item.State == EntityState.Added)
                {
                    item.Entity.DateOfCreate = DateTimeOffset.UtcNow;
                }
            }

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
