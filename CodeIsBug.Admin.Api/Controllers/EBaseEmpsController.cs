using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeIsBug.Admin.Models.DbContext;
using CodeIsBug.Admin.Models.Models;
using Microsoft.AspNetCore.Authorization;

namespace CodeIsBug.Admin.Api.Controllers
{
    [Authorize]
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
       

       
    }


}
