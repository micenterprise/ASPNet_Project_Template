using $safeprojectname$.Interface;
using $safeprojectname$.Models;

namespace $safeprojectname$.Data
{
    public class ApplicationHelper : IApplicationHelper
    {
        private readonly IDatabaseAccess _access;

        public ApplicationHelper(IDatabaseAccess access)
        {
            _access = access;
        }

        public ErrorLogsModel CreateErrorLogsModel(Exception e, string user)
        {
            ErrorLogsModel errorLogsModel = new ErrorLogsModel
            {
                Id = Guid.NewGuid().ToString(),
                Error_Date = DateTime.UtcNow,
                User = user,
                Error_Message = e.Message,
                Stack_Trace = e.StackTrace,
            };
            return errorLogsModel;
        }

        public Task InsertErrorLogs(ErrorLogsModel errorLogsModel)
        {
            string sql = "insert into error_logs values (@Id, @Error_Date, @User_Id, @Error_Message, @Stack_Trace)";
            return _access.SaveData(sql, errorLogsModel);
        }
    }
}
