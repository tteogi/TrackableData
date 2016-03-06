﻿using System;
using System.Configuration;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace TrackableData.MySql.Tests
{
    public class Database : IDisposable
    {
        public Database()
        {
            var cstr = ConfigurationManager.ConnectionStrings["TestDb"].ConnectionString;

            // create TestDb if not exist

            var cstrForMaster = "";
            {
                var connectionBuilder = new SqlConnectionStringBuilder(cstr);
                connectionBuilder.InitialCatalog = "";
                cstrForMaster = connectionBuilder.ToString();
            }

            using (var conn = new MySqlConnection(cstrForMaster))
            {
                conn.Open();

                using (var cmd = new MySqlCommand())
                {
                    cmd.CommandText = string.Format(@"
                        DROP DATABASE IF EXISTS {0};
                        CREATE DATABASE {0};
                    ", new SqlConnectionStringBuilder(cstr).InitialCatalog);
                    cmd.Connection = conn;

                    var result = cmd.ExecuteScalar();
                }
            }
        }

        public MySqlConnection Connection
        {
            get
            {
                var cstr = ConfigurationManager.ConnectionStrings["TestDb"].ConnectionString;
                var connection = new MySqlConnection(cstr);
                connection.Open();
                return connection;
            }
        }

        public void Dispose()
        {
        }
    }
}
