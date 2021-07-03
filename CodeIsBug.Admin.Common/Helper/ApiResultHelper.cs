using SqlSugar;

namespace CodeIsBug.Admin.Common.Helper
{
    /// <summary>
    ///     ApiResult统一返回类
    /// </summary>
    public static class ApiResultHelper
    {
        /// <summary>
        ///     自定义成功消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static Result Success(string msg)
        {
            return new()
            {
                Code = 1,
                Message = msg
            };
        }

        /// <summary>
        ///     success
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Result Success(dynamic data)
        {
            return new()
            {
                Code = 1,
                Message = "Success",
                Object = data,
                ExtendObject = null,
                Total = 0
            };
        }

        /// <summary>
        ///     自定义消息和data
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Result Success(string msg, dynamic data)
        {
            return new()
            {
                Code = 1,
                Message = msg,
                Object = data,
                Total = 0,
                ExtendObject = null
            };
        }

        /// <summary>
        ///     自定义data或者扩展属性
        /// </summary>
        /// <param name="data"></param>
        /// <param name="extendData"></param>
        /// <returns></returns>
        public static Result Success(dynamic data, dynamic extendData)
        {
            return new()
            {
                Code = 1,
                Message = "Success",
                Object = data,
                ExtendObject = extendData,
                Total = 0
            };
        }

        /// <summary>
        ///     自定义消息、data、扩展属性
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <param name="extendData"></param>
        /// <returns></returns>
        public static Result Success(string msg, dynamic data, dynamic extendData)
        {
            return new()
            {
                Code = 1,
                Message = msg,
                Object = data,
                ExtendObject = extendData,
                Total = 0
            };
        }

        /// <summary>
        ///     succeed and total总数
        /// </summary>
        /// <param name="data"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static Result Success(dynamic data, RefAsync<int> total)
        {
            return new()
            {
                Code = 1,
                Message = "Success",
                Object = data,
                Total = total
            };
        }


        /// <summary>
        ///     错误消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static Result Error(string msg)
        {
            return new()
            {
                Code = 500,
                Message = msg,
                Object = null,
                ExtendObject = null,
                Total = 0
            };
        }

        /// <summary>
        ///     错误消息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static Result Error(int code, string msg)
        {
            return new()
            {
                Code = code,
                Message = msg,
                Object = null,
                ExtendObject = null,
                Total = 0
            };
        }

        /// <summary>
        ///     自定义错误消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static Result Failed(string msg)
        {
            return new()
            {
                Code = 0,
                Message = msg,
                Total = 0,
                Object = null,
                ExtendObject = null
            };
        }

        /// <summary>
        ///     自定义错误码和消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Result Failed(string msg, int code)
        {
            return new()
            {
                Code = code,
                Message = msg,
                Total = 0,
                Object = null,
                ExtendObject = null
            };
        }
    }
}