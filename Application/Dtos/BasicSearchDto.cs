using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class BasicSearchDto
    {
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? ManufacturerYear { get; set; }
        public string? ModelYear { get; set; }
    }
}
