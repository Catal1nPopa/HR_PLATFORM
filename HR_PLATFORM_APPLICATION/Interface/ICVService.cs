using HR_PLATFORM_APPLICATION.Model.CV;
using HR_PLATFORM_DOMAIN.Entity.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_APPLICATION.Interface
{
    public interface ICVService
    {
        Task AddCV(string fileName, byte[] fileData, int codeEmployee, string contentType);
        Task<CVDownload> DownloadCV(int codeEmployee);
    }
}
