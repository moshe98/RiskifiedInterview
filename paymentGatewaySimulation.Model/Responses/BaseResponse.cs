using System;
using Newtonsoft.Json;

namespace paymentGatewaySimulation.Model.Responses
{
    public class BaseResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
