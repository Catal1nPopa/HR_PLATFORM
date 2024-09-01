namespace HR_PLATFORM.DTOs.Time
{
    public class TimexDto
    {
        public int CodeEmployee { get; set; }
        public DateTime TimeFirstEntry { get; set; }
        public DateTime TimeLastExit { get; set; }
        public string LocationEntry { get; set; }
        public string LocationExit { get; set; }
    }
}
