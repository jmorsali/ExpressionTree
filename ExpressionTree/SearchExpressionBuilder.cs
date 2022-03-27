using System.Linq.Expressions;
using System.Reflection;

public static class SearchExpressionBuilder
{

    public static Expression<Func<TEntity, bool>> SearchExpression<TEntity, TEntityDto>(TEntityDto dto)
    {
        var entity = Expression.Parameter(typeof(TEntity));
        var ExpresionItems = new List<Expression>();

        var dtoProperties = typeof(TEntityDto).GetProperties(BindingFlags.Public | BindingFlags.Instance);


        foreach (var dtoProperty in dtoProperties)
        {
            var value = dtoProperty.GetValue(dto);

            var entityParamAttribute = dtoProperty.GetCustomAttributes<EntityParam>().FirstOrDefault();
            string? entityParamAttributeValue = string.Empty;
            if (entityParamAttribute != null)
                entityParamAttributeValue = entityParamAttribute.GetType().GetProperty("Name")?.GetValue(entityParamAttribute)?.ToString();

            var entityParam = string.IsNullOrWhiteSpace(entityParamAttributeValue) ?
                                                                    typeof(TEntity).GetProperty(dtoProperty.Name) :
                                                                    typeof(TEntity).GetProperty(entityParamAttributeValue);

            if (entityParam == null)
                continue;


            var equalAttribute = dtoProperty.GetCustomAttributes<Equal>().FirstOrDefault();
            var containAttribute = dtoProperty.GetCustomAttributes<Contain>().FirstOrDefault();
            var lessThanAttribute = dtoProperty.GetCustomAttributes<LessThan>().FirstOrDefault();
            var greaterThanAttribute = dtoProperty.GetCustomAttributes<GreaterThan>().FirstOrDefault();


            if (dtoProperty.PropertyType == typeof(string)
                 && (value == null || string.IsNullOrWhiteSpace(value.ToString())))
                continue;

            if (dtoProperty.PropertyType == typeof(long?) && (value as long?) == null)
                continue;

            if (dtoProperty.PropertyType == typeof(int?) && (value as int?) == null)
                continue;


            if (equalAttribute != null)
            {
                var expression = Expression.Equal(Expression.Property(entity, entityParam), Expression.Constant(value));
                ExpresionItems.Add(expression);
            }
            if (lessThanAttribute != null)
            {
                var expression = Expression.LessThanOrEqual(Expression.Property(entity, entityParam), Expression.Constant(value));
                ExpresionItems.Add(expression);
            }
            if (greaterThanAttribute != null)
            {
                var expression = Expression.GreaterThanOrEqual(Expression.Property(entity, entityParam), Expression.Constant(value));
                ExpresionItems.Add(expression);
            }
            if (containAttribute != null)
            {
                MethodInfo? method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var containexpression = Expression.Call(Expression.Property(entity, entityParam), method!, Expression.Constant(value));
                ExpresionItems.Add(containexpression);
            }


        }

        var comparisonExpression = ExpresionItems.Aggregate(Expression.And);
        return Expression.Lambda<Func<TEntity, bool>>(comparisonExpression, entity);
    }
}