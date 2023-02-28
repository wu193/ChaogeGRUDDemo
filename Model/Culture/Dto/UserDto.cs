using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Culture.Dto
{
     public  class   UserDto : PageInputDto
    {

        public int id { get; set; }
        public string name { get; set; }
        public string pass { get; set; }
        public DateTime createtime { get; set; }
    }
}
