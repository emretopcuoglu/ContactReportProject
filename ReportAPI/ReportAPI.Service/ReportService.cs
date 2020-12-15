using AutoMapper;
using ContactReport.Core;
using ContactReport.DataAccess;
using ContactReport.Entity;
using ContactReport.Entity.Enum;
using Microsoft.EntityFrameworkCore;
using ReportAPI.Core;
using ReportAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportAPI.Service
{
    public class ReportService : IReport
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public ReportService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ApiResult<ReportDto> Get(Guid reportId)
        {
            ApiResult<ReportDto> result = new ApiResult<ReportDto>();
            try
            {
                var report = _mapper.Map<ReportDto>(_context.Report.Where(x => x.ReportId == reportId).FirstOrDefault());
                if (report != null)
                {
                    result.Success = true;
                    result.Data = report;
                    result.Message = "Success";
                }
                else
                {
                    result.Success = false;
                    result.Message = "Kayıt bulunamadı";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.ToString();
                result.Success = false;
            }
            return result;
        }

        public ApiResult<List<ReportDto>> GetAll()
        {
            ApiResult<List<ReportDto>> result = new ApiResult<List<ReportDto>>();
            try
            {
                var reportList = _mapper.Map<List<ReportDto>>(_context.Report.ToList());
                if (reportList != null)
                {
                    result.Success = true;
                    result.Data = reportList;
                    result.Message = "Success";
                }
                else
                {
                    result.Success = false;
                    result.Message = "Kayıt bulunamadı";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.ToString();
                result.Success = false;
            }
            return result;
        }

        public ApiResult<ReportDto> RequestReport(string location)
        {
            ApiResult<ReportDto> result = new ApiResult<ReportDto>();
            try
            {
                if (string.IsNullOrEmpty(location))
                {
                    result.Message = "Konum bilgisi bulumadı";
                    result.Success = false;
                }
                else
                {
                    Report report = new Report()
                    {
                        ReportDate = DateTime.Now,
                        ReportStatus = ReportStatus.GettingReady,
                        Location = location
                    };
                    _context.Report.Add(report);
                    int res = _context.SaveChanges();
                    if (res > 0)
                    {
                        result.Data = _mapper.Map<ReportDto>(report);
                        result.Message = "Rapor talebi oluşturuldu";
                        result.Success = true;
                    }
                    else
                    {
                        result.Message = "Talep sırasında hata oluştu";
                        result.Success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.ToString();
                result.Success = false;
            }
            return result;
        }

        public void Update(Guid id, string location)
        {
            var report = _context.Report.Where(x => x.ReportId == id).FirstOrDefault();
            report.ReportStatus = ReportStatus.Completed;

            report.ContactCount = _context.CommInfo.Where(x => x.CommInfoType == CommType.Location && x.CommInfoContent == location).Count();

            report.PhoneNumberCount = _context.Contact.Where(x => x.CommInfos.Any(y => y.CommInfoType == CommType.Phone)
            && x.CommInfos.Any(y => y.CommInfoContent == location))
            .SelectMany(x => x.CommInfos.Where(y => y.CommInfoType == CommType.Phone)).Count();

            _context.Entry(report).State = EntityState.Modified;
            _context.Report.Update(report);
            _context.SaveChanges();
        }

        public ApiResult Create(ReportDto entity)
        {
            throw new NotImplementedException();
        }

        public ApiResult Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
