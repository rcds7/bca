namespace Program;

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

public class Sedan : Vehicle
{
    int nDoors;
    public Sedan(int id, string manufacturer, string model, int year, decimal startingBid, int nDoors) : base(id, VehicleType.Sedan, manufacturer, model, year, startingBid)
    {
        this.nDoors = nDoors;
    }
}

public class SUV : Vehicle
{
    int nDoors;
    public SUV(int id, string manufacturer, string model, int year, decimal startingBid, int nDoors) : base(id, VehicleType.SUV, manufacturer, model, year, startingBid)
    {
        this.nDoors = nDoors;
    }
}

public class Truck : Vehicle
{
    int loadCapacity;
    public Truck(int id, string manufacturer, string model, int year, decimal startingBid, int loadCapacity) : base(id, VehicleType.Truck, manufacturer, model, year, startingBid)
    {
        this.loadCapacity = loadCapacity;
    }
}

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



public class AuctionManager
{
    Dictionary<int, Vehicle> Vehicles = new Dictionary<int, Vehicle>();
    Dictionary<int, Auction> Auctions = new Dictionary<int, Auction>();

    public bool AddVehicleToInventory(Vehicle vehicle)
    {
        if (Vehicles.ContainsKey(vehicle.Id))
            return false;
        Vehicles.Add(vehicle.Id, vehicle);
        return true;
    }

    public bool AddVehicleToAuction(int auctionId, int vehicleId)
    {
        if (Auctions.ContainsKey(auctionId) == false || Vehicles.ContainsKey(vehicleId) == false)
            return false;

        Vehicle vehicle = Vehicles[vehicleId];
        if (vehicle.AuctionId != null)
            return false;

        Auction auction = Auctions[auctionId];
        return auction.AddVehicle(vehicle);
    }

    public bool CreateAuction(int auctionId)
    {
        if (Auctions.ContainsKey(auctionId))
            return false;
        Auction auction = new Auction(auctionId);
        Auctions.Add(auctionId, auction);
        return true;
    }

    public bool CloseAuction(int auctionId)
    {
        if (Auctions.ContainsKey(auctionId) == false)
            return false;
        var result = Auctions[auctionId].CloseAuction();
        if (result)
            Auctions.Remove(auctionId);
        return result;
    }

    public bool StartAuction(int auctionId)
    {
        if (Auctions.ContainsKey(auctionId) == false)
            return false;
        return Auctions[auctionId].StartAuction();
    }

    public Dictionary<int, Vehicle> SearchVehicles(VehicleType? vehicleType = null, string? manufacturer = null, string? model = null, int? year = null)
    {
        if (vehicleType == null && manufacturer == null && model == null && year == null) return this.Vehicles;
        Dictionary<int, Vehicle> result = new Dictionary<int, Vehicle>();
        foreach (var vehicle in this.Vehicles)
        {
            bool? add = null;
            if (vehicleType != null)
            {
                if (vehicle.Value.Type == vehicleType) add = add == null ? true : add & true;
                else add = add == null ? false : add & false;
            }
            if (manufacturer != null)
            {
                if (vehicle.Value.Manufacturer.ToLower() == manufacturer.ToLower()) add = add == null ? true : add & true;
                else add = add == null ? false : add & false;
            }
            if (model != null)
            {
                if (vehicle.Value.Model.ToLower() == model.ToLower()) add = add == null ? true : add & true;
                else add = add == null ? false : add & false;
            }
            if (year != null)
            {
                if (vehicle.Value.Year == year) add = add == null ? true : add & true;
                else add = add == null ? false : add & false;
            }
            if (add != null)
                if ((bool)add)
                    result.Add(vehicle.Key, vehicle.Value);
        }
        return result;
    }

    public bool PlaceBidVehicle(int vehicleId, decimal bid)
    {
        if (Vehicles.ContainsKey(vehicleId) == false)
            return false;
        Vehicle vehicle = Vehicles[vehicleId];
        if (vehicle.AuctionId == null)
            return false;
        if (Auctions.ContainsKey((int)vehicle.AuctionId) == false)
            return false;
        Auction auction = Auctions[(int)vehicle.AuctionId];
        if (auction.Started == false) return false;
        if (auction.Closed == true) return false;
        return vehicle.NewBid(bid);
    }
}

class Program
{
    static void Main(string[] args)
    {
    }
}

