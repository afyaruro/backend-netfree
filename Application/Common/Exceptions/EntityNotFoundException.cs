using System.Net;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Exceptions
{
    public class EntityNotFoundException : CustomException
    {

        public EntityNotFoundException(string message)
            : base(message, 404)
        {
        }

    }
}