using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreEFDemo.Infrastructure
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        bool IsTransaction { get; set; }

        /// <summary>
        /// 数据库上下文
        /// </summary>
        public DbContextBase DbContext { get; }

        /// <summary>
        /// 开启事务
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// 完成事务
        /// </summary>
        Task<int> Commit();

        /// <summary>
        /// 回滚事务
        /// </summary>
        ValueTask Rollback();
    }
}

