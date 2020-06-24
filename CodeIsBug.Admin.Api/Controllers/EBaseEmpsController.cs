using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeIsBug.Admin.Models.DbContext;
using CodeIsBug.Admin.Models.Models;

namespace CodeIsBug.Admin.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EBaseEmpsController : ControllerBase
    {
        private readonly CodeIsBugContext _context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public EBaseEmpsController(CodeIsBugContext context)
        {
            _context = context;
        }
        /// <summary>
        /// GetEBaseEmps
        /// </summary>
        /// <returns></returns>
        // GET: api/EBaseEmps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EBaseEmp>>> GetEBaseEmps()
        {
            return await _context.EBaseEmps.ToListAsync();
        }

       
    }
}
