using Model.Culture.Dto;
using Model.Culture.ViewModel;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tnfrastrue;
using Tnfrastrue.Entities;
using Tnfrastrue.ViewModel;

namespace Dal.Culture
{
    public class UserBusiness
    {

        /// <summary>
        /// 人员表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PageModel<UserViewModel> Getuse(UserDto input)
        {
            using (var db = DbFactory.GetSqlSugarClient())
            {
                string user = $@"  SELECT  * from  user ";


                var list = new List<User>();
                //var entity = db.Queryable<Cul_Category>().With(SqlWith.NoLock).ToList();
                var total = 0;
                if (input.PageIndex > 0 && input.PageSize > 0)
                {
                    list = db.SqlQueryable<User>(user).OrderBy(_ => _.createtime, OrderByType.Desc).ToPageList(input.PageIndex, input.PageSize, ref total);

                }
                else
                {
                    list = db.SqlQueryable<User>(user).OrderBy(_ => _.createtime, OrderByType.Desc).ToList();

                    total = list.Count();
                }
                List<UserViewModel> lists = list.Select(x => new UserViewModel()
                {
                    id = x.id,
                    name = x.name,
                    pass = x.pass,
                    createtime = x.createtime

                }).ToList();


             

                return new PageModel<UserViewModel>
                {
                    Total = total,
                    Data = lists
                };
            }
        }

        /// <summary>
        /// 人员表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PageModel<UserViewModel> GetRole(UserDto input)
        {
            using (var db = DbFactory.GetSqlSugarClient())
            {
                var list = new List<User>();
                //var entity = db.Queryable<Cul_Category>().With(SqlWith.NoLock).ToList();
                var total = 0;
                if (input.PageIndex > 0 && input.PageSize > 0)
                {
                    list = db.Queryable<User>().OrderBy(_ => _.createtime, OrderByType.Desc).ToPageList(input.PageIndex, input.PageSize, ref total);

                }
                else
                {
                    list = db.Queryable<User>().OrderBy(_ => _.createtime, OrderByType.Desc).ToList();

                    total = list.Count();
                }
                List<UserViewModel> lists = list.Select(x => new UserViewModel()
                {
                    id = x.id,
                    name = x.name,
                    pass = x.pass,
                    createtime = x.createtime

                }).ToList();
                return new PageModel<UserViewModel>
                {
                    Total = total,
                    Data = lists
                };
            }
        }


        /// <summary>
        /// 添加人员
        /// </summary>
        /// <param name="input"></param>
        public void AddUse(UserDto input)
        {
            using (var db = DbFactory.GetSqlSugarClient())
            {
                User user = new User();
                user.name = input.name;
                user.pass = input.pass;
                user.createtime = DateTime.Now;
                var returnId = db.Insertable(user).ExecuteCommand();
            }
        }


        /// <summary>
        /// 修改人员
        /// </summary>
        /// <param name="input"></param>
        public void Updateuser(UserDto input)
        {
            using (var db = DbFactory.GetSqlSugarClient())
            {
                db.Updateable<User>().SetColumns(it => new User()
                {
                    name = input.name,
                    pass = input.pass,
                    createtime = DateTime.Now,
                }).Where(it => it.id == input.id).ExecuteCommand();
            }
        }


        /// <summary>
        /// 删除人员
        /// </summary>
        /// <param name="input"></param>
        public void Delteuser(UserDto input)
        {
            using (var db = DbFactory.GetSqlSugarClient())
            {
                db.Deleteable<User>().Where(new User() { id = input.id }).ExecuteCommand();
            }
        }







    }
}
