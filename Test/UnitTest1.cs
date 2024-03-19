using Program;

namespace Test;

public class Test
{

    [Fact]
    public void AddVehicleToInventoryTest()
    {
        AuctionManager AuctionManager = new AuctionManager();
        var vehicle = new Sedan(1, "Toyota", "Camry", 2020, 15000, 4);

        var added = AuctionManager.AddVehicleToInventory(vehicle);

        Assert.True(added);
    }

    [Fact]
    public void AddSameVehicleToInventoryTest()
    {
        AuctionManager AuctionManager = new AuctionManager();
        var vehicle = new Sedan(1, "Toyota", "Camry", 2020, 15000, 4);

        var added = AuctionManager.AddVehicleToInventory(vehicle);
        var addedAgain = AuctionManager.AddVehicleToInventory(vehicle);

        Assert.True(added);
        Assert.False(addedAgain);
    }

    [Fact]
    public void AddVehicleToAuctionTest()
    {
        AuctionManager AuctionManager = new AuctionManager();
        var vehicle = new Sedan(1, "Toyota", "Camry", 2020, 15000, 4);

        var added = AuctionManager.AddVehicleToInventory(vehicle);
        var auction = AuctionManager.CreateAuction(1);
        var addedToAuction = AuctionManager.AddVehicleToAuction(1, 1);

        Assert.True(added);
        Assert.True(auction);
        Assert.True(addedToAuction);
    }

    [Fact]
    public void AddSameVehicleToAnotherAuctionTest()
    {
        AuctionManager AuctionManager = new AuctionManager();
        var vehicle = new Sedan(1, "Toyota", "Camry", 2020, 15000, 4);

        var added = AuctionManager.AddVehicleToInventory(vehicle);
        var auction = AuctionManager.CreateAuction(1);
        var auction2 = AuctionManager.CreateAuction(2);
        var addedToAuction = AuctionManager.AddVehicleToAuction(1, 1);
        var addedToSameAuction = AuctionManager.AddVehicleToAuction(1, 1);
        var addedToAuction2 = AuctionManager.AddVehicleToAuction(2, 1);

        Assert.True(added);
        Assert.True(auction);
        Assert.True(auction2);
        Assert.True(addedToAuction);
        Assert.False(addedToSameAuction);
        Assert.False(addedToAuction2);
    }

    [Fact]
    public void CreateAuctionTest()
    {
        AuctionManager AuctionManager = new AuctionManager();
        var system = new AuctionManager();

        var action = system.CreateAuction(1);

        Assert.True(action);
    }

    [Fact]
    public void CreateSameAuctionTest()
    {
        AuctionManager AuctionManager = new AuctionManager();
        var system = new AuctionManager();

        var auction = system.CreateAuction(1);
        var auction1 = system.CreateAuction(1);

        Assert.True(auction);
        Assert.False(auction1);
    }

    [Fact]
    public void StartAuctionTest()
    {
        AuctionManager AuctionManager = new AuctionManager();
        var system = new AuctionManager();

        var auction = system.CreateAuction(1);
        var start = system.StartAuction(1);

        Assert.True(auction);
        Assert.True(start);
    }

    [Fact]
    public void StartAuctionAlreadyStartedTest()
    {
        AuctionManager AuctionManager = new AuctionManager();
        var system = new AuctionManager();

        var auction = system.CreateAuction(1);
        var start = system.StartAuction(1);
        var startAgain = system.StartAuction(1);

        Assert.True(auction);
        Assert.True(start);
        Assert.False(startAgain);
    }


    [Fact]
    public void CloseAuctionTest()
    {
        AuctionManager AuctionManager = new AuctionManager();
        var system = new AuctionManager();

        var auction = system.CreateAuction(1);
        var start = system.StartAuction(1);
        var close = system.CloseAuction(1);

        Assert.True(auction);
        Assert.True(start);
        Assert.True(close);
    }

    [Fact]
    public void CloseNonExistedAuctionTest()
    {
        AuctionManager AuctionManager = new AuctionManager();
        var system = new AuctionManager();

        var auction = system.CreateAuction(1);
        var close = system.CloseAuction(2);

        Assert.True(auction);
        Assert.False(close);
    }

    [Fact]
    public void SearchVehicles()
    {
        AuctionManager AuctionManager = new AuctionManager();
        var sedan1 = new Sedan(2, "Honda", "Accord", 2018, 18000, 4);
        var suv1 = new SUV(3, "Toyota", "RAV4", 2021, 25000, 4);
        var truck1 = new Truck(4, "Ford", "F-150", 2019, 30000, 2000);
        var sedan2 = new Sedan(5, "Nissan", "Altima", 2020, 20000, 4);
        var suv2 = new SUV(6, "Chevrolet", "Tahoe", 2017, 28000, 4);
        var truck2 = new Truck(7, "GMC", "Sierra", 2022, 35000, 2500);
        var truck3 = new Truck(13, "Toyota", "Tacoma", 2019, 28000, 1500);

        var added = AuctionManager.AddVehicleToInventory(sedan1);
        var added1 = AuctionManager.AddVehicleToInventory(suv1);
        var added2 = AuctionManager.AddVehicleToInventory(truck1);
        var added3 = AuctionManager.AddVehicleToInventory(sedan2);
        var added4 = AuctionManager.AddVehicleToInventory(suv2);
        var added5 = AuctionManager.AddVehicleToInventory(truck2);
        var added6 = AuctionManager.AddVehicleToInventory(truck3);

        Assert.True(added);
        Assert.True(added1);
        Assert.True(added2);
        Assert.True(added3);
        Assert.True(added4);
        Assert.True(added5);
        Assert.True(added6);


        var truckList = AuctionManager.SearchVehicles(VehicleType.Sedan);
        Assert.Equal(2, truckList.Count);
        var year2017 = AuctionManager.SearchVehicles(null, null, null, 2017);
        Assert.Single(year2017);
        var tyota = AuctionManager.SearchVehicles(null, "toyota", null, null);
        Assert.Equal(2, tyota.Count);
        var accord = AuctionManager.SearchVehicles(null, null, "accord", null);
        Assert.Single(accord);
        var specific = AuctionManager.SearchVehicles(VehicleType.Sedan, "Honda", "accord", 2018);
        Assert.Single(specific);
        var nonExist = AuctionManager.SearchVehicles(VehicleType.Sedan, "Ford", "accord", 2018);
        Assert.Empty(nonExist);
    }

    [Fact]
    public void PlaceBidTest()
    {
        AuctionManager AuctionManager = new AuctionManager();
        var sedan1 = new Sedan(2, "Honda", "Accord", 2018, 18000, 4);
        var added = AuctionManager.AddVehicleToInventory(sedan1);

        Assert.True(added);

        var auction = AuctionManager.CreateAuction(1);
        var auctionSedan1 = AuctionManager.AddVehicleToAuction(1, 2);
        var startAuction = AuctionManager.StartAuction(1);

        Assert.True(auction);
        Assert.True(auctionSedan1);
        Assert.True(startAuction);

        var overBid = AuctionManager.PlaceBidVehicle(2, 20000);
        var underBid = AuctionManager.PlaceBidVehicle(2, 1000);
        var underBidReplace = AuctionManager.PlaceBidVehicle(2, 19000);
        var newOverBid = AuctionManager.PlaceBidVehicle(2, 21000);

        Assert.True(overBid);
        Assert.False(underBid);
        Assert.False(underBidReplace);
        Assert.True(newOverBid);
    }

    [Fact]
    public void PlaceBidClosedOrUnstartedTest()
    {
        AuctionManager AuctionManager = new AuctionManager();
        var sedan1 = new Sedan(2, "Honda", "Accord", 2018, 18000, 4);
        var added = AuctionManager.AddVehicleToInventory(sedan1);

        Assert.True(added);

        var auction = AuctionManager.CreateAuction(1);
        var auctionSedan1 = AuctionManager.AddVehicleToAuction(1, 2);

        Assert.True(auction);
        Assert.True(auctionSedan1);

        var notStartAuctionBid = AuctionManager.PlaceBidVehicle(2, 20000);
        Assert.False(notStartAuctionBid);

        var start = AuctionManager.StartAuction(1);
        Assert.True(start);

        var bidStart = AuctionManager.PlaceBidVehicle(2, 20000);
        Assert.True(bidStart);

        var closed = AuctionManager.CloseAuction(1);
        Assert.True(closed);

        var bidClosed = AuctionManager.PlaceBidVehicle(2, 20000);
        Assert.False(bidClosed);
    }
}