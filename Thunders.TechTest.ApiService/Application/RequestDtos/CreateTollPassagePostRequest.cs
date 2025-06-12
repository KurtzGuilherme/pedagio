using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;
using Thunders.TechTest.ApiService.Domain.Enum;

namespace Thunders.TechTest.ApiService.Application.RequestDtos;

public class CreateTollPassagePostRequest
{
    public string Plaza { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public decimal AmountPaid { get; set; }
    public VehicleType VehicleType { get; set; }
}
