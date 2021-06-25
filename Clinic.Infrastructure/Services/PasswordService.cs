using Clinic.Core.Interfaces.InfrastructureServices;
using Clinic.Core.Options;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Clinic.Infrastructure.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordOptions _passwordOptions;
        public PasswordService(IOptions<PasswordOptions> passwordOptions)
        {
            _passwordOptions = passwordOptions.Value;
        }

        public bool Check(string hashedPassword, string plainPassword)
        {
            var parts = hashedPassword.Split('.');

            if (parts.Length != 3)
            {
                throw new FormatException("Unexpected hash format.");
            }

            int iterations = Convert.ToInt32(parts[0]);
            byte[] salt = Convert.FromBase64String(parts[1]);
            byte[] key = Convert.FromBase64String(parts[2]);

            using (var algorithm = new Rfc2898DeriveBytes(plainPassword, salt, iterations,HashAlgorithmName.SHA512))
            {
                byte[] keyToCheck = algorithm.GetBytes(_passwordOptions.KeySize);

                return keyToCheck.SequenceEqual(key);
            }
        }

        public string Hash(string plainPassword)
        {
            using (var algorithm = new Rfc2898DeriveBytes(plainPassword, _passwordOptions.SaltSize, _passwordOptions.Iterations, HashAlgorithmName.SHA512))
            {
                string key = Convert.ToBase64String(algorithm.GetBytes(_passwordOptions.KeySize));
                string salt = Convert.ToBase64String(algorithm.Salt);

                return $"{_passwordOptions.Iterations}.{salt}.{key}";
            }
        }
    }
}
