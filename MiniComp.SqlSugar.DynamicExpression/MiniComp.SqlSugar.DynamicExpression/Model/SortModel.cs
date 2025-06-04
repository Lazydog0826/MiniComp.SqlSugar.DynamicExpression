namespace MiniComp.SqlSugar.DynamicExpression.Model;

public class SortModel
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
    /// 排序方式
    /// </summary>
    public SortTypeEnum SortType { get; set; } = SortTypeEnum.Desc;
}
