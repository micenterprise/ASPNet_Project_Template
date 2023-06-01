namespace $safeprojectname$.Interface
{
    public interface IDatabaseStartup
    {
        Task CreateErrorLogsTable();
    }
}
