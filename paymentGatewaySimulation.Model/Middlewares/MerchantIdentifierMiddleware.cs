using System;
namespace paymentGatewaySimulation.Model.Middlewares
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    namespace MyContacts.API.Middleware
    {
        public class MerchantIdentifierMiddleware
        {
            private readonly RequestDelegate _next;
            public MerchantIdentifierMiddleware(RequestDelegate next)
            {
                _next = next;
            }
            public async Task Invoke(HttpContext context)
            {
                if (!context.Request.Headers.Keys.Contains(Consts.MERCHANT_IDENTIFIER_HEADER_KEY))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(context.Request.Headers[Consts.MERCHANT_IDENTIFIER_HEADER_KEY]))
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest; 
                        return;
                    }
                }
                await _next.Invoke(context);
            }
        }
    }
}
