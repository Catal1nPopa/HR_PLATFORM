namespace HR_PLATFORM_DOMAIN.Entity.Salary
{
    public class SalaryHistory
    {
        public int CodeEmployee { get; set; }
        public decimal Salary_Brut { get; set; }
        public decimal Salary_Net { get; set; }
        public DateTime Data_Send { get; set; }
    }
}
