using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace Db
{
    public abstract class Dao
    {
        static DbProviderFactory providerFactory;
        static string connStr;
        static bool initialized;

        public static void Init(string providerName, string connString)
        {
            providerFactory = DbProviderFactories.GetFactory(providerName);
            connStr = connString;
            initialized = true;
        }

        public static void Init()
        {
            Init(ConfigurationManager.AppSettings["dbProvider"], ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString);
        }

        protected DbConnection CreateConnection()
        {
            if (!initialized)
            {
                Init();
            }

            DbConnection conn = providerFactory.CreateConnection();
            conn.ConnectionString = connStr;
            conn.Open();
            return conn;
        }

        protected DbCommand CreateCommand(DbConnection conn, string text)
        {
            DbCommand cmd = providerFactory.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = text;
            return cmd;
        }

        protected DbParameter CreateParameter(DbCommand cmd, string name, object value, DbType? type)
        {
            DbParameter param = providerFactory.CreateParameter();
            param.ParameterName = name;
            param.Value = value;

            if (type.HasValue)
            {
                param.DbType = type.Value;
            }

            cmd.Parameters.Add(param);
            return param;
        }
    }
}
