using System.Linq.Expressions;

namespace MiniComp.SqlSugar.DynamicExpression.Provide;

/// <summary>
/// 大于等于
/// </summary>
public class GreaterThanOrEqualCustomCompare : ICustomCompare
{
    public Expression Compare(Expression expressionA, Expression expressionB) =>
        Expression.GreaterThanOrEqual(expressionA, expressionB);
}
