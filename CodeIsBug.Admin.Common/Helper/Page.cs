﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CodeIsBug.Admin.Common.Helper
{
    public class Page<T>
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// 集合总数
        /// </summary>
        public int TotalRows { get; set; }
        /// <summary>
        /// 每页项数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public IList<T> LsList { get; set; }
    }
}
