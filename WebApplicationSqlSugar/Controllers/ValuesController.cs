using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebApplicationSqlSugar.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string[] fileList = GetFiles();
            foreach (var v in fileList)
            {
                context.Response.Write("<li>" + v + "</li>");
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetFiles()
        {
            return Directory.GetFiles("D:\\backup");
            //List<string> vs = new List<string>();
            //vs.Add("a.jpg");
            //vs.Add("b.jpg");
            //vs.Add("c.jpg");
            //vs.Add("d.jpg");
            //return vs;
        }
    }
}
