using Application.Common.Interfaces.Persistence;
using Application.Common.Specifications;
using Domain.Common;
using Infrastructure.Persistence.Context;
using Infrastructure.Persistence.Evaluators;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

internal class EfReadRepository<TEntity> : IReadRepository<TEntity> where TEntity : class, IEntity
{
    private readonly ApplicationDbContext _context;

    public EfReadRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<TEntity?> GetAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(spec)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<TEntity>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>()
            .ToListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> ListAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(spec)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>()
            .CountAsync(cancellationToken);
    }

    public async Task<int> CountAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(spec, true)
            .CountAsync(cancellationToken);
    }
    
    protected IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec, bool evaluateOnlyCriteria = default)
    {
        return SpecificationEvaluator<TEntity>.GetQuery(
            _context.Set<TEntity>().AsQueryable(),
            spec,
            evaluateOnlyCriteria).AsNoTracking();
    }
}