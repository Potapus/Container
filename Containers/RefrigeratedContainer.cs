﻿using ConsoleApp1.Enums;

namespace ConsoleApp1.Containers;

public class RefrigeratedContainer : Container
{
    public RefrigeratedContainer(
        double height,
        double depth,
        double tareWeight,
        double maxPayload,
        ProductType productType,
        double maintainedTemperature
    )
        : base(
            height,
            depth,
            tareWeight,
            maxPayload,
            'C')
    {
        if (TemperatureValidator.IsValid(productType, maintainedTemperature))
        {
            ProductType = productType;
            MaintainedTemperature = maintainedTemperature;
        }
        else
        {
            throw new ArgumentException("Invalid temperature for the provided product type");
        }
    }

    public ProductType ProductType { get; }
    public double MaintainedTemperature { get; }

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"Maintained Temperature: {MaintainedTemperature}°C, Product Type: {ProductType}");
    }
}