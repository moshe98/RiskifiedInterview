using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using paymentGatewaySimulation.Business.Services;
using paymentGatewaySimulation.Model;
using paymentGatewaySimulation.Model.Requests.CreditCardApiRequests;
using paymentGatewaySimulation.Model.Responses;
using paymentGatewaySimulation.Model.Responses.CreditCardApiResponses;

namespace paymentGatewaySimulation.Business.CreditCardProviders
{
    public class VisaProvider : ICreditCardCompanyProvider
    {
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _clientFactory;

        private const string SUCCESS = "success";

        public VisaProvider(IMapper mapper, IHttpClientFactory clientFactory)
        {
            _mapper = mapper;
            _clientFactory = clientFactory;
        }

        public override async Task<BaseResponse> ChargeCard(BaseCreditCardChargeRequest request)
        {
            var apiRequest = CreateApiChargeRequest(request);
            var apiResponse = await SendChargeRequestToCreditCompanyApi(apiRequest);
            return HandleChargeApiResponse((VisaChargeResponse)apiResponse);
        }

        public override BaseCreditCardChargeRequest CreateApiChargeRequest(BaseCreditCardChargeRequest request)
        {
            return _mapper.Map<VisaChargeRequest>(request);
        }

        public override async Task<BaseCreditCardChargeResponse> SendChargeRequestToCreditCompanyApi(BaseCreditCardChargeRequest request)
        {
            try
            {
                var apiResponse = new VisaChargeResponse();
                var _httpClient = _clientFactory.CreateClient("HttpClient");
                _httpClient.BaseAddress = new Uri(Consts.APP_SETTINGS_VISA_CHARGE_URL_ENTRY);
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Add("identifier", "Moshe");
                HttpResponseMessage responseMessage = await _httpClient.PostAsJsonAsync("visa/api/chargeCard", request);
                if (responseMessage != null)
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();
                    apiResponse = !string.IsNullOrEmpty(responseContent) ?
                            JsonConvert.DeserializeObject<VisaChargeResponse>(responseContent) :
                            new VisaChargeResponse();
                }
                return apiResponse;

            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public override BaseResponse HandleChargeApiResponse<T> (T apiResponse) where T: class
        {
            var response = new BaseResponse();
            var visaChargeResponse = (apiResponse as VisaChargeResponse);
            // if no result 
            if (string.IsNullOrWhiteSpace(visaChargeResponse.chargeResult))
            {
                throw new Exception("No cahrge result specified");
            }
            else if(!visaChargeResponse.chargeResult.Trim().ToLower().Equals(SUCCESS))
            {
                response.Error = Consts.CHARGE_CARD_DECLINED;
            }
            return response;
        }
    }
}
