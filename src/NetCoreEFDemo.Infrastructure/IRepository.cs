using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreEFDemo.Infrastructure
{
    /// <summary>
    /// Repository接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class, IEntity
    {

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IQueryable<T> AsQueryable();

        /// <summary>
        /// 删除行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task Delete(params T[] entities);

        /// <summary>
        /// 添加行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task Insert(params T[] entities);

        /// <summary>
        /// 更新行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Update(params T[] entities);
    }
}

