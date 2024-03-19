using Auction;
using Vehicle;

public class VehicleTest
{
    [Fact]
    public void AddVehicleToInventoryTest()
    {
        AuctionManager auctionManager = new AuctionManager();

        var vehicle = new Sedan(1, "Toyota", "Camry", 2020, 15000, 4);

        var added = auctionManager.AddVehicleToInventory(vehicle);
        Assert.True(added);
    }

    [Fact]
    public void AddSameVehicleToInventoryTest()
    {
        AuctionManager AuctionManager = new AuctionManager();
        var vehicle = new Sedan(1, "Toyota", "Camry", 2020, 15000, 4);

        var added = AuctionManager.AddVehicleToInventory(vehicle);
        Assert.True(added);

        var addedAgain = AuctionManager.AddVehicleToInventory(vehicle);
        Assert.False(addedAgain);
    }

    [Fact]
    public void SearchVehicles()
    {
        AuctionManager auctionManager = new AuctionManager();

        var sedan1 = new Sedan(2, "Honda", "Accord", 2018, 18000, 4);
        var suv1 = new SUV(3, "Toyota", "RAV4", 2021, 25000, 4);
        var truck1 = new Truck(4, "Ford", "F-150", 2019, 30000, 2000);
        var sedan2 = new Sedan(5, "Nissan", "Altima", 2020, 20000, 4);
        var suv2 = new SUV(6, "Chevrolet", "Tahoe", 2017, 28000, 4);
        var truck2 = new Truck(7, "GMC", "Sierra", 2022, 35000, 2500);
        var truck3 = new Truck(13, "Toyota", "Tacoma", 2019, 28000, 1500);

        var added = auctionManager.AddVehicleToInventory(sedan1);
        Assert.True(added);

        var added1 = auctionManager.AddVehicleToInventory(suv1);
        Assert.True(added1);

        var added2 = auctionManager.AddVehicleToInventory(truck1);
        Assert.True(added2);

        var added3 = auctionManager.AddVehicleToInventory(sedan2);
        Assert.True(added3);

        var added4 = auctionManager.AddVehicleToInventory(suv2);
        Assert.True(added4);

        var added5 = auctionManager.AddVehicleToInventory(truck2);
        Assert.True(added5);

        var added6 = auctionManager.AddVehicleToInventory(truck3);
        Assert.True(added6);

        var truckList = auctionManager.SearchVehicles(VehicleType.Sedan);
        Assert.Equal(2, truckList.Count);

        var year2017 = auctionManager.SearchVehicles(null, null, null, 2017);
        Assert.Single(year2017);

        var tyota = auctionManager.SearchVehicles(null, "toyota", null, null);
        Assert.Equal(2, tyota.Count);

        var accord = auctionManager.SearchVehicles(null, null, "accord", null);
        Assert.Single(accord);

        var specific = auctionManager.SearchVehicles(VehicleType.Sedan, "Honda", "accord", 2018);
        Assert.Single(specific);

        var nonExist = auctionManager.SearchVehicles(VehicleType.Sedan, "Ford", "accord", 2018);
        Assert.Empty(nonExist);
    }

    [Fact]
    public void PlaceVehicleBidTest()
    {
        AuctionManager auctionManager = new AuctionManager();
        var sedan1 = new Sedan(2, "Honda", "Accord", 2018, 18000, 4);

        var added = auctionManager.AddVehicleToInventory(sedan1);
        Assert.True(added);

        var auction = auctionManager.CreateAuction(1);
        Assert.True(auction);

        var auctionSedan1 = auctionManager.AddVehicleToAuction(1, 2);
        Assert.True(auctionSedan1);

        var startAuction = auctionManager.StartAuction(1);
        Assert.True(startAuction);

        var overBid = auctionManager.PlaceBidVehicle(2, 20000);
        Assert.True(overBid);

        var underBid = auctionManager.PlaceBidVehicle(2, 1000);
        Assert.False(underBid);

        var underBidReplace = auctionManager.PlaceBidVehicle(2, 19000);
        Assert.False(underBidReplace);

        var newOverBid = auctionManager.PlaceBidVehicle(2, 21000);
        Assert.True(newOverBid);
    }
}