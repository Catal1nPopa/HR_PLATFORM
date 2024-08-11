using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_DOMAIN.Entity.Vacation
{
    public class Vacation
    {
        public int Id { get; set; }
        public int CodEmployee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DaysVacation { get; set; }
        public int VacationDaysLeft { get; set; }
        public string TypeVacation { get; set; }

        public Vacation() { }

        public Vacation(int codVacation,int codEmployee, DateTime startDate, DateTime endDate, int daysVacation, int vacationDaysLeft, string typeVacation)
        {
            Id = codVacation;
            CodEmployee = codEmployee;
            StartDate = startDate;
            EndDate = endDate;
            DaysVacation = daysVacation;
            VacationDaysLeft = vacationDaysLeft;
            TypeVacation = typeVacation;
        }

        public Vacation( int codEmployee, DateTime startDate, DateTime endDate, int daysVacation, int vacationDaysLeft, string typeVacation)
        {
            CodEmployee = codEmployee;
            StartDate = startDate;
            EndDate = endDate;
            DaysVacation = daysVacation;
            VacationDaysLeft = vacationDaysLeft;
            TypeVacation = typeVacation;
        }
    }
}
