using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodeIsBug.Admin.Common.Helper;
namespace CodeIsBug.Admin.UI.Controllers
{
    public class VerifyCodeController : Controller
    {
        public FileContentResult NumberVerifyCode()
        {
            string code = VerifyCodeHelper.GetSingleObj().CreateVerifyCode(VerifyCodeHelper.VerifyCodeType.NumberVerifyCode);
            byte[] codeImage = VerifyCodeHelper.GetSingleObj().CreateByteByImgVerifyCode(code, 100, 40);
            return File(codeImage, @"image/jpeg");
        }
    }
}