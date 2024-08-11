using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_APPLICATION.Model.Vacation
{
    public class VacationModel
    {
        public int Id { get; set; }
        public int CodEmployee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DaysVacation { get; set; }
        public int VacationDaysLeft { get; set; }
        public string TypeVacation { get; set; }
    }
}
