namespace $safeprojectname$.Models
{
    public class ErrorLogsModel
    {
        public string Id { get; set; }
        public DateTime Error_Date { get; set; }
        public string User { get; set; }
        public string Error_Message { get; set; }
        public string Stack_Trace { get; set; }
    }
}
