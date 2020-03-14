using CodeIsBug.Admin.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeIsBug.Admin.Repository.Repository
{
    public class EmpRepository  : BaseRepository<EBaseEmp>,IEmpRepository
    {
        private CodeIsBugContext _dbcontext;

        public EmpRepository(CodeIsBugContext context):base(context)
        {
            this._dbcontext = context;
        }
    }
}
