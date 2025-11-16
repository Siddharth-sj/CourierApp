#Courier Delivery Cost & Time Estimation CLI
A lightweight, modular, and extensible command-line application that calculates delivery cost, discounts, and estimated delivery times for courier packages.
It simulates real-world delivery constraints such as vehicle capacity, speed, shipment grouping, and return trip scheduling.

This solves the common logistics problem of:

Estimating delivery cost per package
Maximizing shipment efficiency
Scheduling delivery using multiple vehicles
Applying discount rules based on offers
Providing accurate delivery ETA for each package

#Getting started
This application runs directly from the terminal after building the .NET project.

To get started:

1. Clone/download the repository
2. Edit appsettings.json if you want to modify offer rules
3. Build and run the project:
dotnet build
dotnet run --project CourierApp.ConsoleApp

You will be prompted for:

1. Base delivery cost
2. Package details
3. Vehicle details

The system calculates:

1. Discount
2. Final cost
3. Estimated delivery time in hours

#Project Structure:
CourierApp/
│
├── Interfaces/                   # Abstractions for core services
│   ├── ICostCalculation.cs
│   ├── IDeliveryTime.cs
│   ├── IInputService.cs
│   ├── IOfferService.cs
│   └── IOutputService.cs
│
├── Models/                       # Domain models used across the application
│   ├── CourierPackage.cs
│   ├── DeliveryOutput.cs
│   ├── OfferSettings.cs
│   ├── OfferSpecs.cs
│   └── VehSettings.cs
│
├── Services/                     # Business logic implementation
│   ├── InputService.cs
│   ├── OutputService.cs
│   ├── DeliveryCostCalculationService.cs
│   ├── DeliveryTimeCalculationService.cs
│   └── OfferService.cs
│
├── appsettings.json              # Offer rules configuration
└── Program.cs                    # Application entry point

1. Interfaces/
Defines all abstraction layers ensuring loose coupling, testability, and easy extension.

2. Models/
Contains strongly-typed classes representing all input, output, and configuration structures.

3. Services/
Implements core business logic including:
User input handling
Output formatting
Cost calculation
Delivery scheduling logic
Offer/discount processing

4. Program.cs
The entry point where dependencies are registered and the CLI workflow is orchestrated.

5. appsettings.json
Configurable offer rules used by the OfferService


#Prerequisites
1. .NET 6.0+ SDK installed
2. A terminal/command prompt

#How Code Works
1. Delivery Cost Calculation For each package: 
cost = base_cost + (weight × 10) + (distance × 5)
discount = cost × discount_percentage
total_cost = cost - discount

Discount percentage is determined using:
. Offer code
. Weight range
. Distance range
Configured in appsettings.json.

2. Delivery Time Estimation
Delivery time estimation follows these business rules:

. Each vehicle has:
Max weight capacity (L)
Speed (S)

. A shipment:
Must maximize number of packages
If equal, prefer heavier total weight
If still equal, pick the one that finishes earlier

. Vehicle timeline:

delivery_time = distance / speed
return_time = delivery_time × 2

. Vehicles operate in parallel, always choosing the next available one.

. Each package’s ETA:
ETA = vehicle_available_time + (package_distance / speed)


#Usage
1. Input Format

base_delivery_cost no_of_packages
pkg_id1 weight1 distance1 offer_code1
pkg_id2 weight2 distance2 offer_code2
...
no_of_vehicles max_speed max_carriable_weight

2. Output Format

pkg_id discount total_cost estimated_delivery_time

3. Example Input

100 5
PKG1 50 30 OFR001
PKG2 75 125 OFFR0008
PKG3 175 100 OFR003
PKG4 110 60 OFR002
PKG5 155 95 NA
2 70 200

4. Example Output

PKG1 0 750 3.98
PKG2 0 1475 1.78
PKG3 0 2350 1.42
PKG4 105 1395 0.85
PKG5 0 2125 4.19

#Test-Driven Development
The project follows TDD approach to ensure correctness, maintainability, and predictable behavior across all delivery cost and time-calculation rules.

1. Test Structure

CourierApp.Tests/
│
├── Models/
│   ├── ModelValidationTests.cs
│   
│
├── Services/
│   ├── DeliveryCostCalculationServiceTests.cs
│   ├── DeliveryTimeCalculationServiceTests.cs
│   ├── OfferServiceTests.cs
│
│── TestData/
│   ├── appsettings.test.json
│
└── ProgramTests.cs

2. How to Run Tests
    a. Open Test Explorer
    b. Click Run All Tests
