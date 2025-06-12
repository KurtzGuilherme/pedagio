using System.Text.Json.Serialization;
using Thunders.TechTest.ApiService.Domain.Enum;

namespace Thunders.TechTest.ApiService.Application.Messaging.Messages;

public class TollPassageMessage
{
    public DateTime PassageDateTime { get; set; }
    public string Plaza { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public decimal AmountPaid { get; set; }
    public VehicleType VehicleType { get; set; }
}