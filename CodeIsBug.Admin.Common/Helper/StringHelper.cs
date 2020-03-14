﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CodeIsBug.Admin.Common.Helper
{
    public class StringHelper
    {
        /// <summary>
        /// 生成MD5加密字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Md5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "").ToLower();
            }
        }

    }
}
