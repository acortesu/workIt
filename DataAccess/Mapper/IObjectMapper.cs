using System.Collections.Generic;
using Entities;

namespace DataAccess.Mapper
{
    interface IObjectMapper
    {
        List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows);
        BaseEntity BuildObject(Dictionary<string, object> row);
    }
}