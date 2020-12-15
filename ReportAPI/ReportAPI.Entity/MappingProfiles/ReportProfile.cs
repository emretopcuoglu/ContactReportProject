using AutoMapper;
using ContactReport.Entity;
using ContactReport.Entity.Enum;
using System;

namespace ReportAPI.Entity
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<Report, ReportDto>()
                .ForMember(dest => dest.ReportStatus, opt => opt.MapFrom(src => src.ReportStatus.ToString()));

            CreateMap<ReportDto, Report>()
           .ForMember(dest => dest.ReportStatus, opt => opt.MapFrom(src => Enum.Parse(typeof(ReportStatus), src.ReportStatus)));
        }
    }
}
