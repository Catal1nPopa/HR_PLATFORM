using HR_PLATFORM_DOMAIN.Entity.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_DOMAIN.Interface
{
    public interface ICVRepository
    {
        Task AddCV(string fileName, byte[] fileData, int codeEmployee, string contentType);
        Task UpdateCV(string fileName, byte[] fileData, int codeEmployee, string contentType);
        Task<CV> DownloadCV(int codeEmployee);
    }
}
