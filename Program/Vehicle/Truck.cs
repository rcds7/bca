namespace Vehicle;

public class Truck : Vehicle
{
    int loadCapacity;
    public Truck(int id, string manufacturer, string model, int year, decimal startingBid, int loadCapacity) : base(id, VehicleType.Truck, manufacturer, model, year, startingBid)
    {
        this.loadCapacity = loadCapacity;
    }
}