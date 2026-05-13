using ScheduleApp.UI.Services;

namespace ScheduleApp.UI.Services
{
    // IMPORTANTE: Asegúrate de tener el ": IAuthenticationService"
    public class FakeAuthenticationService : IAuthenticationService
    {
        private const string ValidUser = "admin";
        private const string ValidPassword = "1234";

        public bool Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return false;

            return username == ValidUser && password == ValidPassword;
        }
    }
}