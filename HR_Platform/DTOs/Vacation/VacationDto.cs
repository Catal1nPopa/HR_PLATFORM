namespace HR_PLATFORM.DTOs.Vacation
{
    public class VacationDto
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
