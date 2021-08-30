using System;
using System.Threading.Tasks;
using paymentGatewaySimulation.Model.Requests;
using paymentGatewaySimulation.Model.Responses;
namespace paymentGatewaySimulation.Business.Services
{
    public interface IChargeService
    {
        Task<BaseResponse> ChargeCreditCard(ChargeRequest request);
    }
}
