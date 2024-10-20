using Microsoft.Extensions.Options;

using Sasayaku.Configuration;
using Sasayaku.Data;

namespace Sasayaku.Services
{
    public interface IUserService
    {
        void Get();
    }

    public class UserService : IUserService
    {
        private readonly ClientCredentials _superUserCredentials;

        public UserService(
            ILogger<UserService> logger,
            IOptions<AuthenticationOptions> authenticationOptions)
        {
            _superUserCredentials = authenticationOptions.Value.Superuser;
        }

        public void Get()
        {

        }
    }
}
