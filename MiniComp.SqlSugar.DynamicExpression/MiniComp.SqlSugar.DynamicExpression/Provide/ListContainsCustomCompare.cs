using System.Linq.Expressions;

namespace MiniComp.SqlSugar.DynamicExpression.Provide;

/// <summary>
/// 目标包含属性
/// </summary>
public class ListContainsCustomCompare : ICustomCompare
{
    public Expression Compare(Expression expressionA, Expression expressionB)
    {
        var method = expressionB.Type.GetMethod(nameof(List<dynamic>.Contains));
        return Expression.Call(expressionB, method!, expressionA);
    }
}
