using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using XamF.Controls.DataGrid.DataGridControl.Core.Enums;

namespace XamF.Controls.DataGrid.DataGridControl.Utilities
{
    public static class LinqExtensions
    {
        public static IOrderedQueryable SortBy(this IQueryable source, string property,
            OrderType direction = OrderType.Ascending)
        {
            var properties = property.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (direction == OrderType.Ascending)
            {
                var orderQuery = source.OrderBy(properties[0]);
                for (int i = 1; i < properties.Length; i++)
                {
                    orderQuery = orderQuery.ThenBy(properties[i]);
                }
                return orderQuery;
            }
            else
            {
                var orderQuery = source.OrderByDescending(properties[0]);
                for (int i = 1; i < properties.Length; i++)
                {
                    orderQuery = orderQuery.ThenByDescending(properties[i]);
                }
                return orderQuery;
            }
        }

        public static IOrderedQueryable OrderBy(this IQueryable source, string property)
        {
            return ApplyOrder(source, property, "OrderBy");
        }

        public static IOrderedQueryable OrderByDescending(this IQueryable source, string property)
        {
            return ApplyOrder(source, property, "OrderByDescending");
        }

        public static IOrderedQueryable ThenBy(this IOrderedQueryable source, string property)
        {
            return ApplyOrder(source, property, "ThenBy");
        }

        public static IOrderedQueryable ThenByDescending(this IOrderedQueryable source, string property)
        {
            return ApplyOrder(source, property, "ThenByDescending");
        }

        private static IOrderedQueryable ApplyOrder(IQueryable source, string property, string methodName)
        {
            Type itemType = source.GetType().GetGenericArguments()[0];
            ParameterExpression arg = Expression.Parameter(itemType, "x");
            Expression expr = arg;
            PropertyInfo pi = itemType.GetProperty(property);
            expr = Expression.Property(expr, pi);
            var propType = pi.PropertyType;
            Type delegateType = typeof(Func<,>).MakeGenericType(itemType, propType);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(itemType, propType)
                    .Invoke(null, new object[] { source, lambda });

            return (IOrderedQueryable)result;
        }
    }
}
