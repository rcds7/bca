namespace Vehicle;

public enum VehicleType
{
    Sedan,
    SUV,
    Truck
}

public class Vehicle
{
    public int Id { get; }
    public string Manufacturer { get; }
    public string Model { get; }
    public int Year { get; }
    public decimal StartingBid { get; private set; }
    public int? AuctionId { get; private set; }
    public VehicleType Type { get; }

    public Vehicle(int id, VehicleType type, string manufacturer, string model, int year, decimal startingBid)
    {
        Id = id;
        Type = type;
        Manufacturer = manufacturer;
        Model = model;
        Year = year;
        StartingBid = startingBid;
    }

    public void ClearAuctionId()
    {
        AuctionId = null;
    }

    public void SetAuctionId(int auctionId)
    {
        AuctionId = auctionId;
    }

    public bool NewBid(decimal newBid)
    {
        if (newBid <= StartingBid) return false;
        StartingBid = newBid;
        return true;
    }
}