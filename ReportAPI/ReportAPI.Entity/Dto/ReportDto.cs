using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportAPI.Entity
{
    public class ReportDto
    {
        public Guid ReportId { get; set; }

        public DateTime ReportDate { get; set; }

        public string ReportStatus { get; set; }

        public string Location { get; set; }

        public int? ContactCount { get; set; }

        public int? PhoneNumberCount { get; set; }
    }
}
