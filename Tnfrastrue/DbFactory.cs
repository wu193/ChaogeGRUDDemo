using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tnfrastrue
{
  public  class DbFactory
    {


        /// <summary>
        /// SqlSugarClient属性
        /// </summary>
        /// <returns></returns>
        public static SqlSugarClient GetSqlSugarClient()
        {
            var db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = Config.ConnectionString, //必填
                DbType = DbType.MySql, //必填
                IsAutoCloseConnection = true, //默认false
                InitKeyType = InitKeyType.Attribute,
                MoreSettings = new ConnMoreSettings
                {
                    IsWithNoLockQuery = true
                }
            }); //默认SystemTable

            return db;
        }
    }
}
