using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Culture.ViewModel
{
 public   class PageModel<T>
    {

        /// <summary>
        /// 当前页标
        /// </summary>
        public int Page { get; set; } = 1;
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount { get; set; } = 6;
        /// <summary>
        /// 数据总数
        /// </summary>
        public int Total { get; set; } = 0;
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { set; get; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public List<T> Data { get; set; }


    }
}
