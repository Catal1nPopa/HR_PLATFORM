namespace HR_PLATFORM.DTOs.CV
{
    public class CVDownloadDTO
    {
        public int CodEmployee { get; set; }
        public byte[] CV_Data { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }

        public CVDownloadDTO() { }
        public CVDownloadDTO(int codEmployee, byte[] cV_Data, string fileName, string contentType)
        {
            CodEmployee = codEmployee;
            CV_Data = cV_Data;
            FileName = fileName;
            ContentType = contentType;
        }   
    }
}
