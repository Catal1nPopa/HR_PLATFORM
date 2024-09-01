namespace HR_PLATFORM.DTOs.Employee
{
    public class UploadImage
    {
        public int EmployeeId { get; set; }
        public IFormFile File { get; set; }
    }
}
