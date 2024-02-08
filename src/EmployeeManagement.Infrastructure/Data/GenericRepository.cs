using EmployeeManagement.Common.Interfaces;
using EmployeeManagement.Common.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeManagement.Infrastructure.Data;
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<List<T>> GetFilteredAsync(Expression<Func<T, bool>>[] filters, int? skip, int? take, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet.AsQueryable<T>();

        foreach(var filter in filters)
        {
            query = query.Where(filter);
        }

        foreach(var include in includes)
        {
            query = query.Include(include);
        }

        if (skip != null)
            query = query.Skip(skip.Value);

        if (take != null)
            query = query.Take(take.Value);

        return await query.ToListAsync();
    }

    public async Task<List<T>> GetAsync(int? skip, int? take, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet.AsQueryable<T>();

        foreach (var include in includes)
            query = query.Include(include);

        if (skip != null)
            query = query.Skip(skip.Value);

        if (take != null)
            query = query.Take(take.Value);

        return await query.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet.AsQueryable<T>();

        query = query.Where(e => e.Id == id);

        foreach (var include in includes)
            query = query.Include(include);

        return await query.SingleOrDefaultAsync();
    }

    public async Task<int> InsertAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return entity.Id;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
            _dbSet.Attach(entity);

        _dbSet.Entry(entity).State = EntityState.Modified;
        //_context.SaveChanges();
    }
    public void Delete(T entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
            _dbSet.Attach(entity);
        
        _dbSet.Remove(entity);
    }
}
