using System.Linq.Expressions;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteira { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; }
        public Expression<Func<T, object>> OrderBy { get; set; } = null;
        public Expression<Func<T, object>> OrderByDesc { get; set; } = null;
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }

        public BaseSpecifications()
        {
            //Includes = new List<Expression<Func<T, object>>>();

            //Criteira = null;
        }

        public BaseSpecifications(Expression<Func<T, bool>> CriteiraExpressions)
        {
            Criteira = CriteiraExpressions;
        }

        public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDesc = orderByDescExpression;
        }

        public void ApplyPagination(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPaginationEnabled = true;
        }
    }
}
