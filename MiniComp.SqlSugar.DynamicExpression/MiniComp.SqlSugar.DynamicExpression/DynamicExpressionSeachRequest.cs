using MiniComp.SqlSugar.DynamicExpression.Model;

namespace MiniComp.SqlSugar.DynamicExpression;

public class DynamicExpressionSearchRequest
{
    /// <summary>
    /// 动态表达式参数
    /// </summary>
    public List<ConditionModel> Search { get; set; } = [];

    /// <summary>
    /// 排序
    /// </summary>
    public List<SortModel> Sort { get; set; } = [];
}
