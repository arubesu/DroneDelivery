namespace DroneDelivery.Models;

public class Location
{
    public string Name { get;  }
    public int Weight { get;  }
    
    public Location(string name, int weight)
    {
        Name = name;
        Weight = weight;
    }
}