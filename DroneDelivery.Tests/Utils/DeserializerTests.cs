using DroneDelivery.Models;
using DroneDelivery.Utils;

namespace DroneDelivery.Tests.Utils;

[TestFixture]
public class DeserializerTests
{
    private const string TestFilePath = "test_data.txt";

    [Test]
    public void Deserialize_ValidFile_ReturnsDronesAndLocations()
    {
        // Create a test file with sample data
        File.WriteAllText(TestFilePath, "[DroneA, 200],[DroneB, 250]\n[LocationA, 200]\n[LocationB, 150]");

        // Act
        var (drones, locations) = Deserializer.Deserialize(TestFilePath);

        // Assert
        Assert.IsNotNull(drones);
        Assert.IsNotNull(locations);
        Assert.AreEqual(2, drones.Length);
        Assert.AreEqual(2, locations.Length);

        var droneA = new Drone("DroneA", 200);
        var droneB = new Drone("DroneB", 250);
        
        var locationA = new Location("LocationA", 200);
        var locationB = new Location("LocationB", 150);

        Assert.That(drones[0].Model, Is.EqualTo(droneA.Model));
        Assert.That(drones[0].Capacity, Is.EqualTo(droneA.Capacity));
        
        Assert.That(drones[1].Model, Is.EqualTo(droneB.Model));
        Assert.That(drones[1].Capacity, Is.EqualTo(droneB.Capacity));
        
        Assert.That(locations[0].Name, Is.EqualTo(locationA.Name));
        Assert.That(locations[0].Weight, Is.EqualTo(locationA.Weight));
        
        Assert.That(locations[1].Name, Is.EqualTo(locationB.Name));
        Assert.That(locations[1].Weight, Is.EqualTo(locationB.Weight));
        
        // Clean up
        File.Delete(TestFilePath);
    }
}