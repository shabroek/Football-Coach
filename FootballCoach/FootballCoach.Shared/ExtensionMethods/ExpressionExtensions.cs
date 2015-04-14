using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace Isah.Core
{
    [ExcludeFromCodeCoverage]
    public static class ExpressionExtensions
    {
        public static Expression<Func<TBase, TResult>> ReplaceParameterWithBase<T, TResult, TBase>(this Expression<Func<T, TResult>> lambda) where T : TBase
        {
            var param = lambda.Parameters.Single();
            return (Expression<Func<TBase, TResult>>)
                ParameterRebinder.ReplaceParameters(new Dictionary<ParameterExpression, ParameterExpression>
                {
                    {param, Expression.Parameter(typeof (TBase), param.Name)}
                }, lambda.Body);
        }

        //public static Expression<Func<object, object>> ConvertParameterToObject<T>(this Expression<Func<T, object>> source)
        //{
        //    return source.ReplaceParametersWithBase<T, object, object>();
        //}
    }

    [ExcludeFromCodeCoverage]
    public class ParameterRebinder : ExpressionVisitor
    {

        private readonly Dictionary<ParameterExpression, ParameterExpression> map;
        
        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }



        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }
        
        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;

            if (map.TryGetValue(p, out replacement))
            {
                p = replacement;

            }

            return base.VisitParameter(p);

        }

    }

}
