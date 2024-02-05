using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Infrastructure.Persistence.Repositories.QueryHelpers
{
    public static class QueryHelpers
    {
        private const int MAX_SIZE = 25;
        public static IQueryable<T> ApplyRange<T>(this IQueryable<T> query, string range)
        {
            if (string.IsNullOrWhiteSpace(range))
            {
                throw new ArgumentNullException(nameof(range));
            }
            if (!range.Contains(".."))
            {
                throw new ArgumentException("Range must be in format: 0..10");
            }
            var indexes = range.Split("..");

            if (!int.TryParse(indexes[0], out int startIndex))
            {
                throw new ArgumentException("Start index is not valid");
            }
            if (!int.TryParse(indexes[1], out int endIndex))
            {
                throw new ArgumentException("End index is not valid");
            }

            if (startIndex < 0 || endIndex < 0)
            {
                throw new ArgumentException("Start and end indexes must be positive");
            }

            if (startIndex > endIndex)
            {
                throw new ArgumentException("Range end must be greater or equal to start");
            }

            int rangeSize = endIndex - startIndex + 1;
            if (rangeSize > MAX_SIZE)
            {
                throw new ArgumentException($"Range size must be less than {MAX_SIZE}");
            }
            return query.Skip(startIndex).Take(endIndex - startIndex + 1);

        }

        public static IQueryable<T> FilterByProperties<T>(this IQueryable<T> query, Dictionary<string, object> filterCriteria)
        {
            if (filterCriteria == null || filterCriteria.Count == 0)
            {
                return query;
            }

            var parameter = Expression.Parameter(typeof(T));
            Expression? condition = null;

            foreach (var kvp in filterCriteria)
            {
                var property = Expression.Property(parameter, kvp.Key);
                var value = Expression.Constant(kvp.Value);

                if (kvp.Value == null) continue;
                Expression? equality = Expression.Equal(property, value);

                equality = Expression.Equal(property, value);

                condition = condition == null ? equality : Expression.AndAlso(condition, equality);

            }

            if (condition != null)
            {
                var predicat = Expression.Lambda<Func<T, bool>>(condition, parameter);
                return query.Where(predicat);
            }

            return query;
        }
        public static IQueryable<T> SortByProperties<T>(this IQueryable<T> query, string? sort, bool? desc)
        {
            desc??= false;
            string orderByMethodName = desc.Value ? "OrderByDescending" : "OrderBy";
            IOrderedQueryable<T> getOrderedQuery<T>(string propertyName, IOrderedQueryable<T> query, bool firstOrderBy = true)
            {
                var propertyInfo = typeof(T).GetProperty(propertyName);
                var propertyType = propertyInfo?.PropertyType;
                if (propertyInfo == null) return query;

                var parameter = Expression.Parameter(typeof(T));
                var propertyAccess = Expression.Property(parameter, propertyInfo);
                var orderExpressionType = typeof(Func<,>).MakeGenericType(typeof(T), propertyInfo.PropertyType);
                var orderExpression = Expression.Lambda(orderExpressionType, propertyAccess, parameter);

                MethodInfo getOrderByMethod(string methodName)
                {
                    return typeof(Queryable).GetMethods()
            .Where(method => method.Name == methodName && method.IsGenericMethodDefinition)
            .First()
            .MakeGenericMethod(typeof(T), propertyInfo.PropertyType);
                }
                MethodInfo orderByMethod;

                orderByMethod = firstOrderBy ? getOrderByMethod(orderByMethodName) : getOrderByMethod("ThenBy");


                return (IOrderedQueryable<T>)orderByMethod.Invoke(null, new object[] { query, orderExpression })!;


            }

            if (string.IsNullOrWhiteSpace(sort)) return query;

            var properties = sort.Split(',').Select(s => s.Trim()).ToList();

            var firstProp = properties.First();

            IOrderedQueryable<T> orderedQuery = getOrderedQuery<T>(firstProp, (IOrderedQueryable<T>)query);
            properties = properties.Skip(1).ToList();

            foreach (var prop in properties)
            {
                orderedQuery = getOrderedQuery<T>(prop, orderedQuery, firstOrderBy: false);
            }
            return orderedQuery;
        }
    }
}

