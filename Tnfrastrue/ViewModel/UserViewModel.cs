using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tnfrastrue.ViewModel
{
  public class UserViewModel
    {
        // id
        public int id { get; set; }
        //名称
        public string name { get; set; }
        // 密码
        public string pass { get; set; }

        //创建时间
        public DateTime createtime { get; set; }
    }
}
