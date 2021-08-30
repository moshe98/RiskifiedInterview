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
    public class MastercardProvider : ICreditCardCompanyProvider
    {
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _clientFactory;

        public MastercardProvider(IMapper mapper, IHttpClientFactory clientFactory)
        {
            _mapper = mapper;
            _clientFactory = clientFactory;
        }

        public override async Task<BaseResponse> ChargeCard(BaseCreditCardChargeRequest request)
        {
            var apiRequest = CreateApiChargeRequest(request);
            var apiResponse = await SendChargeRequestToCreditCompanyApi(apiRequest);
            return HandleChargeApiResponse((MasterCardChargeResponse)apiResponse);
        }

        public override BaseCreditCardChargeRequest CreateApiChargeRequest(BaseCreditCardChargeRequest request)
        {
            return _mapper.Map<MastercardChargeRequest>(request);
        }

        public override async Task<BaseCreditCardChargeResponse> SendChargeRequestToCreditCompanyApi(BaseCreditCardChargeRequest request)
        {
            try
            {
                var apiRespone = new MasterCardChargeResponse();
                var _httpClient = _clientFactory.CreateClient("HttpClient");
                _httpClient.BaseAddress = new Uri(Consts.APP_SETTINGS_MASTERCARD_CHARGE_URL_ENTRY);
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Add("identifier", "Moshe");
                HttpResponseMessage responseMessage = await _httpClient.PostAsJsonAsync("mastercard/capture_card", request);
                if (responseMessage != null)
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();
                    apiRespone = string.IsNullOrEmpty(responseContent) ?
                            JsonConvert.DeserializeObject<MasterCardChargeResponse>(responseContent) :
                            new MasterCardChargeResponse();

                        //assume we have a logger here and we log the response body
                        //Logger.WriteLog(apiResponseToLog)
                }
                return apiRespone;

            }catch(Exception ex)
            {
                throw ex;
            }            
        }

        public override BaseResponse HandleChargeApiResponse<T>(T apiResponse) where T : class
        {
            var response = new BaseResponse();
            var masterCardResponse = (apiResponse as MasterCardChargeResponse);
            if (!string.IsNullOrWhiteSpace(masterCardResponse.decline_reason))
            {
                response.Error = Consts.CHARGE_CARD_DECLINED;
            }
            return response;
        }
    }
}
