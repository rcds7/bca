namespace Vehicle;

public class SUV : Vehicle
{
    int nDoors;
    public SUV(int id, string manufacturer, string model, int year, decimal startingBid, int nDoors) : base(id, VehicleType.SUV, manufacturer, model, year, startingBid)
    {
        this.nDoors = nDoors;
    }
}