using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.JWT
{
    public interface IJWT
    {
        string key { get; }
        string issuer { get; }
        string audience { get; }
        int expiryMinutes { get; }

    }
}