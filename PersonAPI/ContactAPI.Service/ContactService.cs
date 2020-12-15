using AutoMapper;
using ContactAPI.Core;
using ContactAPI.Entity;
using ContactReport.Core;
using ContactReport.DataAccess;
using ContactReport.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactAPI.Service
{
    public class ContactService : IContact
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public ContactService(DatabaseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public ApiResult Create(ContactDto contact)
        {
            ApiResult result = new ApiResult();
            try
            {
                if (contact != null)
                {
                    _context.Add(_mapper.Map<Contact>(contact));
                    int res = _context.SaveChanges();
                    if (res > 0)
                    {
                        result.Message = "Kayıt başarılı";
                        result.Success = true;
                    }
                    else
                    {
                        result.Message = "Kayıt sırasında hata oluştu";
                        result.Success = false;
                    }
                }
                else
                {
                    result.Message = "Veri bulunamadı";
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.ToString();
                result.Success = false;
            }
            return result;
        }

        public ApiResult CreateRelation(Guid contactId, CommInfoDto info)
        {
            ApiResult result = new ApiResult();
            try
            {
                var commInfo = _mapper.Map<CommInfo>(info);
                var contact = _context.Contact.Include(p => p.CommInfos).FirstOrDefault(x => x.ContactId == contactId);
                if (contact != null)
                {
                    commInfo.Contact = contact;
                    _context.CommInfo.Add(commInfo);
                    int res = _context.SaveChanges();
                    if (res > 0)
                    {
                        result.Message = "İletişim bilgisi kayıtı başarılı";
                        result.Success = true;
                    }
                    else
                    {
                        result.Message = "İletişim bilgisi kayıtı sırasında hata oluştu";
                        result.Success = false;
                    }
                }
                else
                {
                    result.Message = "Kişi bulunamadı";
                    result.Success = false;
                }
            }
            catch (AutoMapperConfigurationException ex)
            {
                result.Message = ex.ToString();
                result.Success = false;
            }
            catch (Exception ex)
            {
                result.Message = ex.ToString();
                result.Success = false;
            }
            return result;
        }

        public ApiResult Delete(Guid contactId)
        {
            ApiResult result = new ApiResult();
            try
            {
                var contactCommunicationCount = _context.Contact.Include(p => p.CommInfos).FirstOrDefault(x => x.ContactId == contactId).CommInfos.Count();
                if (contactCommunicationCount > 0)
                {
                    result.Message = "İletişim bilgisi bulunan kişi silinemez";
                    result.Success = false;
                }
                else
                {
                    var contact = _context.Contact.Where(x => x.ContactId == contactId).FirstOrDefault();
                    if (contact != null)
                    {
                        _context.Contact.Remove(contact);
                        int res = _context.SaveChanges();
                        if (res > 0)
                        {
                            result.Message = "Silme başarılı";
                            result.Success = true;
                        }
                        else
                        {
                            result.Message = "Silme sırasında hata oluştu";
                            result.Success = false;
                        }
                    }
                    else
                    {
                        result.Message = "Kayıt bulunamadı";
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

        public ApiResult DeleteRelation(Guid infoId)
        {
            ApiResult result = new ApiResult();
            try
            {
                var contactInfo = _context.CommInfo.Where(x => x.CommInfoId == infoId).FirstOrDefault();
                if (contactInfo != null)
                {
                    _context.CommInfo.Remove(contactInfo);
                    var res = _context.SaveChanges();
                    if (res > 0)
                    {
                        result.Message = "İletişim bilgisi silme başarılı";
                        result.Success = true;
                    }
                    else
                    {
                        result.Message = "İletişim bilgisi silme sırasında hata oluştu";
                        result.Success = false;
                    }
                }
                else
                {
                    result.Message = "Kişi iletişim bilgisi bulunamadı";
                    result.Success = false;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.ToString();
                result.Success = false;
            }
            return result;
        }

        public ApiResult<ContactDto> Get(Guid contactId)
        {
            ApiResult<ContactDto> result = new ApiResult<ContactDto>();
            try
            {
                var contact = _mapper.Map<ContactDto>(_context.Contact.Where(x => x.ContactId == contactId).FirstOrDefault());
                if (contact != null)
                {
                    result.Success = true;
                    result.Data = contact;
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

        public ApiResult<List<ContactDto>> GetAll()
        {
            ApiResult<List<ContactDto>> result = new ApiResult<List<ContactDto>>();
            try
            {
                var contactList = _mapper.Map<List<ContactDto>>(_context.Contact.ToList());
                if (contactList != null)
                {
                    result.Success = true;
                    result.Data = contactList;
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

        public ApiResult<ContactDto> GetRelationList(Guid contactId)
        {
            ApiResult<ContactDto> result = new ApiResult<ContactDto>();
            try
            {
                var contact = _mapper.Map<ContactDto>(_context.Contact.Include(p => p.CommInfos).FirstOrDefault(x => x.ContactId == contactId));

                if (contact != null)
                {
                    result.Success = true;
                    result.Data = contact;
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
    }
}
