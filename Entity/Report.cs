using ContactReport.Entity.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactReport.Entity
{
    [Table("Report")]
    public class Report
    {
        public Guid ReportId { get; set; }

        public DateTime ReportDate { get; set; }

        public ReportStatus ReportStatus { get; set; }

        public string Location { get; set; }

        public int? ContactCount { get; set; }

        public int? PhoneNumberCount { get; set; }

        [ForeignKey("ContactId")]
        public Contact Contact { get; set; }
    }
}
