using Application.Common.Specifications;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Evaluators;

public static class SpecificationEvaluator<TEntity> where TEntity : class, IEntity
{
    public static IQueryable<TEntity> GetQuery(
        IQueryable<TEntity> inputQuery,
        ISpecification<TEntity> spec,
        bool evaluateOnlyCriteria = default)
    {
        if (spec.Criteria is not null)
            inputQuery = inputQuery.Where(spec.Criteria);

        if (evaluateOnlyCriteria)
        {
            return inputQuery;
        }

        if (spec.OrderBy is not null)
            inputQuery = inputQuery.OrderBy(spec.OrderBy);

        if (spec.OrderByDescending is not null)
            inputQuery = inputQuery.OrderByDescending(spec.OrderByDescending);

        if (spec is IPaginationSpecification<TEntity> paginationSpec)
        {
            inputQuery = inputQuery.Skip(paginationSpec.Skip).Take(paginationSpec.Take);
        }
        
        inputQuery = spec.Includes.Aggregate(inputQuery, (current, include) => current.Include(include));

        return inputQuery;
    }
}