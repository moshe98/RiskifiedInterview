using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using paymentGatewaySimulation.Model.Requests;
using paymentGatewaySimulation.Model.Responses;
using paymentGatewaySimulation.Business.Services;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace paymentGatewaySimulation.Controllers
{
    public class ChargeController : Controller
    {
        private IChargeService _chargeService { get; set; }
        public ChargeController(IChargeService chargeService)
        {
            _chargeService = chargeService;
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult<BaseResponse>> Charge([FromBody] ChargeRequest request)
        {
            var response = new BaseResponse();
            try
            {
                response = await _chargeService.ChargeCreditCard(request);
                // no error - return status 200 with no body
                if(string.IsNullOrWhiteSpace(response.Error))
                {
                    return Ok();
                }
            }
            catch(ValidationException ex)
            {
                // validation error occured or the request in bad format - return status 400 
                return BadRequest();
            }
            catch (Exception ex)
            {
                //any other occured - return server status error 500
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            // return the response with status 200 and the error in body
            return Ok(response);
        }
    }
}
