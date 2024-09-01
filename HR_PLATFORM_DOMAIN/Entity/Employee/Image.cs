using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_DOMAIN.Entity.Employee
{
    public class Image
    {
        public int CodEmployee { get; set; }
        public byte[] FileData { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}
