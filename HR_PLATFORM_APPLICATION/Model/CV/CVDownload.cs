using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_APPLICATION.Model.CV
{
    public class CVDownload
    {
        public int CodEmployee { get; set; }
        public byte[] CVData { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }

        public CVDownload() { }
        public CVDownload(int codEmployee, byte[] cV, string fileName, string contentType)
        {
            CodEmployee = codEmployee;
            CVData = cV;
            FileName = fileName;
            ContentType = contentType;
        }   
    }
}
