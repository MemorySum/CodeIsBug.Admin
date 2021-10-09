using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeIsBug.Admin.Models.Enums;
using CodeIsBug.Admin.Models.Models;
using CodeIsBug.Admin.Services.Base;

namespace CodeIsBug.Admin.Services.Service
{
    public class SysErrorLogService : BaseService<ESysErrorLog>
    {
        public async Task<List<ESysErrorLog>> GetAll()
        {
            return await Context.Queryable<ESysErrorLog>().ToListAsync();
        }
    }
}
