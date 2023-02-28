using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationSqlSugar.ViewModel
{
    public class ResultViewModel
    {
        public string Msg { get; set; }
        public int Code { get; set; }
        public object Data { get; set; }
    }
}