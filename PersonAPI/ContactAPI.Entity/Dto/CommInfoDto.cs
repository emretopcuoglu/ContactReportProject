using ContactReport.Entity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactAPI.Entity
{
    public class CommInfoDto: IValidatableObject
    {
        public Guid? CommInfoId { get; set; }
        
        public string CommInfoType { get; set; }

        public string CommInfoContent { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Enum.TryParse(CommInfoType, true, out CommType result))
            {
                yield return new ValidationResult("Geçersiz iletişim tipi", new[] { nameof(CommType) });
            }
            CommInfoType = result.ToString();
        }
    }
}
