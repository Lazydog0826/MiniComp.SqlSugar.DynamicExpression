using System.Linq.Expressions;

namespace MiniComp.SqlSugar.DynamicExpression.Provide;

/// <summary>
/// 大于
/// </summary>
public class GreaterThanCustomCompare : ICustomCompare
{
    public Expression Compare(Expression expressionA, Expression expressionB) =>
        Expression.GreaterThan(expressionA, expressionB);
}
