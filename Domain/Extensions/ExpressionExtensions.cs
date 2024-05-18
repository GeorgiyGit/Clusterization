using Domain.Entities.Clusterization.Displaying;
using System.Linq.Expressions;

namespace Domain.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right)
        {
            var invokedExpr = Expression.Invoke(right, left.Parameters[0]);
            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(left.Body, invokedExpr), left.Parameters);
        }

        public static Expression<Func<T, bool>> Or<T>(
            this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right)
        {
            var invokedExpr = Expression.Invoke(right, left.Parameters[0]);
            return Expression.Lambda<Func<T, bool>>(
                Expression.OrElse(left.Body, invokedExpr), left.Parameters);
        }

        // Method to create OR expression for multiple ClusterIds
        public static Expression<Func<DisplayedPoint, bool>> PointsCreateOrExpression(params int[] clusterIds)
        {
            Expression<Func<DisplayedPoint, bool>> orExpression = e => false;
            foreach (var clusterId in clusterIds)
            {
                var temp = orExpression;
                var id = clusterId;
                orExpression = temp.Or(e => e.ClusterId == id);
            }
            return orExpression;
        }

        // Method to create the final AND expression
        public static Expression<Func<DisplayedPoint, bool>> PointsCreateAndExpression(int tileId, params int[] clusterIds)
        {
            var orExpression = PointsCreateOrExpression(clusterIds);
            Expression<Func<DisplayedPoint, bool>> andExpression = e => e.TileId == tileId;
            return andExpression.And(orExpression);
        }
    }
}
