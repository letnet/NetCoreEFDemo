using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MySql.Data.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace NetCoreEFDemo.Infrastructure.Repositories
{
    public static class EntityFrameworkCoreExtensions
    {
        public static DataTable SqlQuery(this DatabaseFacade databaseFacade, string sql, params object[] parameters)
        {
            var command = CreateCommand(databaseFacade, sql, CommandType.Text, parameters);
            var reader = command.ExecuteReader();
            var dt = new DataTable();
            dt.Load(reader);
            reader.Close();
            command.Connection.Close();
            return dt;
        }
        public static List<T> SqlQuery<T>(this DatabaseFacade facade, string sql, params object[] parameters) where T : class, new()
        {
            var dt = SqlQuery(facade, sql, parameters);
            return dt.ToList<T>();
        }

        public static int ExecuteNonQuery(this DatabaseFacade databaseFacade, string sql, CommandType commandType, params object[] parameters)
        {
            var command = CreateCommand(databaseFacade, sql, commandType, parameters);
            var i = command.ExecuteNonQuery();
            command.Connection.Close();
            return i;
        }

        private static IDbCommand CreateCommand(DatabaseFacade databaseFacade, string sql, CommandType commandType, params object[] parameters)
        {
            var conn = databaseFacade.GetDbConnection();
            conn.Open();
            var cmd = conn.CreateCommand();
            if (databaseFacade.IsMySql())
            {
                cmd.CommandText = sql;
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(parameters);
            }
            return cmd;
        }

        private static List<T> ToList<T>(this DataTable dt) where T : class, new()
        {
            var list = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T obj = row.ToEntity<T>();
                list.Add(obj);
            }
            return list;
        }

        /// <summary>
        /// 行转化为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        private static T ToEntity<T>(this DataRow row)
        {
            Type objType = typeof(T);
            T obj = Activator.CreateInstance<T>();
            if (row == null)
                return obj;
            foreach (DataColumn column in row.Table.Columns)
            {
                PropertyInfo property = objType.GetProperty(column.ColumnName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (property == null || !property.CanWrite)
                    continue;
                object value = row[column.ColumnName];
                if (property.PropertyType == typeof(Boolean))
                    value = value.ToString() == "1" || value.ToString().ToLower() == "true" ? true : false;
                //object value = row[column.ColumnName];
                if (value == DBNull.Value) value = null;
                property.SetValue(obj, value, null);
            }
            return obj;
        }
    }
}
