using System.Linq.Expressions;

namespace MiniComp.SqlSugar.DynamicExpression.Provide;

/// <summary>
/// 等于空
/// </summary>
public class EqualNullCustomCompare : ICustomCompare
{
    public Expression Compare(Expression expressionA, Expression expressionB)
    {
        var constant = Expression.Constant(null, typeof(object));
        return Expression.Equal(expressionA, constant);
    }
}
