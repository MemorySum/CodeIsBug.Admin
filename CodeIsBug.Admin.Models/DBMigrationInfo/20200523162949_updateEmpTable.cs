using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeIsBug.Admin.Models.DBMigrationInfo
{
    public partial class updateEmpTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_e_Base_Emp",
                table: "e_Base_Emp");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "e_Base_Emp");

            migrationBuilder.AddPrimaryKey(
                name: "PK_e_Base_Emp",
                table: "e_Base_Emp",
                column: "UserGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_e_Base_Emp",
                table: "e_Base_Emp");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "e_Base_Emp",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_e_Base_Emp",
                table: "e_Base_Emp",
                column: "Id");
        }
    }
}
