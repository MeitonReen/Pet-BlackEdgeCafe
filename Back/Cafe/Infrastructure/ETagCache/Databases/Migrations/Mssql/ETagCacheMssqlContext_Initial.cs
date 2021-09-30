using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Cafe.Infrastructure.ETagCache.Databases.Migrations.Mssql
{
	public partial class ETagCacheMssqlContext_Initial : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "ETags",
				columns: table => new
				{
					ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					GetResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
					UrlGetResource = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
					ETagString = table.Column<string>(type: "nvarchar(44)", maxLength: 44, nullable: false),
					RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
					SqliteVersion = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ETags_ClientIdGetResourceId", x => new { x.ClientId, x.GetResourceId });
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "ETags");
		}
	}
}
