using System;

public class Car : ICar
{
    public IDrivingProcessor drivingProcessor; // car #2
    public IDrivingInformationDisplay drivingInformationDisplay; // car #2
    public IEngine engine;
    public IFuelTank fuelTank;
    public IFuelTankDisplay fuelTankDisplay;
    public bool EngineIsRunning { get => engine.IsRunning; }
    public Car(double fuelLevel = 20, int maxAcceleration = 10)
    {
        drivingProcessor = new DrivingProcessor(maxAcceleration, this); // car #2
        drivingInformationDisplay = new DrivingInformationDisplay(this); // car #2
        engine = new Engine(this);
        fuelTank = new FuelTank(fuelLevel, this);
        fuelTankDisplay = new FuelTankDisplay(this);
    }
    public void EngineStart() => engine.Start();
    public void EngineStop() => engine.Stop();
    public void RunningIdle() => fuelTank.Consume(0.0003);
    public void Refuel(double liters) => fuelTank.Refuel(liters);
    public void BrakeBy(int speed) => drivingProcessor.ReduceSpeed(speed); // car #2
    public void Accelerate(int speed) => drivingProcessor.IncreaseSpeedTo(speed); // car #2
    public void FreeWheel() => drivingProcessor.ReduceSpeed(1); // car #2
}

public class Engine : IEngine
{
    private Car _car;
    public bool IsRunning { get; private set; }
    public Engine(Car car) => _car = car;
    public void Start() => IsRunning = _car.fuelTank.FillLevel > 0 ? true : false;
    public void Stop() => IsRunning = false;
    public void Consume(double liters) => _car.fuelTank.Consume(liters);
}

public class FuelTank : IFuelTank
{
    private Car _car;
    const double maxLevel = 60; const double minLevel = 0; const double reserveLevel = 5;
    public double FillLevel { get; private set; }
    public bool IsOnReserve { get => FillLevel < reserveLevel ? true : false; }
    public bool IsComplete { get => FillLevel == maxLevel ? true : false; }
    public FuelTank(double fuelLevel, Car car)
    {
        FillLevel = fuelLevel > maxLevel ? maxLevel : fuelLevel > minLevel ? fuelLevel : minLevel;
        _car = car;
    }
    public void Consume(double liters) 
    {
        if (_car.EngineIsRunning)
        {
            FillLevel = FillLevel - liters > minLevel ? FillLevel - liters : minLevel;
            if (FillLevel == minLevel) _car.engine.Stop();
        }
    }
    public void Refuel(double liters) => FillLevel = FillLevel + liters < maxLevel ? FillLevel + liters : maxLevel;
}

public class FuelTankDisplay : IFuelTankDisplay
{
    private Car _car;
    public double FillLevel => Math.Round(_car.fuelTank.FillLevel, 2);
    public bool IsOnReserve => _car.fuelTank.IsOnReserve;
    public bool IsComplete => _car.fuelTank.IsComplete;
    public FuelTankDisplay(Car car) => _car = car;
}

public class DrivingProcessor : IDrivingProcessor // car #2
{
    private Car _car;
    private int _acceleration;
    const int maxSpeed = 250; const int minSpeed = 0; const int maxSpeedReduction = 10;
    const int maxAcceleration = 20; const int minAcceleration = 5;
    public int ActualSpeed { get; private set; }
    public DrivingProcessor(int acceleration, Car car)
    {
        ActualSpeed = minSpeed;
        _acceleration = acceleration > maxAcceleration ? maxAcceleration : 
                        acceleration < minAcceleration ? minAcceleration : acceleration;
        _car = car;
    }

    public void IncreaseSpeedTo(int speed)
    {
        if (_car.EngineIsRunning)
        {
            if (ActualSpeed < speed)
            {
                ActualSpeed += _acceleration;
                ActualSpeed = ActualSpeed > speed ? speed : ActualSpeed;
                ActualSpeed = ActualSpeed > maxSpeed ? maxSpeed : ActualSpeed < minSpeed ? minSpeed : ActualSpeed;
            }
            else _car.FreeWheel();
            double liters = 0;
            if (ActualSpeed > 1 && ActualSpeed <= 60) liters = 0.0020;
            if (ActualSpeed > 60 && ActualSpeed <= 100) liters = 0.0014;
            if (ActualSpeed > 100 && ActualSpeed <= 140) liters = 0.0020;
            if (ActualSpeed > 140 && ActualSpeed <= 200) liters = 0.0025;
            if (ActualSpeed > 200 && ActualSpeed <= 250) liters = 0.0030;
            _car.fuelTank.Consume(liters);
        }
    }
    public void ReduceSpeed(int speed)
    {
        ActualSpeed -= speed > maxSpeedReduction ? maxSpeedReduction : speed > minSpeed ? speed : minSpeed;
        ActualSpeed = ActualSpeed > minSpeed ? ActualSpeed : minSpeed;
        if (ActualSpeed == minSpeed) _car.RunningIdle();
    }
}

public class DrivingInformationDisplay : IDrivingInformationDisplay // car #2
{
    private Car _car;
    public int ActualSpeed => _car.drivingProcessor.ActualSpeed;
    public DrivingInformationDisplay(Car car) => _car = car;
}
