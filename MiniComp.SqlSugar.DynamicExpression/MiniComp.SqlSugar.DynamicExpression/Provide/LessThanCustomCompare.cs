using System.Linq.Expressions;

namespace MiniComp.SqlSugar.DynamicExpression.Provide;

/// <summary>
/// 小于
/// </summary>
public class LessThanCustomCompare : ICustomCompare
{
    public Expression Compare(Expression expressionA, Expression expressionB) =>
        Expression.LessThan(expressionA, expressionB);
}
