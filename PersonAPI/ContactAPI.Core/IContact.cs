using ContactAPI.Entity;
using ContactReport.Core;
using System;

namespace ContactAPI.Core
{
    public interface IContact : IRepository<ContactDto>
    {
        ApiResult CreateRelation(Guid id, CommInfoDto entity);
        ApiResult DeleteRelation(Guid infoId);
        ApiResult<ContactDto> GetRelationList(Guid id);
    }
}
