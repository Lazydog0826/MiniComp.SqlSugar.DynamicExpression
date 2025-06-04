using System.Linq.Expressions;

namespace MiniComp.SqlSugar.DynamicExpression;

public interface ICustomCompare
{
    /// <summary>
    /// 自定义比较方法
    /// </summary>
    /// <param name="expressionA"></param>
    /// <param name="expressionB"></param>
    /// <returns></returns>
    public Expression Compare(Expression expressionA, Expression expressionB);
}
