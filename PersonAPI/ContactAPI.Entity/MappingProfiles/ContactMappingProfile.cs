using AutoMapper;
using ContactReport.Entity;
using ContactReport.Entity.Enum;
using System;

namespace ContactAPI.Entity
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ContactDto>().ReverseMap();

            CreateMap<CommInfo, CommInfoDto>()
                .ForMember(dest => dest.CommInfoType, opt => opt.MapFrom(src => src.CommInfoType.ToString()));

            CreateMap<CommInfoDto, CommInfo>()
           .ForMember(dest => dest.CommInfoType, opt =>
           {
               opt.MapFrom(src => Enum.Parse(typeof(CommType), src.CommInfoType));
           });
        }
    }
}
