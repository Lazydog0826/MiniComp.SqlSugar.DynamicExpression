using System.Linq.Expressions;

namespace MiniComp.SqlSugar.DynamicExpression.Provide;

/// <summary>
/// 属性包含目标
/// </summary>
public class StringContainsCustomCompare : ICustomCompare
{
    public Expression Compare(Expression expressionA, Expression expressionB)
    {
        var method = expressionA.Type.GetMethod(nameof(string.Empty.Contains), [typeof(string)]);
        return Expression.Call(expressionA, method!, expressionB);
    }
}
