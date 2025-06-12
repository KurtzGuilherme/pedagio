using System.Text.Json.Serialization;
using Thunders.TechTest.ApiService.Domain.Enum;
using Thunders.TechTest.ApiService.Domain.Exceptions;

namespace Thunders.TechTest.ApiService.Domain.Entities;

public sealed class TollPassage
{
    public Guid Id { get; private set; }
    public DateTime PassageDateTime { get; private set; }
    public string Plaza { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public decimal AmountPaid { get; private set; }
    public VehicleType VehicleType { get; private set; }

    public TollPassage(
        Guid id,
        DateTime passageDateTime,
        string plaza,
        string city,
        string state,
        decimal amountPaid,
        VehicleType vehicleType)
    {
        if (id == Guid.Empty)
            throw new ArgumentNullOrEmptyException(nameof(id));

        Id = id;
        PassageDateTime = passageDateTime;
        Plaza = plaza ;
        City = city ;
        State = state ;
        AmountPaid = amountPaid;
        VehicleType = vehicleType;
    }

    public TollPassage(
        DateTime passageDateTime,
        string plaza,
        string city,
        string state,
        decimal amountPaid,
        VehicleType vehicleType)
        :this(Guid.NewGuid(), passageDateTime, plaza, city, state, amountPaid, vehicleType)
    {
            
    }
}
