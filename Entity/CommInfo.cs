using ContactReport.Entity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace ContactReport.Entity
{
    [Table("CommunicationInfo")]
    public class CommInfo
    {
        public Guid CommInfoId { get; set; }

        [Required]
        public CommType CommInfoType { get; set; }

        [StringLength(200), Required]
        public string CommInfoContent { get; set; }

        [ForeignKey("ContactId")]
        public Contact Contact { get; set; }
    }
}
