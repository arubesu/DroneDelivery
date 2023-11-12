using DroneDelivery.Models;
using DroneDelivery.Services;

namespace DroneDelivery.Tests.Services;

[TestFixture]
public class TripPlannerTests
{
    [Test]
    public void Plan_ValidInput_ReturnsPlannedTrips()
    {
        // Arrange
        var drones = new[]
        {
            new Drone("DroneA", 200),
            new Drone("DroneB", 250),
            new Drone("DroneC", 100),
        };

        var locations = new[]
        {
            new Location("LocationA", 200),
            new Location("LocationB", 150),
            new Location("LocationC", 50),
            new Location("LocationD", 150),
            new Location("LocationE", 100),
            new Location("LocationF", 200),
            new Location("LocationG", 50),
            new Location("LocationH", 80),
            new Location("LocationI", 70),
            new Location("LocationJ", 50),
            new Location("LocationK", 30),
            new Location("LocationL", 20),
            new Location("LocationM", 50),
            new Location("LocationN", 30),
            new Location("LocationO", 20),
            new Location("LocationP", 90),
        };

        var planner = new TripPlanner(drones, locations);

        // Act
        var plannedTrips = planner.Plan();

        // Assert
        Assert.IsNotNull(plannedTrips);
        Assert.AreEqual(7, plannedTrips.Length);
    }

    [Test]
    public void Print_ValidPlan_ReturnsPrintedPlan()
    {
        // Arrange
        var drones = new[]
        {
            new Drone("DroneA", 200),
            new Drone("DroneB", 250),
        };

        var locations = new[]
        {
            new Location("LocationA", 200),
            new Location("LocationB", 150),
            new Location("LocationC", 50),
        };

        var expectedPlan = "[DroneA]\nTrip #1\n[LocationB], [LocationC]\nTrip #2\n[LocationA]\n\n";

        var planner = new TripPlanner(drones, locations);
        planner.Plan(); 

        // Act
        var printedPlan = planner.Print();

        // Assert
        Assert.IsNotNull(printedPlan);
        Assert.AreEqual(expectedPlan, printedPlan);
    }
    
    [Test]
    public void FindTripWithMaxLocations_ValidData_ReturnsValidTrip()
    {
        // Arrange
        var drone = new Drone("Drone", 250);

        var locations = new[]
        {
            new Location("LocationA", 200),
            new Location("LocationB", 150),
            new Location("LocationC", 50),
            new Location("LocationD", 150),
            new Location("LocationE", 100),
            new Location("LocationF", 200),
            new Location("LocationG", 50),
            new Location("LocationH", 80),
            new Location("LocationI", 70),
            new Location("LocationJ", 50),
            new Location("LocationK", 30),
            new Location("LocationL", 20),
            new Location("LocationM", 50),
            new Location("LocationN", 30),
            new Location("LocationO", 20),
            new Location("LocationP", 90),
        };

        var planner = new TripPlanner(new []{drone}, locations);

        // Act
        var result = planner.FindTripWithMaxLocations(locations, drone);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(result.Locations.Count, 6);
        Assert.AreEqual(result.Locations.Sum(x => x.Weight), 240);
    }
}