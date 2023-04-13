using System.Linq.Expressions;

namespace Application.Common.Specifications;

public abstract class BaseSpecification<TEntity> : ISpecification<TEntity>
{
    public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

    public List<Expression<Func<TEntity, object>>> Includes { get; } = new();

    public Expression<Func<TEntity, object>>? OrderBy { get; private set; }

    public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

    protected void AddCriteria(Expression<Func<TEntity, bool>> criteria)
    {
        Criteria = criteria;
    }
    
    protected void AddInclude(Expression<Func<TEntity, object>> include)
    {
        Includes.Add(include);
    }

    protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
        OrderByDescending = null;
    }

    protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDesExpression)
    {
        OrderByDescending = orderByDesExpression;
        OrderBy = null;
    }
}