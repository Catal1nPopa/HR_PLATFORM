using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_APPLICATION.Model.Employee
{
    public class GetImage
    {
        public int CodEmployee { get; set; }
        public byte[] CVData { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }

        public GetImage() { }
        public GetImage(int codEmployee, byte[] cV, string fileName, string contentType)
        {
            CodEmployee = codEmployee;
            CVData = cV;
            FileName = fileName;
            ContentType = contentType;
        }
    }
}
