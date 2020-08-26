using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NetCoreEFDemo.Infrastructure
{
    public class DbContextBase : DbContext
    {
        public DbContextBase(DbContextOptions<DbContextBase> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var allEntityType = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Where(p => !Path.GetFileName(p).StartsWith("System.")
                            && !Path.GetFileName(p).StartsWith("Microsoft."))
                .Select(Assembly.LoadFrom)
                .SelectMany(y => y.DefinedTypes)
                .Where(x => typeof(IEntity).IsAssignableFrom(x) && x.IsClass)?
                .ToArray();

            foreach (var type in allEntityType)
            {
                modelBuilder.Model.AddEntityType(type);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
