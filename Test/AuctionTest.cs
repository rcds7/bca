namespace Test;

using Auction;
using Vehicle;

public class AuctionTest
{
    [Fact]
    public void AddVehicleToAuctionTest()
    {
        AuctionManager auctionManager = new AuctionManager();
        var vehicle = new Sedan(1, "Toyota", "Camry", 2020, 15000, 4);

        var added = auctionManager.AddVehicleToInventory(vehicle);
        Assert.True(added);

        var auction = auctionManager.CreateAuction(1);
        Assert.True(auction);

        var addedToAuction = auctionManager.AddVehicleToAuction(1, 1);
        Assert.True(addedToAuction);
    }

    [Fact]
    public void AddSameVehicleToAnotherAuctionTest()
    {
        AuctionManager auctionManager = new AuctionManager();
        var vehicle = new Sedan(1, "Toyota", "Camry", 2020, 15000, 4);

        var added = auctionManager.AddVehicleToInventory(vehicle);
        Assert.True(added);

        var auction = auctionManager.CreateAuction(1);
        Assert.True(auction);

        var auction2 = auctionManager.CreateAuction(2);
        Assert.True(auction2);

        var addedToAuction = auctionManager.AddVehicleToAuction(1, 1);
        Assert.True(addedToAuction);

        var addedToSameAuction = auctionManager.AddVehicleToAuction(1, 1);
        Assert.False(addedToSameAuction);

        var addedToAuction2 = auctionManager.AddVehicleToAuction(2, 1);
        Assert.False(addedToAuction2);
    }

    [Fact]
    public void CreateAuctionTest()
    {
        AuctionManager auctionManager = new AuctionManager();

        var auction = auctionManager.CreateAuction(1);
        Assert.True(auction);
    }

    [Fact]
    public void CreateSameAuctionTest()
    {
        AuctionManager auctionManager = new AuctionManager();

        var auction = auctionManager.CreateAuction(1);
        Assert.True(auction);

        var auction1 = auctionManager.CreateAuction(1);
        Assert.False(auction1);
    }

    [Fact]
    public void StartAuctionTest()
    {
        AuctionManager auctionManager = new AuctionManager();

        var auction = auctionManager.CreateAuction(1);
        Assert.True(auction);

        var start = auctionManager.StartAuction(1);
        Assert.True(start);
    }

    [Fact]
    public void StartAuctionAlreadyStartedTest()
    {
        AuctionManager auctionManager = new AuctionManager();

        var auction = auctionManager.CreateAuction(1);
        Assert.True(auction);

        var start = auctionManager.StartAuction(1);
        Assert.True(start);

        var startAgain = auctionManager.StartAuction(1);
        Assert.False(startAgain);
    }


    [Fact]
    public void CloseAuctionTest()
    {
        AuctionManager auctionManager = new AuctionManager();

        var auction = auctionManager.CreateAuction(1);
        Assert.True(auction);

        var start = auctionManager.StartAuction(1);
        Assert.True(start);

        var close = auctionManager.CloseAuction(1);
        Assert.True(close);
    }

    [Fact]
    public void CloseNonExistedAuctionTest()
    {
        AuctionManager auctionManager = new AuctionManager();

        var auction = auctionManager.CreateAuction(1);
        Assert.True(auction);

        var close = auctionManager.CloseAuction(2);
        Assert.False(close);
    }

    [Fact]
    public void PlaceBidClosedOrUnstartedAuctionTest()
    {
        AuctionManager auctionManager = new AuctionManager();
        var sedan1 = new Sedan(2, "Honda", "Accord", 2018, 18000, 4);

        var added = auctionManager.AddVehicleToInventory(sedan1);
        Assert.True(added);

        var auction = auctionManager.CreateAuction(1);
        Assert.True(auction);

        var auctionSedan1 = auctionManager.AddVehicleToAuction(1, 2);
        Assert.True(auctionSedan1);

        var notStartAuctionBid = auctionManager.PlaceBidVehicle(2, 20000);
        Assert.False(notStartAuctionBid);

        var start = auctionManager.StartAuction(1);
        Assert.True(start);

        var bidStart = auctionManager.PlaceBidVehicle(2, 20000);
        Assert.True(bidStart);

        var closed = auctionManager.CloseAuction(1);
        Assert.True(closed);

        var bidClosed = auctionManager.PlaceBidVehicle(2, 20000);
        Assert.False(bidClosed);
    }
}