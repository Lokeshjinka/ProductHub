using Microsoft.Extensions.Configuration;

namespace Common.AppSetings
{
    public class AppSettings
    {
        private readonly IConfiguration _configuration;
        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string DatabaseConnectionString => _configuration["ConnectionStrings:DbConn"].ToString();
        public string InstanceId => _configuration["AppSettings:InstanceId"].ToString();
    }
}