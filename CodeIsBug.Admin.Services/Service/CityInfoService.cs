using System.Collections.Generic;
using System.Threading.Tasks;
using CodeIsBug.Admin.Models.Models;
using CodeIsBug.Admin.Services.Base;
namespace CodeIsBug.Admin.Services.Service
{
    public class CityInfoService : BaseService<EBaseCityInfo>
    {

        public async Task<List<EBaseCityInfo>> GetCityInfoTree()
        {
            return await Context.Queryable<EBaseCityInfo>()
                .ToTreeAsync(sys => sys.Children,
                    sys => sys.ParentId, 0);
        }
    }
}
