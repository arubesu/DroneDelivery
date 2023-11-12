using System.Text;
using DroneDelivery.Models;

namespace DroneDelivery.Services;

public class TripPlanner
{
    public Drone[] Drones { get;  }
    public Location[] Locations { get;  }
    
    private Location[] _remainingLocations { get; set; }
    private Trip[] _plannedTrips { get; set; }

    public TripPlanner(Drone[] drones, Location[] locations)
    {
        var sortedLocations = locations.OrderByDescending(x => x.Weight).ToArray();

        Drones = drones;
        Locations = locations;
        _remainingLocations = sortedLocations;
    }

    public Trip[] Plan()
    {
        var trips = new List<Trip>();
        
        while (_remainingLocations.Length > 0)
        {
            var tripsWithMaxLocations = new List<Trip>();
            
            foreach (var drone in Drones)
            {
                var tripWithMaxLocations = FindTripWithMaxLocations(_remainingLocations, drone);
                tripsWithMaxLocations.Add(tripWithMaxLocations);
            }
            
            var bestTrip = FindBestTrip(tripsWithMaxLocations);
            trips.Add(bestTrip);
            
            _remainingLocations = _remainingLocations.Except(bestTrip.Locations).ToArray();
        }

        var plannedTrips = trips.ToArray();

        _plannedTrips = plannedTrips;
        
        return plannedTrips;
    }

    public string Print()
    {
        var plannedTripsGroupedByDrone = _plannedTrips.GroupBy(t => t.Drone.Model);

        var sb = new StringBuilder();

        foreach (var tripGroup in plannedTripsGroupedByDrone)
        {
            var trips = tripGroup.ToArray();
            sb.AppendLine($"[{tripGroup.Key}]");
            var tripCount = 0;
            foreach (var trip in trips)
            {
                sb.AppendLine($"Trip #{++tripCount}");
                sb.AppendJoin(", ", trip.Locations.Select(l => $"[{l.Name}]"));
                sb.AppendLine();
            }

            sb.AppendLine();
        }
        
        var planContent = sb.ToString();

        Console.WriteLine(planContent);

        return planContent;
    }

    private  Trip FindBestTrip(List<Trip> tripsWithMaxLocations)
    {
        return tripsWithMaxLocations
            .Where(x => x.Locations.Count > 0)
            .OrderByDescending(x => x.Locations.Count)
            .ThenBy(x => x.GetUnusedCapacity())
            .First();
    }

    private Trip FindTripWithMaxLocations(Location[] locations,  Drone drone)
    {
        var trip = new Trip(drone);
        var droneCapacity = drone.Capacity;
        int length = locations.Length;
        var firstLocation = locations.First(); 
        
        int currentSum = firstLocation.Weight; 
        trip.Locations.Add(firstLocation);
        
        int startIndex = 0; 

        for (int i = 1; i < length; i++)
        {
            // If current sum becomes greater than the target sum, subtract starting elements of the array
            while (currentSum + locations[i].Weight > droneCapacity && startIndex < i)
            {
                currentSum -= locations[startIndex].Weight;
                trip.Locations.Remove(locations[startIndex]);
                
                startIndex++;
            }

            currentSum += locations[i].Weight;
            trip.Locations.Add(locations[i]);
        }
        return trip;
    }
}
