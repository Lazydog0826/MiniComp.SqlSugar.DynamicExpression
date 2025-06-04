using System.Linq.Expressions;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using MiniComp.Core.App;
using MiniComp.Core.Extension;
using MiniComp.SqlSugar.DynamicExpression.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MiniComp.SqlSugar.DynamicExpression;

public static class DynamicExpression
{
    #region 并且或者

    /// <summary>
    /// 或者
    /// </summary>
    /// <param name="expressionA"></param>
    /// <param name="expressionB"></param>
    /// <returns></returns>
    public static Expression Or(Expression expressionA, Expression expressionB) =>
        Expression.OrElse(
            (expressionA as LambdaExpression)?.Body ?? expressionA,
            (expressionB as LambdaExpression)?.Body ?? expressionB
        );

    /// <summary>
    /// 并且
    /// </summary>
    /// <param name="expressionA"></param>
    /// <param name="expressionB"></param>
    /// <returns></returns>
    public static Expression And(Expression expressionA, Expression expressionB) =>
        Expression.AndAlso(
            (expressionA as LambdaExpression)?.Body ?? expressionA,
            (expressionB as LambdaExpression)?.Body ?? expressionB
        );

    /// <summary>
    /// 或者
    /// </summary>
    /// <param name="expressions"></param>
    /// <returns></returns>
    public static Expression Or(List<Expression> expressions)
    {
        var i = expressions.First();
        expressions
            .Skip(1)
            .ToList()
            .ForEach(d =>
            {
                i = Or(i, d);
            });
        return i;
    }

    /// <summary>
    /// 并且
    /// </summary>
    /// <param name="expressions"></param>
    /// <returns></returns>
    public static Expression And(List<Expression> expressions)
    {
        var i = expressions.First();
        expressions
            .Skip(1)
            .ToList()
            .ForEach(d =>
            {
                i = And(i, d);
            });
        return i;
    }

    #endregion 并且或者

    public static Expression CreateWhere(
        ParameterExpression param,
        string propName,
        object target,
        ComparisonTypeEnum comparisonTypeEnum,
        ICustomCompare? customCompare = null
    )
    {
        var prop = Expression.PropertyOrField(param, propName);
        Type targetType;
        if (
            comparisonTypeEnum
            is ComparisonTypeEnum.ListContains
                or ComparisonTypeEnum.ListNotContains
        )
        {
            targetType = typeof(List<>).MakeGenericType([prop.Type]);
            target = target is JsonElement or JArray
                ? JsonConvert.DeserializeObject(target.ToString() ?? "[]", targetType)!
                : target;
        }
        else
        {
            target =
                target.ToString().ChangeType(prop.Type, null, true)
                ?? throw new Exception("类型转换错误");
            targetType = prop.Type;
        }
        var constant = Expression.Constant(target, targetType);
        customCompare ??= HostApp.RootServiceProvider.GetRequiredKeyedService<ICustomCompare>(
            comparisonTypeEnum.ToString()
        );
        return customCompare.Compare(prop, constant);
    }
}
