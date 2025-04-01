using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Application.Service.User.Commands
{

    public class PasswordEncryptionHelper
    {
        private readonly PasswordHasher<string> _passwordHasher;

        public PasswordEncryptionHelper()
        {
            _passwordHasher = new PasswordHasher<string>();
        }

        public string HashPassword(string password, string mail)
        {
            return _passwordHasher.HashPassword(mail, password);
        }
        public bool VerifyPassword(string hashedPassword, string providedPassword, string mail)
        {
            var result = _passwordHasher.VerifyHashedPassword(mail, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }

}