using System;
using System.Threading.Tasks;
using paymentGatewaySimulation.Model.Requests.CreditCardApiRequests;
using paymentGatewaySimulation.Model.Responses;
using paymentGatewaySimulation.Model.Responses.CreditCardApiResponses;

namespace paymentGatewaySimulation.Business.Services
{
    public abstract class ICreditCardCompanyProvider
    {
        public abstract Task<BaseResponse> ChargeCard(BaseCreditCardChargeRequest request);
        public abstract BaseCreditCardChargeRequest CreateApiChargeRequest(BaseCreditCardChargeRequest request);
        public abstract Task<BaseCreditCardChargeResponse> SendChargeRequestToCreditCompanyApi(BaseCreditCardChargeRequest request);
        public abstract BaseResponse HandleChargeApiResponse<T>(T apiResponse) where T : BaseCreditCardChargeResponse;


    }
}
