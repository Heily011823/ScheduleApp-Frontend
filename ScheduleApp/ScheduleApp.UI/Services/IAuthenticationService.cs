namespace ScheduleApp.UI.Services
{
    public interface IAuthenticationService
    {
        bool Login(string username, string password);
    }
}