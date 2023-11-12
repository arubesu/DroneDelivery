namespace DroneDelivery.Models;

public class Trip
{
    public IList<Location> Locations { get; } = new List<Location>();
    public Drone Drone { get; }

    public int GetUnusedCapacity()
    {
        return Drone.Capacity - Locations.Sum(l => l.Weight);
    }

    public Trip(Drone drone)
    {
        Drone = drone;
    }
}
