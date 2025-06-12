using System;
using Thunders.TechTest.ApiService.Domain.Entities;
using Thunders.TechTest.ApiService.Domain.Enum;
using Thunders.TechTest.ApiService.Domain.Exceptions;

namespace Thunders.TechTest.Tests.Domain.Entities;
public class TollPassageTest
{
    [Fact]
    public void Create_Construtor_CreateInstanceCorrect()
    {
        //Arrange
        var passageDateTime = DateTime.Now;
        var vehicleType =VehicleType.Motorcycle;
        var plaza = "fakePlaza";
        var city = "fakeCity";
        var state = "fakeState";
        var amountPaid = 12.5m;

        //Act
        var fakeTollPassage = new TollPassage(passageDateTime, plaza, city, state, amountPaid, vehicleType);

        //Assert
        Assert.Multiple(() =>
        {
            Assert.NotNull(fakeTollPassage);
            Assert.Equal(passageDateTime, fakeTollPassage.PassageDateTime);
            Assert.Equal(plaza, fakeTollPassage.Plaza);
            Assert.Equal(amountPaid, fakeTollPassage.AmountPaid);
            Assert.Equal(vehicleType, fakeTollPassage.VehicleType);
        });
    }

    [Fact]
    public void Construtor_IdGuidEmpty_ThrowsArgumentNullOrEmptyException()
    {
        //Arrange
        var passageDateTime = DateTime.Now;
        var vehicleType = VehicleType.Motorcycle;
        var plaza = "fakePlaza";
        var city = "fakeCity";
        var state = "fakeState";
        var amountPaid = 12.5m;

        //Act
        var result = Assert.Throws<ArgumentNullOrEmptyException>(() =>
            new TollPassage(Guid.Empty, passageDateTime, plaza, city, state, amountPaid, vehicleType));

        //Assert
        Assert.IsType<ArgumentNullOrEmptyException>(result);
    }
}
