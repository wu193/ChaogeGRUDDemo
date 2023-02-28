using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Culture.Dto
{
 public   class PageInputDto
    {

        /// <summary>
        /// 当前页标
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { set; get; }

    }
}
