using DroneDelivery.Models;
using DroneDelivery.Services;
using DroneDelivery.Utils;

string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

string filepath = Path.Combine(projectDirectory, "Assets", "input.txt");
string outputFilePath = Path.Combine(projectDirectory, "Assets", "output.txt");

(Drone[] drones, Location[] locations) = Deserializer.Deserialize(filepath);

TripPlanner tripPlanner = new TripPlanner(drones, locations);

tripPlanner.Plan();
string planContent = tripPlanner.Print();

File.WriteAllText(outputFilePath, planContent);