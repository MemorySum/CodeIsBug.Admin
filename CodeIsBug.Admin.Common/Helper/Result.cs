namespace CodeIsBug.Admin.Common.Helper
{
    /// <summary>
    ///     统一返回结果
    /// </summary>
    public class Result
    {
        /// <summary>
        ///     状态码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        ///     返回消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        ///     对象
        /// </summary>
        public object Object { get; set; }
        /// <summary>
        ///     扩展对象
        /// </summary>
        public object ExtendObject { get; set; }
    }
}
