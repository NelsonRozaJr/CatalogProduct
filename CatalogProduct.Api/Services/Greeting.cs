using System;

namespace CatalogProduct.Api.Services
{
    public class Greeting : IGreeting
    {
        public string Hello(string name)
        {
            return $"Hello, {name}! Today is {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}";
        }
    }
}