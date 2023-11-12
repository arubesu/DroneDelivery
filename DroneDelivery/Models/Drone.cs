namespace DroneDelivery.Models;

public class Drone
{
    public string Model { get; set; }
    public int Capacity { get; set; }
    
    public Drone(string model, int capacity)
    {
        Model = model;
        Capacity = capacity;
    }
}