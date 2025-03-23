using ConsoleApp1.Containers;

namespace ConsoleApp1
{
    public class ContainerShip
    {
        public string Name { get; }
        public double MaxSpeed { get; }
        public int MaxContainers { get; }
        public double MaxWeight { get; }
        private List<Container> _containers;

        public ContainerShip(string name, double maxSpeed, int maxContainers, double maxWeight)
        {
            Name = name;
            MaxSpeed = maxSpeed;
            MaxContainers = maxContainers;
            MaxWeight = maxWeight;
            _containers = new List<Container>();
        }
        public bool CanLoad(Container container) => !(_containers.Count >= MaxContainers || GetTotalWeight() + container.TareWeight> MaxWeight);

        public bool AddContainer(Container container)
        {
            if (CanLoad(container))
            {
                _containers.Add(container);
                Console.WriteLine($"Container {container.SerialNumber} added.");
                return true;
            }                 
            Console.WriteLine("Cannot add container. Capacity or weight limit exceeded.");
            return false;
        }

        public bool RemoveContainer(string serialNumber)
        {
            var container = _containers.Find(c => c.SerialNumber == serialNumber);
            if (container == null)
            {
                Console.WriteLine("Container not found.");
                return false;
            }
            _containers.Remove(container);
            Console.WriteLine($"Container {serialNumber} removed.");
            return true;
        }

        public void PrintShipDetails()
        {
            Console.WriteLine($"Ship: {Name}, Speed: {MaxSpeed} knots, Max Containers: {MaxContainers}, Max Weight: {MaxWeight} tons");
            Console.WriteLine("Containers on board:");
            foreach (var container in _containers)
            {
                Console.WriteLine($" - {container.SerialNumber} ({container.GetType().Name})");
            }
        }

        private double GetTotalWeight()
        {
            double totalWeight = 0;
            foreach (var container in _containers)
            {
                totalWeight += container.TareWeight;
            }
            return totalWeight;
        }
    }
}
