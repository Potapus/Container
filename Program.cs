// See https://aka.ms/new-console-template for more information

using ConsoleApp1;
using ConsoleApp1.Containers;
using ConsoleApp1.Enums;

static void Main()
{
    List<ContainerShip> ships = new List<ContainerShip>();
    List<Container> containers = new List<Container>();

    while (true)
    {
        Console.Write("List of container ships: ");
        if (ships.Count > 0)
        {
            foreach (var ship in ships)
            {
                ship.PrintShipDetails();
            }
        }
        else Console.Write("None\n");

        Console.Write("List of containers: ");
        if (containers.Count > 0)
        {
            foreach (var container in containers)
            {
                container.PrintInfo();
            }
        }
        else Console.Write("None\n");

        if (ships.Count > 0)
        {
            Console.WriteLine("Possible actions:");
            Console.WriteLine("1.Add a container ship");
            Console.WriteLine("2.Remove a container ship");
            Console.WriteLine("3.Add a container");
            if (containers.Count > 0)
            {
                Console.WriteLine("Possible actions:");
                Console.WriteLine("1.Add a container ship");
                Console.WriteLine("2.Remove a container ship");
                Console.WriteLine("3.Add a container");
                Console.WriteLine("4.Remove a container");
                Console.WriteLine("5.Load container to ship");
                Console.WriteLine("6.Unload container from ship");
            }
        }

        else
        {
            Console.WriteLine("Possible actions:");
            Console.WriteLine("1.Add a container ship");
        }

        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                Console.Write("Enter ship name: ");
                string name = Console.ReadLine();
                Console.Write("Enter max speed (knots): ");
                double speed = double.Parse(Console.ReadLine());
                Console.Write("Enter max containers: ");
                int maxContainers = int.Parse(Console.ReadLine());
                Console.Write("Enter max weight (tons): ");
                double maxWeight = double.Parse(Console.ReadLine());
                ships.Add(new ContainerShip(name, speed, maxContainers, maxWeight));
                Console.WriteLine("Ship added successfully!");
                break;
            case "2":
                Console.Write("Enter ship name: ");
                string removeShipName = Console.ReadLine();
                var removeShip = ships.Find(s => s.Name == removeShipName);
                if (removeShip != null)
                {
                    ships.Remove(removeShip);
                }
                else Console.WriteLine("Ship not found!");

                break;
            case "3":

                Console.Write("Enter height: ");
                double height = double.Parse(Console.ReadLine());

                Console.Write("Enter depth: ");
                double depth = double.Parse(Console.ReadLine());

                Console.Write("Enter tare weight: ");
                double tareWeight = double.Parse(Console.ReadLine());

                Console.Write("Enter max payload: ");
                double maxPayload = double.Parse(Console.ReadLine());

                Console.Write("Enter container type (G = Gas,Liquid = L, R = Refrigerated): ");
                char type = char.ToUpper(Console.ReadLine()[0]);
                Container container;

                switch (type)
                {
                    case 'L':
                        Console.Write("Is cargo hazardous? (Y/N)");
                        char isCargoHazardous = char.ToUpper(Console.ReadLine()[0]);
                        bool cargoHazardous = (isCargoHazardous == 'Y');
                        container = new LiquidContainer(height, depth, tareWeight, maxPayload, cargoHazardous);
                        containers.Add(container);
                        Console.WriteLine("Container added successfully!");
                        break;
                    case 'R':
                        Console.WriteLine("Available Product Types:");
                        foreach (var product in Enum.GetValues(typeof(ProductType)))
                        {
                            Console.WriteLine($"{product}");
                        }

                        Console.Write("Enter Product Type: ");
                        string chosenProduct = Console.ReadLine();

                        if (Enum.TryParse<ProductType>(chosenProduct, true, out ProductType selectedProduct))
                        {
                            container = new RefrigeratedContainer(height, depth, tareWeight, maxPayload,
                                selectedProduct, TemperatureValidator.GetTemperature(selectedProduct));
                            containers.Add(container);
                            Console.WriteLine("Container added successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid product type entered.");
                        }

                        break;
                    case 'G':
                        Console.WriteLine("Enter pressure: ");
                        double pressure = double.Parse(Console.ReadLine());
                        container = new GasContainer(height, depth, tareWeight, maxPayload, pressure);
                        containers.Add(container);
                        Console.WriteLine("Container added successfully!");
                        break;
                    default:
                        Console.WriteLine("Invalid container type entered.");
                        break;
                }

                break;
            case "4":
                Console.Write("Enter container serial number: ");
                string serialNumber1 = Console.ReadLine();
                var contai = containers.Find(c => c.SerialNumber == serialNumber1);

                if (contai != null) containers.Remove(contai);
                else Console.WriteLine("Container not found!");

                break;
            case "5":
                Console.Write("Enter ship name: ");
                string shipName = Console.ReadLine();
                Console.Write("Enter container serial number: ");
                string serialNumber = Console.ReadLine();

                var ship = ships.Find(s => s.Name == shipName);
                var cont = containers.Find(c => c.SerialNumber == serialNumber);

                if (ship != null && cont != null)
                {
                    ship.AddContainer(cont);
                    containers.Remove(cont);
                }
                else Console.WriteLine("Ship or container not found.");

                break;

            case "6":
                Console.Write("Enter ship name: ");
                string shipToRemoveFrom = Console.ReadLine();
                Console.Write("Enter container serial number: ");
                string contToRemove = Console.ReadLine();

                var targetShip = ships.Find(s => s.Name == shipToRemoveFrom);
                if (targetShip != null)
                {
                    targetShip.RemoveContainer(contToRemove);
                }
                else
                {
                    Console.WriteLine("Ship not found.");
                }

                break;

            default:
                Console.WriteLine("Invalid choice. Try again.");
                break;
        }

        Console.Clear();
    }
}

Main();