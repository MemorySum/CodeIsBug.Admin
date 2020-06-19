using CodeIsBug.Admin.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeIsBug.Admin.Models.DbContext
{
    public class CodeIsBugContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public CodeIsBugContext(DbContextOptions<CodeIsBugContext> options) : base(options)
        {

        }
        public DbSet<ESysMenu> ESysMenus { get; set; }
        public DbSet<EBaseEmp> EBaseEmps { get; set; }
        public DbSet<ESyEmpRoleMap> ESyEmpRoleMaps { get; set; }
        public DbSet<ESysRoles> ESysRoles { get; set; }
    }
}
