

namespace Domain.JWT
{
    public class Jwt : IJWT
    {
        public string key { get; set; }

        public string issuer { get; set; }

        public string audience { get; set; }

        public int expiryMinutes { get; set; }

    }
}