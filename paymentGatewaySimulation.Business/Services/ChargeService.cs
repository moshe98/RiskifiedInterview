using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using paymentGatewaySimulation.Business.CreditCardProviders;
using paymentGatewaySimulation.Model;
using paymentGatewaySimulation.Model.Requests;
using paymentGatewaySimulation.Model.Responses;

namespace paymentGatewaySimulation.Business.Services
{
    public class ChargeService : IChargeService
    {

        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _clientFactory;

        public ChargeService(IMapper mapper, IHttpClientFactory clientFactory)
        {
            _mapper = mapper;
            _clientFactory = clientFactory;
        }

        public async Task<BaseResponse> ChargeCreditCard(ChargeRequest request)
        {
            if(request == null || !request.IsValid())
            {
                throw new ValidationException("Invalid charge request");
            }

            ICreditCardCompanyProvider provider = null;
            switch (request.CreditCardCompany.Trim().ToLower())
            {
                case Consts.MASTERCARD:
                    provider = new MastercardProvider(_mapper, _clientFactory);
                    break;

                case Consts.VISA:
                    provider = new VisaProvider(_mapper, _clientFactory);
                    break;
                default:
                    throw new Exception();
            }

            if(provider == null)
            {
                throw new Exception();
            }

            var response = await provider.ChargeCard(request);
            return response;
        }


       
    }
}
