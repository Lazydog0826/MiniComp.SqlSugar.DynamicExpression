namespace MiniComp.SqlSugar.DynamicExpression.Model;

public enum ComparisonTypeEnum
{
    /// <summary>
    /// 相等
    /// </summary>
    Equal,

    /// <summary>
    /// 等于空
    /// </summary>
    EqualNull,

    /// <summary>
    /// 不相等
    /// </summary>
    NotEqual,

    /// <summary>
    /// 不等于空
    /// </summary>
    NotEqualNull,

    /// <summary>
    /// 大于
    /// </summary>
    GreaterThan,

    /// <summary>
    /// 大于等于
    /// </summary>
    GreaterThanOrEqual,

    /// <summary>
    /// 小于
    /// </summary>
    LessThan,

    /// <summary>
    /// 小于等于
    /// </summary>
    LessThanOrEqual,

    /// <summary>
    /// 属性包含目标
    /// </summary>
    StringContains,

    /// <summary>
    /// 属性不包含目标
    /// </summary>
    StringNotContains,

    /// <summary>
    /// 目标包含属性
    /// </summary>
    ListContains,

    /// <summary>
    /// 目标不包含属性
    /// </summary>
    ListNotContains,
}
