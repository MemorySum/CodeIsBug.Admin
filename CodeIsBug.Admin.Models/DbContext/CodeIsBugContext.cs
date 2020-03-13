using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
namespace CodeIsBug.Admin.Models
{
    public class CodeIsBugContext : DbContext
    {
        public CodeIsBugContext(DbContextOptions<CodeIsBugContext> options) : base(options)
        {

        }
        public DbSet<ESysMenu> eSysMenus { get; set; }
        public DbSet<EBaseEmp> eBaseEmps { get; set; }
        public DbSet<ESyEmpRoleMap> eSyEmpRoleMaps { get; set; }
        public DbSet<ESysRoles> ESysRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
