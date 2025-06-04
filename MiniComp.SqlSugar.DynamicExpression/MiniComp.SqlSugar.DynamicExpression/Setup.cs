using Microsoft.Extensions.DependencyInjection;
using MiniComp.Core.Extension;
using MiniComp.SqlSugar.DynamicExpression.Model;
using MiniComp.SqlSugar.DynamicExpression.Provide;

namespace MiniComp.SqlSugar.DynamicExpression;

public static class Setup
{
    /// <summary>
    /// 注入动态表达式比较实现
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddComparisonType(this IServiceCollection services)
    {
        services.Inject(
            nameof(ComparisonTypeEnum.Equal),
            ServiceLifetime.Singleton,
            typeof(ICustomCompare),
            typeof(EqualCustomCompare)
        );
        services.Inject(
            nameof(ComparisonTypeEnum.EqualNull),
            ServiceLifetime.Singleton,
            typeof(ICustomCompare),
            typeof(EqualNullCustomCompare)
        );
        services.Inject(
            nameof(ComparisonTypeEnum.NotEqual),
            ServiceLifetime.Singleton,
            typeof(ICustomCompare),
            typeof(NotEqualCustomCompare)
        );
        services.Inject(
            nameof(ComparisonTypeEnum.NotEqualNull),
            ServiceLifetime.Singleton,
            typeof(ICustomCompare),
            typeof(NotEqualNullCustomCompare)
        );
        services.Inject(
            nameof(ComparisonTypeEnum.GreaterThan),
            ServiceLifetime.Singleton,
            typeof(ICustomCompare),
            typeof(GreaterThanCustomCompare)
        );
        services.Inject(
            nameof(ComparisonTypeEnum.GreaterThanOrEqual),
            ServiceLifetime.Singleton,
            typeof(ICustomCompare),
            typeof(GreaterThanOrEqualCustomCompare)
        );
        services.Inject(
            nameof(ComparisonTypeEnum.LessThan),
            ServiceLifetime.Singleton,
            typeof(ICustomCompare),
            typeof(LessThanCustomCompare)
        );
        services.Inject(
            nameof(ComparisonTypeEnum.LessThanOrEqual),
            ServiceLifetime.Singleton,
            typeof(ICustomCompare),
            typeof(LessThanOrEqualCustomCompare)
        );
        services.Inject(
            nameof(ComparisonTypeEnum.StringContains),
            ServiceLifetime.Singleton,
            typeof(ICustomCompare),
            typeof(StringContainsCustomCompare)
        );
        services.Inject(
            nameof(ComparisonTypeEnum.StringNotContains),
            ServiceLifetime.Singleton,
            typeof(ICustomCompare),
            typeof(StringNotContainsCustomCompare)
        );
        services.Inject(
            nameof(ComparisonTypeEnum.ListContains),
            ServiceLifetime.Singleton,
            typeof(ICustomCompare),
            typeof(ListContainsCustomCompare)
        );
        services.Inject(
            nameof(ComparisonTypeEnum.ListNotContains),
            ServiceLifetime.Singleton,
            typeof(ICustomCompare),
            typeof(ListNotContainsCustomCompare)
        );

        return services;
    }
}
