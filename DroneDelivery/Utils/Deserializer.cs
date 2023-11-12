using DroneDelivery.Models;

namespace DroneDelivery.Utils;

public static class Deserializer
{
   public static (Drone[], Location[]) Deserialize(string filePath)
    {
        var lines = File.ReadLines(filePath);
        
        var droneLine = lines.FirstOrDefault();

        var droneData = droneLine.Split(',').Select(part => part.Trim('[', ']', ' ')).ToArray();

        var drones = new List<Drone>();

        for (int i = 0; i < droneData.Length; i += 2)
        {
            drones.Add(new Drone(model: droneData[i], capacity:int.Parse(droneData[i + 1])));
        }

        var locationLines = lines.Skip(1);

        var locations = new List<Location>();
        
        foreach (var line in locationLines)
        {
            var locationData = line.Split(',').Select(part => part.Trim('[', ']', ' ')).ToArray();
            locations.Add(new Location(name: locationData[0], weight:int.Parse(locationData[1])));
        }

        return (drones.ToArray(), locations.ToArray());
    }
}