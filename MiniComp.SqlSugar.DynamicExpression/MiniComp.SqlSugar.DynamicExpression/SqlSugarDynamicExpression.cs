using System.Linq.Expressions;
using MiniComp.Core.Extension;
using MiniComp.SqlSugar.DynamicExpression.Model;
using SqlSugar;

namespace MiniComp.SqlSugar.DynamicExpression;

public static class SqlSugarDynamicExpression
{
    /// <summary>
    /// 生成表达式参数集合
    /// </summary>
    /// <param name="queryBuilder"></param>
    /// <returns></returns>
    private static List<ParameterExpression> GetParameterExpressionList(QueryBuilder queryBuilder)
    {
        List<ParameterExpression> parameterExpressions = [];
        var pairs = new Dictionary<string, Type>();
        // 连表查询
        if (queryBuilder.JoinExpression != null)
        {
            parameterExpressions =
                (queryBuilder.JoinExpression as LambdaExpression)?.Parameters.ToList()
                ?? throw new Exception("表达式别名获取失败");
        }
        // 单实体查询（在这之前有指定Where）
        if (queryBuilder.LambdaExpressions.Expression != null)
        {
            parameterExpressions =
                (queryBuilder.LambdaExpressions.Expression as LambdaExpression)?.Parameters.ToList()
                ?? throw new Exception("表达式别名获取失败");
        }
        if (parameterExpressions.Count == 0)
        {
            // 如果在这之前没有指定表达式参数别名则默认为"x"
            pairs.Add("x", queryBuilder.EntityType);
        }
        else
        {
            parameterExpressions.ForEach(x =>
            {
                if (!string.IsNullOrWhiteSpace(x.Name))
                {
                    pairs.Add(x.Name, x.Type);
                }
            });
        }
        return pairs.Keys.Select(alias => Expression.Parameter(pairs[alias], alias)).ToList();
    }

    #region 查询条件

    private static Expression? CreateWhereExpression(
        List<ConditionModel> search,
        List<ParameterExpression> paramExs
    )
    {
        var groupType = search.First().GroupType;
        var exList = new List<Expression>();
        search.ForEach(x =>
        {
            if (x.Group.NoNullAny())
            {
                var temEx = CreateWhereExpression(x.Group, paramExs);
                if (temEx != null)
                    exList.Add(temEx);
            }
            else
            {
                var currParamEx = paramExs.FirstOrDefault(x2 => x2.Name == x.TableAlias);
                if (currParamEx == null || x.Value == null)
                    return;
                if (!currParamEx.Type.IncludeProp(x.PropName))
                    return;
                var ex = DynamicExpression.CreateWhere(
                    currParamEx,
                    x.PropName,
                    x.Value,
                    x.OperatorSymbol
                );
                exList.Add(ex);
            }
        });
        return exList.Count switch
        {
            0 => null,
            1 => exList.First(),
            _ => groupType == GroupTypeEnum.And
                ? DynamicExpression.And(exList)
                : DynamicExpression.Or(exList),
        };
    }

    public static ISugarQueryable<T0> Where<T0>(
        this ISugarQueryable<T0> iq,
        List<ConditionModel> search
    )
    {
        var paramExs = GetParameterExpressionList(iq.QueryBuilder);
        var expression = CreateWhereExpression(search, paramExs);
        if (expression == null)
            return iq;
        var where = Expression.Lambda<Func<T0, bool>>(expression, [.. paramExs]);
        iq = iq.Where(where);
        return iq;
    }

    public static ISugarQueryable<T0, T1> Where<T0, T1>(
        this ISugarQueryable<T0, T1> iq,
        List<ConditionModel> search
    )
    {
        var paramExs = GetParameterExpressionList(iq.QueryBuilder);
        var expression = CreateWhereExpression(search, paramExs);
        if (expression == null)
            return iq;
        var where = Expression.Lambda<Func<T0, T1, bool>>(expression, [.. paramExs]);
        iq = iq.Where(where);
        return iq;
    }

    public static ISugarQueryable<T0, T1, T2> Where<T0, T1, T2>(
        this ISugarQueryable<T0, T1, T2> iq,
        List<ConditionModel> search
    )
    {
        var paramExs = GetParameterExpressionList(iq.QueryBuilder);
        var expression = CreateWhereExpression(search, paramExs);
        if (expression == null)
            return iq;
        var where = Expression.Lambda<Func<T0, T1, T2, bool>>(expression, [.. paramExs]);
        iq = iq.Where(where);
        return iq;
    }

    public static ISugarQueryable<T0, T1, T2, T3> Where<T0, T1, T2, T3>(
        this ISugarQueryable<T0, T1, T2, T3> iq,
        List<ConditionModel> search
    )
    {
        var paramExs = GetParameterExpressionList(iq.QueryBuilder);
        var expression = CreateWhereExpression(search, paramExs);
        if (expression == null)
            return iq;
        var where = Expression.Lambda<Func<T0, T1, T2, T3, bool>>(expression, [.. paramExs]);
        iq = iq.Where(where);
        return iq;
    }

    public static ISugarQueryable<T0, T1, T2, T3, T4> Where<T0, T1, T2, T3, T4>(
        this ISugarQueryable<T0, T1, T2, T3, T4> iq,
        List<ConditionModel> search
    )
    {
        var paramExs = GetParameterExpressionList(iq.QueryBuilder);
        var expression = CreateWhereExpression(search, paramExs);
        if (expression == null)
            return iq;
        var where = Expression.Lambda<Func<T0, T1, T2, T3, T4, bool>>(expression, [.. paramExs]);
        iq = iq.Where(where);
        return iq;
    }

    public static ISugarQueryable<T0, T1, T2, T3, T4, T5> Where<T0, T1, T2, T3, T4, T5>(
        this ISugarQueryable<T0, T1, T2, T3, T4, T5> iq,
        List<ConditionModel> search
    )
    {
        var paramExs = GetParameterExpressionList(iq.QueryBuilder);
        var expression = CreateWhereExpression(search, paramExs);
        if (expression == null)
            return iq;
        var where = Expression.Lambda<Func<T0, T1, T2, T3, T4, T5, bool>>(
            expression,
            [.. paramExs]
        );
        iq = iq.Where(where);
        return iq;
    }

    public static ISugarQueryable<T0, T1, T2, T3, T4, T5, T6> Where<T0, T1, T2, T3, T4, T5, T6>(
        this ISugarQueryable<T0, T1, T2, T3, T4, T5, T6> iq,
        List<ConditionModel> search
    )
    {
        var paramExs = GetParameterExpressionList(iq.QueryBuilder);
        var expression = CreateWhereExpression(search, paramExs);
        if (expression == null)
            return iq;
        var where = Expression.Lambda<Func<T0, T1, T2, T3, T4, T5, T6, bool>>(
            expression,
            [.. paramExs]
        );
        iq = iq.Where(where);
        return iq;
    }

    public static ISugarQueryable<T0, T1, T2, T3, T4, T5, T6, T7> Where<
        T0,
        T1,
        T2,
        T3,
        T4,
        T5,
        T6,
        T7
    >(this ISugarQueryable<T0, T1, T2, T3, T4, T5, T6, T7> iq, List<ConditionModel> search)
    {
        var paramExs = GetParameterExpressionList(iq.QueryBuilder);
        var expression = CreateWhereExpression(search, paramExs);
        if (expression == null)
            return iq;
        var where = Expression.Lambda<Func<T0, T1, T2, T3, T4, T5, T6, T7, bool>>(
            expression,
            [.. paramExs]
        );
        iq = iq.Where(where);
        return iq;
    }

    #endregion 查询条件

    #region 排序

    public static ISugarQueryable<T0> OrderBy<T0>(
        this ISugarQueryable<T0> iq,
        List<SortModel> sorts
    )
    {
        var paramExs = GetParameterExpressionList(iq.QueryBuilder);
        sorts.ForEach(x =>
        {
            var currParamEx = paramExs.FirstOrDefault(x2 => x2.Name == x.TableAlias);
            if (currParamEx == null)
                return;
            if (!currParamEx.Type.IncludeProp(x.PropName))
                return;
            var prop = Expression.PropertyOrField(currParamEx, x.PropName);
            var convertedProp = Expression.Convert(prop, typeof(object));
            var lambda = Expression.Lambda<Func<T0, object>>(convertedProp, paramExs);
            iq = x.SortType == SortTypeEnum.Asc ? iq.OrderBy(lambda) : iq.OrderByDescending(lambda);
        });
        return iq;
    }

    public static ISugarQueryable<T0, T1> OrderBy<T0, T1>(
        this ISugarQueryable<T0, T1> iq,
        List<SortModel> sorts
    )
    {
        var paramExs = GetParameterExpressionList(iq.QueryBuilder);
        sorts.ForEach(x =>
        {
            var currParamEx = paramExs.FirstOrDefault(x2 => x2.Name == x.TableAlias);
            if (currParamEx == null)
                return;
            if (!currParamEx.Type.IncludeProp(x.PropName))
                return;
            var prop = Expression.PropertyOrField(currParamEx, x.PropName);
            var convertedProp = Expression.Convert(prop, typeof(object));
            var lambda = Expression.Lambda<Func<T0, T1, object>>(convertedProp, paramExs);
            iq = x.SortType == SortTypeEnum.Asc ? iq.OrderBy(lambda) : iq.OrderByDescending(lambda);
        });
        return iq;
    }

    public static ISugarQueryable<T0, T1, T2> OrderBy<T0, T1, T2>(
        this ISugarQueryable<T0, T1, T2> iq,
        List<SortModel> sorts
    )
    {
        var paramExs = GetParameterExpressionList(iq.QueryBuilder);
        sorts.ForEach(x =>
        {
            var currParamEx = paramExs.FirstOrDefault(x2 => x2.Name == x.TableAlias);
            if (currParamEx == null)
                return;
            if (!currParamEx.Type.IncludeProp(x.PropName))
                return;
            var prop = Expression.PropertyOrField(currParamEx, x.PropName);
            var convertedProp = Expression.Convert(prop, typeof(object));
            var lambda = Expression.Lambda<Func<T0, T1, T2, object>>(convertedProp, paramExs);
            iq = x.SortType == SortTypeEnum.Asc ? iq.OrderBy(lambda) : iq.OrderByDescending(lambda);
        });
        return iq;
    }

    public static ISugarQueryable<T0, T1, T2, T3> OrderBy<T0, T1, T2, T3>(
        this ISugarQueryable<T0, T1, T2, T3> iq,
        List<SortModel> sorts
    )
    {
        var paramExs = GetParameterExpressionList(iq.QueryBuilder);
        sorts.ForEach(x =>
        {
            var currParamEx = paramExs.FirstOrDefault(x2 => x2.Name == x.TableAlias);
            if (currParamEx == null)
                return;
            if (!currParamEx.Type.IncludeProp(x.PropName))
                return;
            var prop = Expression.PropertyOrField(currParamEx, x.PropName);
            var convertedProp = Expression.Convert(prop, typeof(object));
            var lambda = Expression.Lambda<Func<T0, T1, T2, T3, object>>(convertedProp, paramExs);
            iq = x.SortType == SortTypeEnum.Asc ? iq.OrderBy(lambda) : iq.OrderByDescending(lambda);
        });
        return iq;
    }

    public static ISugarQueryable<T0, T1, T2, T3, T4> OrderBy<T0, T1, T2, T3, T4>(
        this ISugarQueryable<T0, T1, T2, T3, T4> iq,
        List<SortModel> sorts
    )
    {
        var paramExs = GetParameterExpressionList(iq.QueryBuilder);
        sorts.ForEach(x =>
        {
            var currParamEx = paramExs.FirstOrDefault(x2 => x2.Name == x.TableAlias);
            if (currParamEx == null)
                return;
            if (!currParamEx.Type.IncludeProp(x.PropName))
                return;
            var prop = Expression.PropertyOrField(currParamEx, x.PropName);
            var convertedProp = Expression.Convert(prop, typeof(object));
            var lambda = Expression.Lambda<Func<T0, T1, T2, T3, T4, object>>(
                convertedProp,
                paramExs
            );
            iq = x.SortType == SortTypeEnum.Asc ? iq.OrderBy(lambda) : iq.OrderByDescending(lambda);
        });
        return iq;
    }

    public static ISugarQueryable<T0, T1, T2, T3, T4, T5> OrderBy<T0, T1, T2, T3, T4, T5>(
        this ISugarQueryable<T0, T1, T2, T3, T4, T5> iq,
        List<SortModel> sorts
    )
    {
        var paramExs = GetParameterExpressionList(iq.QueryBuilder);
        sorts.ForEach(x =>
        {
            var currParamEx = paramExs.FirstOrDefault(x2 => x2.Name == x.TableAlias);
            if (currParamEx == null)
                return;
            if (!currParamEx.Type.IncludeProp(x.PropName))
                return;
            var prop = Expression.PropertyOrField(currParamEx, x.PropName);
            var convertedProp = Expression.Convert(prop, typeof(object));
            var lambda = Expression.Lambda<Func<T0, T1, T2, T3, T4, T5, object>>(
                convertedProp,
                paramExs
            );
            iq = x.SortType == SortTypeEnum.Asc ? iq.OrderBy(lambda) : iq.OrderByDescending(lambda);
        });
        return iq;
    }

    public static ISugarQueryable<T0, T1, T2, T3, T4, T5, T6> OrderBy<T0, T1, T2, T3, T4, T5, T6>(
        this ISugarQueryable<T0, T1, T2, T3, T4, T5, T6> iq,
        List<SortModel> sorts
    )
    {
        var paramExs = GetParameterExpressionList(iq.QueryBuilder);
        sorts.ForEach(x =>
        {
            var currParamEx = paramExs.FirstOrDefault(x2 => x2.Name == x.TableAlias);
            if (currParamEx == null)
                return;
            if (!currParamEx.Type.IncludeProp(x.PropName))
                return;
            var prop = Expression.PropertyOrField(currParamEx, x.PropName);
            var convertedProp = Expression.Convert(prop, typeof(object));
            var lambda = Expression.Lambda<Func<T0, T1, T2, T3, T4, T5, T6, object>>(
                convertedProp,
                paramExs
            );
            iq = x.SortType == SortTypeEnum.Asc ? iq.OrderBy(lambda) : iq.OrderByDescending(lambda);
        });
        return iq;
    }

    public static ISugarQueryable<T0, T1, T2, T3, T4, T5, T6, T7> OrderBy<
        T0,
        T1,
        T2,
        T3,
        T4,
        T5,
        T6,
        T7
    >(this ISugarQueryable<T0, T1, T2, T3, T4, T5, T6, T7> iq, List<SortModel> sorts)
    {
        var paramExs = GetParameterExpressionList(iq.QueryBuilder);
        sorts.ForEach(x =>
        {
            var currParamEx = paramExs.FirstOrDefault(x2 => x2.Name == x.TableAlias);
            if (currParamEx == null)
                return;
            if (!currParamEx.Type.IncludeProp(x.PropName))
                return;
            var prop = Expression.PropertyOrField(currParamEx, x.PropName);
            var convertedProp = Expression.Convert(prop, typeof(object));
            var lambda = Expression.Lambda<Func<T0, T1, T2, T3, T4, T5, T6, T7, object>>(
                convertedProp,
                paramExs
            );
            iq = x.SortType == SortTypeEnum.Asc ? iq.OrderBy(lambda) : iq.OrderByDescending(lambda);
        });
        return iq;
    }

    #endregion 排序
}
