using Dal.Culture;
using Model.Culture.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Tnfrastrue.Entities;
using Tools;
using WebApplicationSqlSugar.ViewModel;

namespace WebApplicationSqlSugar.Controllers.Culture
{
    public class UserController : ApiController
    {
        public static string root = AppDomain.CurrentDomain.BaseDirectory + "\\img";

        UserBusiness dal = new UserBusiness();


        /// <summary>
        /// 分页获取分类列表
        /// </summary>
        /// <param name="input">当前页数，条数</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Getuse([FromUri] UserDto input)
        {
            var list = dal.Getuse(input);

            return Ok(new ResultViewModel
            {
                Code = (int)HttpStatusCode.OK,
                Data = list
            });
        }
        /// <summary>
        /// 分页获取分类列表
        /// </summary>
        /// <param name="input">当前页数，条数</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetRoleDtoList([FromUri] UserDto input)
        {
            var list = dal.GetRole(input);

            return Ok(new ResultViewModel
            {
                Code = (int)HttpStatusCode.OK,
                Data = list
            });
        }
        /// <summary>
        /// 添加广告表
        /// </summary>
        /// <param name="input">角色表集合</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddUse([FromBody] UserDto input)
        {
            dal.AddUse(input);
            return Ok(new ResultViewModel
            {
                Code = (int)HttpStatusCode.OK,
                Msg = "操作成功"
            });
        }

        /// <summary>
        /// 修改人员
        /// </summary>
        /// <param name="input">角色表集合</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Updateuser([FromBody] UserDto input)
        {
            dal.Updateuser(input);
            return Ok(new ResultViewModel
            {
                Code = (int)HttpStatusCode.OK,
                Msg = "操作成功"
            });
        }
        /// 修改人员
        /// </summary>
        /// <param name="input">删除人员</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Delteuser([FromBody] UserDto input)
        {
            dal.Delteuser(input);
            return Ok(new ResultViewModel
            {
                Code = (int)HttpStatusCode.OK,
                Msg = "操作成功"
            });
        }

       //  上传文件
        public async Task<IHttpActionResult> Uploads([FromUri] UserDto input)
        {

          
            List<string> list = new List<string>();

            try
            {
                //web api 获取项目根目录下指定的文件下
                //var root = System.Web.Hosting.HostingEnvironment.MapPath("\\logs");

                if (!Directory.Exists(root))//如果日志目录不存在就创建
                {
                    Directory.CreateDirectory(root);
                }
                //var root = System.Web.Hosting.HostingEnvironment.MapPath("/Resource/Images");
                var provider = new MultipartFormDataStreamProvider(root);

                ////文件已经上传  但是文件没有后缀名  需要给文件添加后缀名
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (var file in provider.FileData)
                {
                    //这里获取含有双引号'" '
                    string filename = file.Headers.ContentDisposition.FileName.Trim('"');
                    //获取对应文件后缀名
                    string fileExt = filename.Substring(filename.LastIndexOf('.'));
                    if (file == null)
                    {
                        throw new Exception("无上传文件");
                    }

                    //string io = file.Headers.ContentLength.ToString();
                    //if (file.Headers.ContentLength>1024*1024* Convert.ToInt32(uploadFileSize))

                    FileInfo fileinfo = new FileInfo(file.LocalFileName);
                    //fileinfo.Name 上传后的文件路径 此处不含后缀名 
                    string newname = DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileinfo.Name;
                    //修改文件名 添加后缀名
                    //string newFilename = fileinfo.Name + fileExt;
                    string newFilename = newname + fileExt;
                    //最后保存文件路径
                    string saveUrl = Path.Combine(root, newFilename);
                  
                    fileinfo.MoveTo(saveUrl);
                    list.Add(newFilename);
                }

                return Ok(new ResultViewModel
                {
                    Code = (int)HttpStatusCode.OK,
                    Msg = "操作成功"
                });

            }
            catch (Exception ex)
            {
              LogHelper.WriteException(ex.Message, ex);
                throw ex;
            }
        }// <summary>
        /// 模板下载           下载参数   路径名称   文件名称
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public HttpResponseMessage GetDownload([FromUri]string fileName = "探头修改.txt")
        {
            try
            {
                return Download(fileName);
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex.Message, ex);
                throw;
            }


        }
        private const string MimeType = "application/octet-stream";


        //相对路径
        //待下载文件存放目录
        //private readonly string DirFilePath = StringHelper.address;
         private readonly string DirFilePath = "D:\\小黑子";
        /// <summary>
        /// 缓冲区大小
        /// </summary>
        private const int BufferSize = 80 * 1024;

        public HttpResponseMessage Download(string fileName)
        {
            var fullFilePath = Path.Combine(DirFilePath, fileName);

            if (!File.Exists(fullFilePath))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            FileStream fileStream = File.Open(fullFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            var response = new HttpResponseMessage();

            response.Content = new StreamContent(fileStream, BufferSize);

            response.Content.Headers.ContentDisposition
                = new ContentDispositionHeaderValue("attachment") { FileName = fileName };

            response.Content.Headers.ContentType
                = new MediaTypeHeaderValue(MimeType);

            response.Content.Headers.ContentLength
                = fileStream.Length;

            return response;
        }


        /// <summary>
        /// 导入信息exel表      //参数不用填写
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult ProcessRequest( )
        {
            try
            {
                StringBuilder errorMsg = new StringBuilder(); // 错误信息
                var file = HttpContext.Current.Request.Files[0];
                if (file == null)
                {
                    throw new Exception("无上传文件");
                }

                string fileExt = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                if (fileExt != ".xls" && fileExt != ".xlsx")
                {
                    throw new Exception("格式不正确必须是xls或者xlsx");
                }
                //HttpPostedFile filePost = context.Request.Files["filed"]; // 获取上传的文件
                ////string filePath = ExcelHelper.SaveExcelFile(filePost); // 保存文件并获取文件路径
                string filePath = ExcelHelper.SaveExcelFile(file);
                // 单元格抬头
                // key：实体对象属性名称，可通过反射获取值
                // value：属性对应的中文注解
                Dictionary<string, string> cellheader = new Dictionary<string, string> {
                 { "name", "姓名" },
                 { "pass", "密码" },
                 { "createtime", "创建时间" },
                };
                // 1.2解析文件，存放到一个List集合里
                List<User> enlist = ExcelHelper.ExcelToEntityList<User>(cellheader, filePath, out errorMsg);
                // 上面已经有数据了,把数据添加到数据库就行了
                int  i= 1;

            }
            catch (Exception)
            {

                throw;
            }

            return Ok(new ResultViewModel
            {
                Code = (int)HttpStatusCode.OK,
                Msg = "操作成功"
            });


        }

        [HttpGet]
        /// <summary>
        /// 导 出信息exel表      
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Export()
        {
            try
            {
                List<Userexe> user = new List<Userexe>();
                Userexe m = new Userexe();
                m.name = "iop";
                m.pass = "123456";
                m.img = "c\\img";
                m.createtime = DateTime.Now.ToString();

                Userexe m1 = new Userexe();
                m1.name = "iop1";
                m1.pass = "1234561";
                m1.img = "c\\img11";
                m1.createtime = DateTime.Now.ToString();
                user.Add(m);
                user.Add(m1);
                Dictionary<string, string> cellheader = new Dictionary<string, string> {
                 { "name", "姓名" },
                 { "pass", "密码" },
                 { "createtime", "创建时间" },
                 { "img", "图片" },
                };
                string sheetName = "小黑子.xls";
                DataTable  list=  ListconversionTable.ConvertToDataTable(user);


              ExcelHelper.TableToExcel(list, sheetName);
                // 上面已经有数据了,把数据添加到数据库就行了
                int i = 1;

            }
            catch (Exception)
            {

                throw;
            }

            return Ok(new ResultViewModel
            {
                Code = (int)HttpStatusCode.OK,
                Msg = "操作成功"
            });


        }


    }
}
