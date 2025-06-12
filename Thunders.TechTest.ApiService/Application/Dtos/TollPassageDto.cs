using Thunders.TechTest.ApiService.Domain.Enum;

namespace Thunders.TechTest.ApiService.Application.Dtos;

public class TollPassageDto
{
    public DateTime PassageDateTime { get; }
    public string Plaza { get;  }
    public string City { get; }
    public string State { get; }
    public decimal AmountPaid { get; }
    public VehicleType VehicleType { get; }

    public TollPassageDto(
        DateTime passageDateTime,
        string plaza,
        string city,
        string state,
        decimal amountPaid,
        VehicleType vehicleType)
    {
        if (string.IsNullOrWhiteSpace(plaza))
            throw new ArgumentNullException(nameof(plaza));

        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentNullException(nameof(city));

        if (string.IsNullOrWhiteSpace(state))
            throw new ArgumentNullException(nameof(amountPaid));

        PassageDateTime = passageDateTime;
        Plaza = plaza;
        City = city;
        State = state;
        AmountPaid = amountPaid;
        VehicleType = vehicleType;

    }

    public TollPassageDto(
        string plaza,
        string city,
        string state,
        decimal amountPaid,
        VehicleType vehicleType)
        :this(DateTime.Now, plaza, city, state, amountPaid, vehicleType)    
    {
            
    }
}
