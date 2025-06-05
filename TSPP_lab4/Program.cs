using System;
using System.Collections.Generic;

//Factory Method
public interface ITransport
{
    void Drive();
}
public class Car : ITransport
{
    public void Drive() => Console.WriteLine("Driving a car.");
}
public class Bike : ITransport
{
    public void Drive() => Console.WriteLine("Riding a bike.");
}
public class Bus : ITransport
{
    public void Drive() => Console.WriteLine("Driving a bus.");
}
public abstract class TransportFactory
{
    public abstract ITransport CreateTransport();
}
public class CarFactory : TransportFactory
{
    public override ITransport CreateTransport() => new Car();
}
public class BikeFactory : TransportFactory
{
    public override ITransport CreateTransport() => new Bike();
}
public class BusFactory : TransportFactory
{
    public override ITransport CreateTransport() => new Bus();
}

//Composite
public abstract class OrganizationComponent
{
    public string Name { get; set; }
    public OrganizationComponent(string name) => Name = name;
    public abstract void Display(int depth);
}
public class Employee : OrganizationComponent
{
    public Employee(string name) : base(name) { }
    public override void Display(int depth) =>
        Console.WriteLine(new string('-', depth) + " Employee: " + Name);
}
public class Department : OrganizationComponent
{
    private readonly List<OrganizationComponent> _components = new();
    public Department(string name) : base(name) { }

    public void Add(OrganizationComponent component) => _components.Add(component);

    public override void Display(int depth)
    {
        Console.WriteLine(new string('-', depth) + " Department: " + Name);
        foreach (var component in _components)
            component.Display(depth + 2);
    }
}

//Strategy
public interface IRouteStrategy
{
    void CalculateRoute(string start, string end);
}
public class ShortestPath : IRouteStrategy
{
    public void CalculateRoute(string start, string end) =>
        Console.WriteLine($"[ShortestPath] {start} -> {end}");
}
public class FastestRoute : IRouteStrategy
{
    public void CalculateRoute(string start, string end) =>
        Console.WriteLine($"[FastestRoute] {start} -> {end}");
}
public class ScenicRoute : IRouteStrategy
{
    public void CalculateRoute(string start, string end) =>
        Console.WriteLine($"[ScenicRoute] {start} -> {end}");
}
public class Navigator
{
    private IRouteStrategy _strategy;
    public void SetStrategy(IRouteStrategy strategy) => _strategy = strategy;
    public void Navigate(string start, string end) => _strategy.CalculateRoute(start, end);
}

//Main Test
public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Factory Method Pattern ===");
        TransportFactory carFactory = new CarFactory();
        ITransport car = carFactory.CreateTransport();
        car.Drive();

        TransportFactory bikeFactory = new BikeFactory();
        ITransport bike = bikeFactory.CreateTransport();
        bike.Drive();

        TransportFactory busFactory = new BusFactory();
        ITransport bus = busFactory.CreateTransport();
        bus.Drive();

        Console.WriteLine("\n=== Composite Pattern ===");
        var devDept = new Department("Development");
        devDept.Add(new Employee("Katya"));
        devDept.Add(new Employee("Tanya"));

        var hrDept = new Department("HR");
        hrDept.Add(new Employee("Dima"));

        var mainCompany = new Department("TechCorp");
        mainCompany.Add(devDept);
        mainCompany.Add(hrDept);

        mainCompany.Display(1);

        Console.WriteLine("\n=== Strategy Pattern ===");
        var navigator = new Navigator();

        navigator.SetStrategy(new ShortestPath());
        navigator.Navigate("City A", "City B");

        navigator.SetStrategy(new FastestRoute());
        navigator.Navigate("City A", "City B");

        navigator.SetStrategy(new ScenicRoute());
        navigator.Navigate("City A", "City B");
    }
}