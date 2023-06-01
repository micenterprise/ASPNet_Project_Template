using $safeprojectname$.Interface;
using $safeprojectname$.Models;

namespace $safeprojectname$.Data
{
    public class DatabaseStartup : IDatabaseStartup
    {
        private readonly IDatabaseAccess _access;

        public DatabaseStartup(IDatabaseAccess access)
        {
            _access = access;
        }

        public Task CreateErrorLogsTable()
        {
            string sql = "create table error_logs ([id] [nvarchar](100) not null, [error_date] [datetime] not null, [user] nvarchar(100) null, " +
                "[error_message] [nvarchar](max) not null, [stack_trace] [nvarchar](max) null)";
            return _access.SaveData(sql, new { });
        }
    }
}
