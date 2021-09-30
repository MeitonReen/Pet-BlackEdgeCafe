using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Cafe.Infrastructure.ETagCache.Databases.Migrations.Sqlite
{
	public partial class ETagCacheSqliteContext_Initial : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "ETags",
				columns: table => new
				{
					ClientId = table.Column<Guid>(type: "TEXT", nullable: false),
					GetResourceId = table.Column<Guid>(type: "TEXT", nullable: false),
					UrlGetResource = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
					ETagString = table.Column<string>(type: "TEXT", maxLength: 44, nullable: false),
					RowVersion = table.Column<byte[]>(type: "BLOB", nullable: true),
					SqliteVersion = table.Column<int>(type: "INTEGER", rowVersion: true, nullable: false, defaultValue: 0)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ETags_ClientIdGetResourceId", x => new { x.ClientId, x.GetResourceId });
				});

			migrationBuilder.Sql(
				@"CREATE TRIGGER UpdateETagsVersion
				AFTER UPDATE ON ETags
				BEGIN
					UPDATE ETags
					SET SqliteVersion = SqliteVersion + 1
					WHERE rowid = NEW.rowid;
				END;");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "ETags");
		}
	}
}
