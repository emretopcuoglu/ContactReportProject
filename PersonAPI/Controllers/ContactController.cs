using ContactAPI.Core;
using ContactAPI.Entity;
using ContactReport.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContact _service;

        public ContactController(IContact service)
        {
            _service = service;
        }

        [HttpGet]
        public ApiResult<List<ContactDto>> GetAllContact()
        {
            return _service.GetAll();
        }

        [HttpGet]
        public ApiResult<ContactDto> GetContact(Guid contactId)
        {
            return _service.Get(contactId);
        }

        [HttpGet]
        public ApiResult<ContactDto> GetContactAllCommunication(Guid contactId)
        {
            return _service.GetRelationList(contactId);
        }

        [HttpPost]
        public ApiResult CreateContact(ContactDto contact)
        {
            return _service.Create(contact);
        }

        [HttpPost]
        public ApiResult CreateCommunication(Guid contactId, CommInfoDto info)
        {
            if (!ModelState.IsValid) return new ApiResult();

            return _service.CreateRelation(contactId, info);
        }

        [HttpDelete]
        public ApiResult DeleteContact(Guid contactId)
        {
            return _service.Delete(contactId);
        }

        [HttpDelete]
        public ApiResult DeleteCommunication(Guid infoId)
        {
            return _service.DeleteRelation(infoId);
        }
    }
}
