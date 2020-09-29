using System;

namespace CatalogProduct.Api.DTOs
{
    public class UserToken
    {
        public bool Authenticated { get; set; }

        public DateTime Expiration { get; set; }

        public string Token { get; set; }

        public string Message { get; set; }
    }
}