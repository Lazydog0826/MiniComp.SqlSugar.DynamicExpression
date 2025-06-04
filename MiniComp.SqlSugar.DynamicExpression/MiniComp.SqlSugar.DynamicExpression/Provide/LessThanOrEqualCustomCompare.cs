using System.Linq.Expressions;

namespace MiniComp.SqlSugar.DynamicExpression.Provide;

/// <summary>
/// 小于等于
/// </summary>
public class LessThanOrEqualCustomCompare : ICustomCompare
{
    public Expression Compare(Expression expressionA, Expression expressionB) =>
        Expression.LessThanOrEqual(expressionA, expressionB);
}
