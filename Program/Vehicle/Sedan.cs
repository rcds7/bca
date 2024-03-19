namespace Vehicle;

public class Sedan : Vehicle
{
    int nDoors;
    public Sedan(int id, string manufacturer, string model, int year, decimal startingBid, int nDoors) : base(id, VehicleType.Sedan, manufacturer, model, year, startingBid)
    {
        this.nDoors = nDoors;
    }
}