using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactReport.Entity
{
    [Table("Contact")]
    public class Contact
    {
        public Guid ContactId { get; set; }

        [StringLength(50), Required]
        public string Name { get; set; }

        [StringLength(50), Required]
        public string Surname { get; set; }

        [StringLength(100)]
        public string Company { get; set; }

        public List<CommInfo> CommInfos { get; set; }

        public List<Report> Reports { get; set; }
    }
}
