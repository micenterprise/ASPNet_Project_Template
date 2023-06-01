using System.Data;

namespace $safeprojectname$.Interface
{
    public interface IDatabaseAccess
    {
        Task<List<T>> LoadData<T, U>(string sql, U parameters, bool sp = false);
        Task SaveData<T>(string sql, T parameters, bool sp = false);
        Task BulkSaveData(DataTable dt, string table_name);
    }
}
