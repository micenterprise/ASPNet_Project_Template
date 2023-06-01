using $safeprojectname$.Interface;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace $safeprojectname$.Data
{
    public class DatabaseAccess : IDatabaseAccess
    {
        private string connectionString;

        public DatabaseAccess()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var config = builder.Build();
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        public async Task<List<T>> LoadData<T, U>(string sql, U parameters, bool sp = false)
        {
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                var data = await con.QueryAsync<T>(sql, parameters, commandType: sp ? CommandType.StoredProcedure : CommandType.Text);
                return data.ToList();
            }
        }

        public async Task SaveData<T>(string sql, T parameters, bool sp = false)
        {
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                await con.ExecuteAsync(sql, parameters, commandType: sp ? CommandType.StoredProcedure : CommandType.Text);
            }
        }

        public async Task BulkSaveData(DataTable dt, string table_name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlBulkCopy bulk = new SqlBulkCopy(connection))
                {
                    bulk.DestinationTableName = table_name;
                    await bulk.WriteToServerAsync(dt);
                }
                connection.Close();
            }
        }
    }
}
