using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_INFRASTRUCTURE.Entities
{
    public class VacationTypeEntity
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PaymentPercentage { get; set; }
    }
}
