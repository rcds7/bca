namespace Auction;

using Vehicle;

public class Auction
{
    Dictionary<int, Vehicle> Vehicles = new Dictionary<int, Vehicle>();
    public int Id;
    public bool Started { get; private set; }
    public bool Closed { get; private set; }

    public Auction(int id)
    {
        Id = id;
        Started = false;
        Closed = false;
    }

    public bool StartAuction()
    {
        if (Started == false)
        {
            Started = true;
            return true;
        }
        return false;
    }

    public bool CloseAuction()
    {
        if (Started == false) return false;
        Closed = true;
        foreach (var vehicle in Vehicles)
        {
            vehicle.Value.ClearAuctionId();
        }
        return true;
    }

    public bool AddVehicle(Vehicle vehicle)
    {
        if (Closed)
            return false;
        if (Vehicles.ContainsKey(vehicle.Id))
            return false;
        Vehicles.Add(vehicle.Id, vehicle);
        vehicle.SetAuctionId(Id);
        return true;
    }
}