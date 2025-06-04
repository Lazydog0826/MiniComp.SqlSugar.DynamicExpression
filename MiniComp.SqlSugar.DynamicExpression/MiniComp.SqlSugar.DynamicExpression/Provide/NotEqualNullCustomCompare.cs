using System.Linq.Expressions;

namespace MiniComp.SqlSugar.DynamicExpression.Provide;

/// <summary>
/// 不等于空
/// </summary>
public class NotEqualNullCustomCompare : ICustomCompare
{
    public Expression Compare(Expression expressionA, Expression expressionB)
    {
        var constant = Expression.Constant(null, typeof(object));
        return Expression.NotEqual(expressionA, constant);
    }
}
