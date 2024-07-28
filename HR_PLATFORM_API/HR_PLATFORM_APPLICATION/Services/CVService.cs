using HR_PLATFORM_APPLICATION.Interface;
using HR_PLATFORM_APPLICATION.Model.CV;
using HR_PLATFORM_DOMAIN.Entity.CV;
using HR_PLATFORM_DOMAIN.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_APPLICATION.Services
{
    public class CVService(ICVRepository cVRepository) : ICVService
    {
        private readonly ICVRepository _cvRepository = cVRepository;
        public async Task AddCV(string fileName, byte[] fileData, int codeEmployee, string contentType)
        {
            await _cvRepository.AddCV(fileName, fileData, codeEmployee, contentType);
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
