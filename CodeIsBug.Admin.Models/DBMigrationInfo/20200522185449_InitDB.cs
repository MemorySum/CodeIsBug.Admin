using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeIsBug.Admin.Models.DBMigrationInfo
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "e_Base_Emp",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Pwd = table.Column<string>(nullable: true),
                    IsDelete = table.Column<int>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    UserGuid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_e_Base_Emp", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "eSyEmpRoleMaps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmpId = table.Column<Guid>(nullable: true),
                    RoleId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eSyEmpRoleMaps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "eSysMenus",
                columns: table => new
                {
                    MenuId = table.Column<Guid>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Icon = table.Column<string>(nullable: true),
                    Sort = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<int>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eSysMenus", x => x.MenuId);
                });

            migrationBuilder.CreateTable(
                name: "ESysRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Sort = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    AddTime = table.Column<DateTime>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESysRoles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "e_Base_Emp");

            migrationBuilder.DropTable(
                name: "eSyEmpRoleMaps");

            migrationBuilder.DropTable(
                name: "eSysMenus");

            migrationBuilder.DropTable(
                name: "ESysRoles");
        }
    }
}
