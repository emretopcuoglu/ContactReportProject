using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactAPI.Entity
{
    public class ContactDto
    {
        public Guid ContactId { get; set; }

        [StringLength(50), Required]
        public string Name { get; set; }

        [StringLength(50), Required]
        public string Surname { get; set; }

        [StringLength(100)]
        public string Company { get; set; }

        public List<CommInfoDto> CommInfos { get; set; }
    }
}
