using Application.Common.Interfaces.Persistence;
using Application.Common.Specifications;
using Domain.Common;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

internal class EfRepository<TEntity> : EfReadRepository<TEntity>, IRepository<TEntity> where TEntity : class, IAggregateRoot
{
    private readonly ApplicationDbContext _context;

    public EfRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }
    
    public async Task<TEntity?> GetTrackedAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync(cancellationToken);
    }
}