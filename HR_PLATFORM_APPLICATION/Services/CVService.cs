using HR_PLATFORM_APPLICATION.Interface;
using HR_PLATFORM_APPLICATION.Model.CV;
using HR_PLATFORM_DOMAIN.Interface;

namespace HR_PLATFORM_APPLICATION.Services
{
    public class CVService(ICVRepository cVRepository) : ICVService
    {
        private readonly ICVRepository _cvRepository = cVRepository;
        public async Task AddCV(string fileName, byte[] fileData, int codeEmployee, string contentType)
        {
            var checkCV = await _cvRepository.DownloadCV(codeEmployee);
            if (checkCV != null)
            {
                await _cvRepository.UpdateCV(fileName, fileData, codeEmployee, contentType);
            }
            else
            {
                await _cvRepository.AddCV(fileName, fileData, codeEmployee, contentType);
            }
        }

        public async Task<CVDownload> DownloadCV(int codeEmployee)
        {
            var result = await _cvRepository.DownloadCV(codeEmployee);

            var cv = new CVDownload
            {
                CodEmployee = result.CodEmployee,
                CVData = result.FileData,
                FileName = result.FileName,
                ContentType = result.ContentType,
            };

            return cv;
        }
    }
}
