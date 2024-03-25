using System;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace UsersMinimalAPI.Authentication
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int KEY_SIZE = 64;
        private const int ITERATIONS = 350000;
        public string HashPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(KEY_SIZE);
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(password,salt, KeyDerivationPrf.HMACSHA512,ITERATIONS,KEY_SIZE));
            return $"{Convert.ToBase64String(salt)}:{hash}";
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            var saltHash = hashedPassword.Split(":");
            var salt = Convert.FromBase64String(saltHash[0]);
            var usersHash = saltHash[1];

            string computedHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: ITERATIONS,
            numBytesRequested: KEY_SIZE));

            return usersHash.Equals(computedHash);
        }
    }
}
