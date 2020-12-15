using ContactReport.Core;
using ReportAPI.Entity;
using System;

namespace ReportAPI.Core
{
    public interface IReport : IRepository<ReportDto>
    {
        ApiResult<ReportDto> RequestReport(string location);
        void Update(Guid id, string location);
    }
}
