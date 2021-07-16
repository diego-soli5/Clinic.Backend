using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Clinic.Infrastructure.Data
{
    public abstract class ConnectionSql
    {
        private readonly IConfiguration _configuration;

        public ConnectionSql(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DevelopmentLocalDb"));
        }
    }
}
