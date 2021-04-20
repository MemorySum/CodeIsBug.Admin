using System;
using System.Threading.Tasks;
using StackExchange.Redis;
namespace CodeIsBug.Admin.Common.Helper
{
    public class RedisHelper
    {
        public RedisHelper(string connection)
        {
            Redis = ConnectionMultiplexer.Connect(connection);
            Db = Redis.GetDatabase();
        }

        private ConnectionMultiplexer Redis { get; }
        private IDatabase Db { get; }


        /// <summary>
        ///     增加/修改
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool SetValue(string key, string value, TimeSpan? expiry = null)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));
            return Db.StringSet(key, value, expiry);
        }
        /// <summary>
        ///     存储-异步
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">有效时间</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">参数为空异常</exception>
        public async Task<bool> SetValueAsync(string key, string value, TimeSpan? expiry = null)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));
            return await Db.StringSetAsync(key, value, expiry);
        }

        /// <summary>
        ///     查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public RedisValue GetValue(string key)
        {
            return Db.StringGet(key);
        }
        public async Task<RedisValue> GetValueAsync(string key)
        {
            try
            {
                return await Db.StringGetAsync(key);
            }
            catch (Exception)
            {
                return RedisValue.EmptyString;
            }
        }
        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool DeleteKey(string key)
        {
            return Db.KeyDelete(key);
        }

        public async Task<bool> DeleteKeyAsync(string key)
        {
            return await Db.KeyDeleteAsync(key);
        }
    }
}
