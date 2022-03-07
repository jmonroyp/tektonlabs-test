using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Test.Infraestructure.Specifications
{
    public class Specification<T> : ISpecification<T>
    {
        public Specification()
        {
        }

        public Specification(Expression<Func<T, bool>> where)
        {
            Where = where;
        }

        public Expression<Func<T, bool>> Where { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}