using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecureDataManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EncryptedDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlainText = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EncryptedText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IV = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PublicKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DecryptKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Algorithm = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncryptedDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DecryptedDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlainText = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DecryptedText = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    EncryptedDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DecryptedDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DecryptedDatas_EncryptedDatas_EncryptedDataId",
                        column: x => x.EncryptedDataId,
                        principalTable: "EncryptedDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DecryptedDatas_EncryptedDataId",
                table: "DecryptedDatas",
                column: "EncryptedDataId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DecryptedDatas");

            migrationBuilder.DropTable(
                name: "EncryptedDatas");
        }
    }
}
