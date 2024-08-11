using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_APPLICATION.Model.CV
{
    public class CVModel
    {
        public IFormFile File { get; set; }
        public int CodeEmployee { get; set; }
    }
}
