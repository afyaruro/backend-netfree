
using System.Net;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Exceptions
{
    public class EntityNotUpdateException : CustomException
    {
        public EntityNotUpdateException(string message)
            : base(message, 400)
        {
        }

    }
}