namespace MiniComp.SqlSugar.DynamicExpression.Model;

public class ConditionModel
{
    /// <summary>
    /// 表别名
    /// </summary>
    public string TableAlias { get; set; } = string.Empty;

    /// <summary>
    /// 属性名称
    /// </summary>
    public string PropName { get; set; } = string.Empty;

    /// <summary>
    /// 运算类型
    /// </summary>
    public ComparisonTypeEnum OperatorSymbol { get; set; } = ComparisonTypeEnum.Equal;

    /// <summary>
    /// 常量值
    /// </summary>
    public object? Value { get; set; }

    /// <summary>
    /// 组连接类型
    /// </summary>
    public GroupTypeEnum GroupType { get; set; } = GroupTypeEnum.And;

    /// <summary>
    /// 组
    /// </summary>
    public List<ConditionModel> Group { get; set; } = [];
}
