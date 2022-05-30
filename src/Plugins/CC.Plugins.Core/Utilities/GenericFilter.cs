using CC.Plugins.Core.Attributes;
using SmartStore.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace CC.Plugins.Core.Utilities
{
    public static class GenericFilter
    {
        private static readonly MethodInfo ContainsMethod = typeof(String).GetMethod("Contains", new Type[] { typeof(String) });
        private static readonly MethodInfo StartsWithMethod = typeof(String).GetMethod("StartsWith", new Type[] { typeof(String) });
        private static readonly MethodInfo EndsWithMethod = typeof(String).GetMethod("EndsWith", new Type[] { typeof(String) });

        public static ExpressionStarter<TEntity> BuildFilterQuery<TView, TEntity>(TView viewModel, TEntity entity =null)
                                    where TView : class
                                    where TEntity : class
        {
            ExpressionStarter<TEntity> predicate = PredicateBuilder.New<TEntity>(true);
            FilterFieldAttribute filterAttribute;
            Expression filterExpression = null;
            MethodCallExpression methodCallExpression = null;
            BinaryExpression binaryExpression = null;

            var parameter = Expression.Parameter(typeof(TEntity));

            foreach (PropertyInfo property in typeof(TView).GetProperties().Where(property => Attribute.IsDefined(property, typeof(FilterFieldAttribute))))             
            {
                var filterProperty = typeof(TEntity).GetProperty(property.Name);
                var propertyType = property.PropertyType;
                var propertyAccess = Expression.MakeMemberAccess(parameter, filterProperty);
                filterAttribute = (FilterFieldAttribute)property.GetCustomAttributes(typeof(FilterFieldAttribute), false).FirstOrDefault();
                var value = property.GetValue(viewModel);
                if (filterAttribute != null && value != null )
                {
                    var filterOperator = filterAttribute.FilterOperator;
                    switch (filterOperator)
                    {
                        case FilterOperator.TextEqual:
                            if (String.IsNullOrEmpty(value.ToString())) continue;
                            binaryExpression = GetEqualBinaryExpression(propertyAccess, value.ToString());
                             filterExpression = GetLogicalExpression(LogicalOperator.And, filterExpression, binaryExpression);
                           
                            break;
                        case FilterOperator.TextNotEqual:
                            if (String.IsNullOrEmpty(value.ToString())) continue;
                            binaryExpression = GetNotEqualBinaryExpression(propertyAccess, value.ToString());
                            filterExpression = GetLogicalExpression(LogicalOperator.And, filterExpression, binaryExpression);
                            break;
                        case FilterOperator.TextLike:
                            if (String.IsNullOrEmpty(value.ToString())) continue;
                            methodCallExpression = GetLikeExpression(propertyAccess, value.ToString());
                            filterExpression = GetMethodCallExpression(LogicalOperator.And, filterExpression, methodCallExpression);
                            break;
                        case FilterOperator.TextStartsWith:
                            if (String.IsNullOrEmpty(value.ToString())) continue;
                            methodCallExpression = GetStartsWithExpression(propertyAccess, value.ToString());
                            filterExpression = GetMethodCallExpression(LogicalOperator.And, filterExpression, methodCallExpression);
                            break;
                        case FilterOperator.TextEndsWith:
                            if (String.IsNullOrEmpty(value.ToString())) continue;
                            methodCallExpression = GetEndsWithExpression(propertyAccess, value.ToString());
                            filterExpression = GetMethodCallExpression(LogicalOperator.And, filterExpression, methodCallExpression);
                            break;
                        case FilterOperator.DateRangeFrom:
                           // if (value ==null) continue;
                            binaryExpression = GetDateGreaterThanOrEqualExp(propertyAccess, value.ToString(), propertyType);
                            filterExpression = GetLogicalExpression(LogicalOperator.And, filterExpression, binaryExpression);
                            break;
                        case FilterOperator.DateRangeTo:
                           // if (value == null) continue;
                            binaryExpression = GetDateLessThanOrEqualExp(propertyAccess, value.ToString(), propertyType);
                            filterExpression = GetLogicalExpression(LogicalOperator.And, filterExpression, binaryExpression);
                            break;
                        case FilterOperator.DateEqual:
                          //  if (value == null) continue;
                            binaryExpression = GetDateEqualExpression(propertyAccess, value.ToString(), propertyType);
                            filterExpression = GetLogicalExpression(LogicalOperator.And, filterExpression, binaryExpression);
                            break;
                        case FilterOperator.DateNotEqual:
                           // if (value == null) continue;
                            binaryExpression = GetDateNotEqualExpression(propertyAccess, value.ToString(), propertyType);
                            filterExpression = GetLogicalExpression(LogicalOperator.And, filterExpression, binaryExpression);
                            break;
                        case FilterOperator.Int16Equal:
                            if ((int)(value) == 0) continue;
                            binaryExpression = GetInt16EqualExpression(propertyAccess, value.ToString());
                            filterExpression = GetLogicalExpression(LogicalOperator.And, filterExpression, binaryExpression);
                            break;
                        case FilterOperator.Int16NotEqual:
                            if ((int)(value) == 0) continue;
                            binaryExpression = GetInt16NotEqualExpression(propertyAccess, value.ToString().ToString());
                            filterExpression = GetLogicalExpression(LogicalOperator.And, filterExpression, binaryExpression);
                            break;
                        case FilterOperator.Int32Equal:
                            if ((int)(value) == 0) continue;
                            binaryExpression = GetInt32EqualExpression(propertyAccess, value.ToString());
                            filterExpression = GetLogicalExpression(LogicalOperator.And, filterExpression, binaryExpression);
                            break;
                        case FilterOperator.Int32NotEqual:
                            if ((int)(value) == 0) continue;
                            binaryExpression = GetInt32NotEqualExpression(propertyAccess, value.ToString().ToString());
                            filterExpression = GetLogicalExpression(LogicalOperator.And, filterExpression, binaryExpression);
                            break;
                    }
                }
            }
            if (filterExpression != null)
            {
                predicate = Expression.Lambda<Func<TEntity, bool>>(filterExpression, parameter);
                Func<TEntity, bool> compiled = predicate.Compile();                
            }
            return (predicate);
        }

        static Expression GetLogicalExpression(string logicalOperatorId, Expression filterExpression, BinaryExpression binaryExpression)
        {
            switch (logicalOperatorId)
            {
                case LogicalOperator.And:
                    filterExpression = filterExpression == null ? binaryExpression : Expression.And(filterExpression, binaryExpression);
                    break;
                case LogicalOperator.Or:
                    filterExpression = filterExpression == null ? binaryExpression : Expression.Or(filterExpression, binaryExpression);
                    break;
                default:
                    filterExpression = filterExpression == null ? binaryExpression : Expression.And(filterExpression, binaryExpression);
                    break;
            }
            return filterExpression;
        }

       
        static Expression GetMethodCallExpression(string logicalOperatorId, Expression filterExpression, MethodCallExpression methodCallExpression)
        {
            switch (logicalOperatorId)
            {
                case LogicalOperator.And:
                    if (filterExpression == null)
                        filterExpression = methodCallExpression;
                    else
                        filterExpression = Expression.And(filterExpression, methodCallExpression);
                    break;
                case LogicalOperator.Or:
                    if (filterExpression == null)
                        filterExpression = methodCallExpression;
                    else
                        filterExpression = Expression.Or(filterExpression, methodCallExpression);
                    break;
                default:
                    if (filterExpression == null)
                        filterExpression = methodCallExpression;
                    else
                        filterExpression = Expression.And(filterExpression, methodCallExpression);
                    break;
            }
            return filterExpression;
        }



        static BinaryExpression GetEqualBinaryExpression(MemberExpression propertyAccess, string columnValue)
        {
            return Expression.Equal(GetLowerCasePropertyAccess(propertyAccess), Expression.Constant(columnValue.ToLower()));
        }

        static BinaryExpression GetNotEqualBinaryExpression(MemberExpression propertyAccess, string columnValue)
        {
            return Expression.NotEqual(GetLowerCasePropertyAccess(propertyAccess), Expression.Constant(columnValue.ToLower()));
        }

        static MethodCallExpression GetStartsWithExpression(MemberExpression propertyAccess, string columnValue)
        {
            MethodCallExpression methodCallExpression = Expression.Call(GetLowerCasePropertyAccess(propertyAccess), StartsWithMethod, Expression.Constant(columnValue.ToLower()));
            return methodCallExpression;
        }

        static MethodCallExpression GetEndsWithExpression(MemberExpression propertyAccess, string columnValue)
        {
            MethodCallExpression methodCallExpression = Expression.Call(GetLowerCasePropertyAccess(propertyAccess), EndsWithMethod, Expression.Constant(columnValue.ToLower()));
            return methodCallExpression;
        }

        static MethodCallExpression GetLikeExpression(MemberExpression propertyAccess, string columnValue)
        {
            MethodCallExpression methodCallExpression = Expression.Call(GetLowerCasePropertyAccess(propertyAccess), ContainsMethod, Expression.Constant(columnValue.ToLower()));
            return methodCallExpression;
        }

        static MethodCallExpression GetLowerCasePropertyAccess(MemberExpression propertyAccess)
        {
            return Expression.Call(Expression.Call(propertyAccess, "ToString", new Type[0]), typeof(string).GetMethod("ToLower", new Type[0]));
        }

        static BinaryExpression GetDateGreaterThanOrEqualExp(MemberExpression propertyAccess, string columnValue, Type propertyType)
        {
            Type columnType = propertyType == typeof(DateTime) ? typeof(DateTime) : typeof(DateTime?);

            BinaryExpression binaryExpression = Expression.GreaterThanOrEqual(propertyAccess,
                                                                       Expression.Constant(
                                                                        Convert.ToDateTime(columnValue),
                                                                        columnType));
            return binaryExpression;
        }

        static BinaryExpression GetDateLessThanOrEqualExp(MemberExpression propertyAccess, string columnValue, Type propertyType)
        {
            Type columnType = propertyType == typeof(DateTime) ? typeof(DateTime) : typeof(DateTime?);

            BinaryExpression binaryExpression = Expression.LessThanOrEqual(propertyAccess,
                                                                    Expression.Constant(Convert.ToDateTime(columnValue),
                                                                    columnType));
            return binaryExpression;
        }

        static BinaryExpression GetDateEqualExpression(MemberExpression propertyAccess, string columnValue, Type propertyType)
        {
            Type columnType = propertyType == typeof(DateTime) ? typeof(DateTime) : typeof(DateTime?);

            BinaryExpression binaryExpression = Expression.Equal(propertyAccess,
                                                                    Expression.Constant(Convert.ToDateTime(columnValue),
                                                                    columnType));
            return binaryExpression;
        }

        static BinaryExpression GetDateNotEqualExpression(MemberExpression propertyAccess, string columnValue, Type propertyType)
        {
            Type columnType = propertyType == typeof(DateTime) ? typeof(DateTime) : typeof(DateTime?);

            BinaryExpression binaryExpression = Expression.NotEqual(propertyAccess,
                                                                    Expression.Constant(Convert.ToDateTime(columnValue),
                                                                    columnType));
            return binaryExpression;
        }

        static BinaryExpression GetInt16EqualExpression(MemberExpression propertyAccess, string columnValue)
        {
            BinaryExpression binaryExpression = Expression.Equal(propertyAccess,
                                    Expression.Constant(Convert.ToInt16(columnValue), typeof(Int16)));
            return binaryExpression;
        }

        static BinaryExpression GetInt16NotEqualExpression(MemberExpression propertyAccess, string columnValue)
        {
            BinaryExpression binaryExpression = Expression.NotEqual(propertyAccess,
                                    Expression.Constant(Convert.ToInt16(columnValue), typeof(Int16)));
            return binaryExpression;
        }

        static BinaryExpression GetInt32EqualExpression(MemberExpression propertyAccess, string columnValue)
        {
            BinaryExpression binaryExpression = Expression.Equal(propertyAccess,
                                    Expression.Constant(Convert.ToInt32(columnValue), typeof(Int32)));
            return binaryExpression;
        }
        
        static BinaryExpression GetInt32NotEqualExpression(MemberExpression propertyAccess, string columnValue)
        {
            BinaryExpression binaryExpression = Expression.NotEqual(propertyAccess,
                                    Expression.Constant(Convert.ToInt32(columnValue), typeof(Int32)));
            return binaryExpression;
        }

        
        static Expression GetDummyExpression<T>(ParameterExpression parameter)
        {
            var dummyProperty = typeof(T).GetProperty("DocumentName");
            var dummyPropertyAccess = Expression.MakeMemberAccess(parameter, dummyProperty);
            return Expression.NotEqual(dummyPropertyAccess, Expression.Constant(string.Empty));
        }

    }

    public static class FilterOperator
    {
        public const string TextEqual = "textEqual";

        public const string TextNotEqual = "textNotEqual";

        public const string TextLike = "textLike";

        public const string TextStartsWith = "textStartsWith";

        public const string TextEndsWith = "textEndsWith";

        public const string DateRangeFrom = "dateRangeFrom";

        public const string DateRangeTo = "dateRangeTo";

        public const string DateEqual = "dateEqual";

        public const string DateNotEqual = "dateNotEqual";

        public const string Int16Equal = "int16Equal";

        public const string Int16NotEqual = "int16NotEqual";

        public const string Int32Equal = "int32Equal";

        public const string Int32NotEqual = "int32NotEqual";               

    }

    public static class LogicalOperator
    {
        public const string And = "and";

        public const string Or = "or";        
    }


    
}