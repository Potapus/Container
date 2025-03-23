namespace ConsoleApp1.Containers;

public class Container
{
    private const string ContainerPrefix = "KON";
    private static int _nextContainer = 1;

    protected Container(
        double height,
        double depth,
        double tareWeight,
        double maxPayload,
        char containerType)
    {
        Height = height > 0 ? height : throw new ArgumentException("Height must be greater than 0");
        Depth = depth > 0 ? depth : throw new ArgumentException("Depth must be greater than 0");
        TareWeight = tareWeight > 0 ? tareWeight : throw new ArgumentException("TareWeight must be greater than 0");
        MaxPayload = maxPayload > 0 ? maxPayload : throw new ArgumentException("MaxPayload must be greater than 0");
        SerialNumber = $"{ContainerPrefix}-{containerType}-{_nextContainer++}";
    }

    public double Height { get; }
    public double Depth { get; }
    public double TareWeight { get; }
    public double MaxPayload { get; }
    public string SerialNumber { get; }
    public double CargoMass { get; protected set; }

    public virtual void LoadCargo(double mass)
    {
        if (mass <= 0) throw new ArgumentException("Mass must be greater than 0");

        if (!CanLoadCargo(mass)) throw new OverflowException();
        CargoMass += mass;
    }
    
    public virtual void EmptyCargo() => CargoMass = 0;
    protected virtual bool CanLoadCargo(double mass) => CargoMass + mass <= MaxPayload;
    public virtual void PrintInfo() => 
        Console.WriteLine($"Container Serial: {SerialNumber},Height: {Height},Depth: {Depth},Tare Weight: {TareWeight},Max Payload: {MaxPayload}");

}