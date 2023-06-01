using $safeprojectname$.Models;

namespace $safeprojectname$.Interface
{
    public interface IApplicationHelper
    {
        ErrorLogsModel CreateErrorLogsModel(Exception e, string user);
        Task InsertErrorLogs(ErrorLogsModel errorLogsModel);
    }
}
