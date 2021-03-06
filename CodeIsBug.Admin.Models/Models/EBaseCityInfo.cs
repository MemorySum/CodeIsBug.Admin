using System.Collections.Generic;
using SqlSugar;
namespace CodeIsBug.Admin.Models.Models
{
    [SugarTable("e_base_cityinfo")]
    public class EBaseCityInfo
    {
        [SugarColumn(IsNullable = false, IsPrimaryKey = true,IsIdentity = true,ColumnDescription = "城市ID")]
        public int CodeId { get; set; }
        [SugarColumn(IsNullable = false,  ColumnDescription = "父级ID")]
        public int ParentId { get; set; }
        [SugarColumn(IsNullable = false, ColumnDescription = "城市名称")]
        public string CityName  { get; set; }
        [SugarColumn(IsIgnore = true)]
        public List<EBaseCityInfo> Children { get; set; }

    }
}