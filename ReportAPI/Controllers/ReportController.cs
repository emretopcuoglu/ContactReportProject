using ContactReport.Core;
using Microsoft.AspNetCore.Mvc;
using ReportAPI.Core;
using ReportAPI.Entity;
using ReportAPI.Service;
using System;
using System.Collections.Generic;

namespace ReportAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReport _service;
        private readonly IRabbitService _rabbitService;

        public ReportController(IReport service, IRabbitService rabbitService)
        {
            _service = service;
            _rabbitService = rabbitService;
        }

        [HttpGet]
        public ApiResult<ReportDto> GetReport(Guid reportId)
        {
            return _service.Get(reportId);
        }

        [HttpGet]
        public ApiResult<List<ReportDto>> GetAllReport()
        {
            return _service.GetAll();
        }

        [HttpPost]
        public ApiResult CreateReport(string location)
        {
            var result = _service.RequestReport(location);

            if (result.Success)
            {
                _rabbitService.SendMessage(result.Data.ReportId,result.Data.Location);
            }
            return result;
        }
    }
}
