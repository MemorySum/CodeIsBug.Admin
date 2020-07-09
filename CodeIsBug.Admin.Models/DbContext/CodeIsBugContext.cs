using CodeIsBug.Admin.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace CodeIsBug.Admin.Models.DbContext
{
	public class CodeIsBugContext : Microsoft.EntityFrameworkCore.DbContext
	{
		private readonly IConfiguration _configuration;

		public CodeIsBugContext(DbContextOptions<CodeIsBugContext> options, IConfiguration configuration) : base(options)
		{
			_configuration = configuration;
		}
		public static readonly ILoggerFactory CodeIsBugContextLogger
			= LoggerFactory.Create(builder => { builder.AddConsole(); });
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
				optionsBuilder.UseLoggerFactory(CodeIsBugContextLogger)
					.UseSqlServer(_configuration.GetConnectionString("codeIsBug.Admin"));
		}

		public DbSet<ESysMenu> ESysMenus { get; set; }
		public DbSet<EBaseEmp> EBaseEmps { get; set; }
		public DbSet<ESyEmpRoleMap> ESyEmpRoleMaps { get; set; }
		public DbSet<ESysRoles> ESysRoles { get; set; }
	}
}
