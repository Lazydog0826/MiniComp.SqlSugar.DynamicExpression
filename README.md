# MiniComp.SqlSugar.DynamicExpression

```csharp
// 注入比较实现
builder.Services.AddComparisonType();
var app = builder.Build();
// 应用程序的配置服务
HostApp.RootServiceProvider = app.Services;
```

```csharp
public class TestModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public int TestModel2Id { get; set; }
}

public class TestModel2
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Quantity { get; set; }
}
```

## 单实体查询

```csharp
[HttpPost("Test")]
public IActionResult Test([FromBody] DynamicExpressionSearchRequest request)
{
    var sql = _db.Queryable<TestModel>()
        .Where(request.Search)
        .OrderBy(request.Sort)
        .ToSqlString();
    return Ok(sql);
}
```

```json
{
    "Search": [
        {
            "TableAlias": "x",
            "PropName": "Name",
            "OperatorSymbol": 0,
            "Value": "abc",
            "GroupType": 0
        }
    ],
    "Sort": [
        {
            "TableAlias": "x",
            "PropName": "Name",
            "SortType": 1
        }
    ]
}
```

```sql
SELECT `Id`, `Name`, `IsActive`, `TestModel2Id`
FROM `TestModel`
WHERE (`Name` = N'abc')
ORDER BY `Name` DESC;
```

```json
{
    "Search": [
        {
            "TableAlias": "x",
            "PropName": "Name",
            "OperatorSymbol": 0,
            "Value": "abc",
            "GroupType": 0
        },
        {
            "TableAlias": "x",
            "PropName": "Id",
            "OperatorSymbol": 10,
            "Value": [
                15038557427,
                16622141896
            ]
        },
        {
            "TableAlias": "x",
            "PropName": "IsActive",
            "OperatorSymbol": 0,
            "Value": true
        }
    ],
    "Sort": [
        {
            "TableAlias": "x",
            "PropName": "Name",
            "SortType": 1
        },
        {
            "TableAlias": "x",
            "PropName": "Id",
            "SortType": 0
        }
    ]
}
```

```sql
SELECT `Id`, `Name`, `IsActive`, `TestModel2Id`
FROM `TestModel`
WHERE (((`Name` = N'abc') AND (`Id` IN (15038557427, 16622141896))) AND (`IsActive` = 1))
ORDER BY `Name` DESC, `Id` ASC;
```

```json
{
    "Search": [
        {
            "TableAlias": "x",
            "PropName": "Name",
            "OperatorSymbol": 0,
            "Value": "abc",
            "GroupType": 1
        },
        {
            "Group": [
                {
                    "TableAlias": "x",
                    "PropName": "Id",
                    "OperatorSymbol": 10,
                    "Value": [
                        15038557427,
                        16622141896
                    ],
                    "GroupType": 0
                },
                {
                    "TableAlias": "x",
                    "PropName": "IsActive",
                    "OperatorSymbol": 0,
                    "Value": true
                }
            ]
        }
    ],
    "Sort": [
        {
            "TableAlias": "x",
            "PropName": "Name",
            "SortType": 1
        },
        {
            "TableAlias": "x",
            "PropName": "Id",
            "SortType": 0
        }
    ]
}
```

```sql
SELECT `Id`, `Name`, `IsActive`, `TestModel2Id`
FROM `TestModel`
WHERE ((`Name` = N'abc') OR ((`Id` IN (15038557427, 16622141896)) AND (`IsActive` = 1)))
ORDER BY `Name` DESC, `Id` ASC;
```

## 连表查询

```csharp
[HttpPost("Test2")]
public IActionResult Test2([FromBody] DynamicExpressionSearchRequest request)
{
    var sql = _db.Queryable<TestModel>()
        .LeftJoin<TestModel2>((t1, t2) => t1.TestModel2Id == t2.Id)
        .Where(request.Search)
        .OrderBy(request.Sort)
        .ToSqlString();
    return Ok(sql);
}
```

```json
{
    "Search": [
        {
            "TableAlias": "t1",
            "PropName": "Name",
            "OperatorSymbol": 0,
            "Value": "abc",
            "GroupType": 1
        },
        {
            "Group": [
                {
                    "TableAlias": "t1",
                    "PropName": "Id",
                    "OperatorSymbol": 10,
                    "Value": [
                        15038557427,
                        16622141896
                    ],
                    "GroupType": 0
                },
                {
                    "TableAlias": "t1",
                    "PropName": "IsActive",
                    "OperatorSymbol": 0,
                    "Value": true
                }
            ]
        },
        {
            "Group": [
                {
                    "TableAlias": "t2",
                    "PropName": "Title",
                    "OperatorSymbol": 0,
                    "Value": "egf"
                }
            ]
        }
    ],
    "Sort": [
        {
            "TableAlias": "t1",
            "PropName": "Name",
            "SortType": 1
        },
        {
            "TableAlias": "t1",
            "PropName": "Id",
            "SortType": 0
        },
        {
            "TableAlias": "t2",
            "PropName": "Quantity",
            "SortType": 0
        }
    ]
}
```

```sql
SELECT `t1`.`Id`, `t1`.`Name`, `t1`.`IsActive`, `t1`.`TestModel2Id`
FROM `TestModel` `t1`
         Left JOIN `TestModel2` `t2` ON (`t1`.`TestModel2Id` = `t2`.`Id`)
WHERE (((`t1`.`Name` = N'abc') OR ((`t1`.`Id` IN (15038557427, 16622141896)) AND (`t1`.`IsActive` = 1))) OR
       (`t2`.`Title` = N'egf'))
ORDER BY `t1`.`Name` DESC, `t1`.`Id` ASC, `t2`.`Quantity` ASC;
```
