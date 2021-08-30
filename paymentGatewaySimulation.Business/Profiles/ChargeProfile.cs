using System;
using System.Text.RegularExpressions;
using AutoMapper;
using paymentGatewaySimulation.Model.Requests;
using paymentGatewaySimulation.Model.Requests.CreditCardApiRequests;

namespace paymentGatewaySimulation.Business.Profiles
{
    public class ChargeProfile: Profile
    {
        public ChargeProfile()
        {
            CreateMap<ChargeRequest, MastercardChargeRequest>()
                .ForMember(d => d.card_number, o => o.MapFrom(s => s.CreditCardNumber))
                .ForMember(d => d.charge_amount, o => o.MapFrom(s => s.Amount))
                .ForMember(d => d.cvv, o => o.MapFrom(s => s.Cvv))
                .ForMember(d => d.expiration, o => o.MapFrom(s => s.ExpirationDate.Replace("/", "-")))
                // we are assuming here that the full name is divided by space and ther is only on e like this -
                // otherwise we need converter here to deal with other corner case
                .ForMember(d => d.first_name, o => o.MapFrom(s => Regex.Split(s.FullName, @"\s+")[0]))
                .ForMember(d => d.last_name, o => o.MapFrom(s => Regex.Split(s.FullName, @"\s+")[1]));

            CreateMap<ChargeRequest, VisaChargeRequest>()
                .ForMember(d => d.number, o => o.MapFrom(s => s.CreditCardNumber))
                .ForMember(d => d.fullName, o => o.MapFrom(s => s.FullName))
                .ForMember(d => d.cvv, o => o.MapFrom(s => s.Cvv))
                .ForMember(d => d.expiration, o => o.MapFrom(s => s.ExpirationDate))
                .ForMember(d => d.totalAmount, o => o.MapFrom(s => s.Amount));
        }
    }
}
