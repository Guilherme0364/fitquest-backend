using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace FitQuest.Infraestructure.Migrations.Versions
{
    // Essa classe irá conter as algumas informações que todas as Entidades adicionadas em migrações irão conter
    public abstract class VersionBase : ForwardOnlyMigration
    {
        protected ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string table)
        {
            return Create.Table(table)
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("CreatedOn").AsDateTime().NotNullable()
                .WithColumn("IsActive").AsBoolean().NotNullable();
        }
    }
}
