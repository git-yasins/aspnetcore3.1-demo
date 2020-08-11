# EFDemo
#EF迁移命令
#dotnet ef migrations add init --创建迁移文件
#dotnet ef database update --更新数据库
#dotnet ef migrations script -o mysql_DBDemo.sql --生成SQL执行脚本
#类库使用EF迁移，项目目标类型不能是NETSTANDARD 须要为netcoreapp3.1