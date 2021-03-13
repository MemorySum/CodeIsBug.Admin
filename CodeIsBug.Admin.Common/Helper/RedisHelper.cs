using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeIsBug.Admin.Common.Helper
{
    public class RedisHelper
    {

        private ConnectionMultiplexer Redis { get; set; }
        private IDatabase Db { get; set; }
        public RedisHelper(string connection)
        {
            Redis = ConnectionMultiplexer.Connect(connection);
            Db = Redis.GetDatabase();
        }

       
        /// <summary>
        /// 增加/修改
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool SetValue(string key, string value, TimeSpan? expiry = null)
        {
            return Db.StringSet(key, value, expiry);
        }
        public async Task<bool> SetValueAsync(string key, string value, TimeSpan? expiry = null)
        {
            return await Db.StringSetAsync(key, value, expiry);
        }

        /// <summary>
        /// 查询
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
        /// 删除
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
