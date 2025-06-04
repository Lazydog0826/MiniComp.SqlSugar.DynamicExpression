using System.Linq.Expressions;

namespace MiniComp.SqlSugar.DynamicExpression.Provide;

/// <summary>
/// 等于
/// </summary>
public class EqualCustomCompare : ICustomCompare
{
    public Expression Compare(Expression expressionA, Expression expressionB) =>
        Expression.Equal(expressionA, expressionB);
}
