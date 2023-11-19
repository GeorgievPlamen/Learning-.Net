using System.ComponentModel;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Press any key to start device...");
        Console.ReadKey();

        IDevice device = new Device();

        device.RunDevice();

        Console.ReadKey();
    }

    public class Device : IDevice
    {
        const double Warning_Level = 27;
        const double Emergency_Level = 75;

        public double WarningTemperatureLevel => Warning_Level;

        public double EmergencyTemperatureLevel => Emergency_Level;

        public void HandleEmergency()
        {
            Console.WriteLine();
            Console.WriteLine("Sending out notifications to emergency services personel...");
            ShutDownDevice();
            Console.WriteLine();
        }

        private void ShutDownDevice()
        {
            Console.WriteLine("Shutting down device...");
        }
        public void RunDevice()
        {
            Console.WriteLine("Device is running...");

            ICoolingMechanism coolingMechanism = new CoolingMechanism();
            IHeatSenser heatSensor = new HeatSensor(Warning_Level,Emergency_Level);
            IThermostat thermostat = new Thermostat(this, coolingMechanism,heatSensor);

            thermostat.RunThermostat();
        }
    }

    public class HeatSensor : IHeatSenser
    {
        double _warningLevel = 0;
        double _emergencyLevel = 0;

        bool _hasReachedWarningTemperature = false;

        protected EventHandlerList _listEventDelegates = new EventHandlerList();

        static readonly object _temperatureReachesWarningLevelKey = new object();
        static readonly object _temperatureFallsBelowWarningLevelKey = new object();
        static readonly object _temperatureReachesEmergencyLevelKey = new object();

        private double[] _temperatueData = null;

        public HeatSensor(double warningLevel, double emergencyLevel)
        {
            _emergencyLevel = emergencyLevel;
            _warningLevel = warningLevel;

            SeedData();
        }

        private void MonitorTemperature()
        {
            foreach (double temperature in _temperatueData)
            {
                Console.WriteLine($"{DateTime.Now} Temperature: {temperature}");

                if (temperature >= _emergencyLevel)
                {
                    TemperatueEventArgs e = new TemperatueEventArgs
                    {
                        Temperature = temperature,
                        CurrentDateTime = DateTime.Now,
                    };
                    OnTemperatureReachesEmergencyLevel(e);
                }
                else if (temperature >= _warningLevel)
                {
                    _hasReachedWarningTemperature = true;
                    TemperatueEventArgs e = new TemperatueEventArgs
                    {
                        Temperature = temperature,
                        CurrentDateTime = DateTime.Now
                    };
                    OnTemperatureReachesWarningLevel(e);
                }
                else if (temperature < _warningLevel && _hasReachedWarningTemperature)
                {
                    _hasReachedWarningTemperature = false;
                    TemperatueEventArgs e = new TemperatueEventArgs
                    {
                        Temperature = temperature,
                        CurrentDateTime = DateTime.Now
                    };
                    OnTemperatureFallsBelowWarningLevel(e);
                }

                System.Threading.Thread.Sleep(1000);
            }
        }

        private void SeedData()
        {
            _temperatueData = new double[] { 16, 17, 16.5, 18, 19, 22, 24, 26.75, 28.7, 27.6, 26, 24, 22, 45, 68, 86.45 };
        }

        protected void OnTemperatureReachesWarningLevel(TemperatueEventArgs e)
        {
            EventHandler<TemperatueEventArgs> handler = (EventHandler<TemperatueEventArgs>)_listEventDelegates[_temperatureReachesWarningLevelKey];

            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected void OnTemperatureReachesEmergencyLevel(TemperatueEventArgs e)
        {
            EventHandler<TemperatueEventArgs> handler = (EventHandler<TemperatueEventArgs>)_listEventDelegates[_temperatureReachesEmergencyLevelKey];

            if (handler != null)
            {
                handler(this, e);
            }
        }
        protected void OnTemperatureFallsBelowWarningLevel(TemperatueEventArgs e)
        {
            EventHandler<TemperatueEventArgs> handler = (EventHandler<TemperatueEventArgs>)_listEventDelegates[_temperatureFallsBelowWarningLevelKey];

            if (handler != null)
            {
                handler(this, e);
            }
        }


        event EventHandler<TemperatueEventArgs> IHeatSenser.TemperatureReachesEmergencyLevelEventHandler
        {
            add
            {
                _listEventDelegates.AddHandler(_temperatureReachesEmergencyLevelKey, value);
            }

            remove
            {
                _listEventDelegates.RemoveHandler(_temperatureReachesEmergencyLevelKey, value);
            }
        }

        event EventHandler<TemperatueEventArgs> IHeatSenser.TemperatureReachesWarningLevelEventHandler
        {
            add
            {
                _listEventDelegates.AddHandler(_temperatureReachesWarningLevelKey, value);
            }

            remove
            {
                _listEventDelegates.RemoveHandler(_temperatureReachesWarningLevelKey, value);

            }
        }

        event EventHandler<TemperatueEventArgs> IHeatSenser.TemperatureFallsBelowWarningLevelEventHandler
        {
            add
            {
                _listEventDelegates.AddHandler(_temperatureFallsBelowWarningLevelKey, value);

            }

            remove
            {
                _listEventDelegates.RemoveHandler(_temperatureFallsBelowWarningLevelKey, value);
            }
        }

        public void RunHeatSensor()
        {
            Console.WriteLine("Heat sensor is running...");
            MonitorTemperature();
        }
    }

    public class CoolingMechanism : ICoolingMechanism
    {
        public void Off()
        {
            Console.WriteLine();
            Console.WriteLine("Switching cooling mechanism off...");
            Console.WriteLine();
        }

        public void On()
        {
            Console.WriteLine();
            Console.WriteLine("Switching cooling mechanism on...");
            Console.WriteLine();
        }
    }
    public class Thermostat : IThermostat
    {
        private ICoolingMechanism _coolingMechanism = null;
        private IHeatSenser _heatSensor = null;
        private IDevice _device = null;

        private const double WarningLevel = 27;
        private const double EmergencyLevel = 75;

        public Thermostat(IDevice device, ICoolingMechanism coolingMechanism, IHeatSenser heatSensor)
        {
            _coolingMechanism = coolingMechanism;
            _heatSensor = heatSensor;
            _device = device;
        }

        private void WireUpEventsToEventHandlers()
        {
            _heatSensor.TemperatureReachesWarningLevelEventHandler += _heatSensor_TemperatureReachesWarningLevelEventHandler;
            _heatSensor.TemperatureReachesEmergencyLevelEventHandler += _heatSensor_TemperatureReachesEmergencyLevelEventHandler;
            _heatSensor.TemperatureFallsBelowWarningLevelEventHandler += _heatSensor_TemperatureFallsBelowWarningLevelEventHandler;
        }

        private void _heatSensor_TemperatureFallsBelowWarningLevelEventHandler(object? sender, TemperatueEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            Console.WriteLine($"Information Alert!! Temperature falls below warning level (Warning level is between {_device.WarningTemperatureLevel} and {_device.EmergencyTemperatureLevel})");
            _coolingMechanism.Off();
            Console.ResetColor();
        }

        private void _heatSensor_TemperatureReachesEmergencyLevelEventHandler(object? sender, TemperatueEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine($"Emergency Alert!! (Emergency level is above {_device.EmergencyTemperatureLevel})");
            _device.HandleEmergency();
            Console.ResetColor();
        }

        private void _heatSensor_TemperatureReachesWarningLevelEventHandler(object? sender, TemperatueEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            Console.WriteLine($"Warning Alert!! (Warning level is between {_device.WarningTemperatureLevel} and {_device.EmergencyTemperatureLevel})");
            _coolingMechanism.On();
            Console.ResetColor();
        }

        public void RunThermostat()
        {
            Console.WriteLine("Thermostat is running...");
            WireUpEventsToEventHandlers();
            _heatSensor.RunHeatSensor();
        }
    }

    public interface IThermostat
    {
        void RunThermostat();
    }

    public interface IDevice
    {
        double WarningTemperatureLevel { get; }
        double EmergencyTemperatureLevel { get; }

        void RunDevice();
        void HandleEmergency();
    }
    public interface IHeatSenser
    {
        event EventHandler<TemperatueEventArgs> TemperatureReachesEmergencyLevelEventHandler;
        event EventHandler<TemperatueEventArgs> TemperatureReachesWarningLevelEventHandler;
        event EventHandler<TemperatueEventArgs> TemperatureFallsBelowWarningLevelEventHandler;

        void RunHeatSensor();
    }

    public interface ICoolingMechanism
    {
        void On();
        void Off();
    }

    public class TemperatueEventArgs : EventArgs
    {
        public double Temperature { get; set; }
        public DateTime CurrentDateTime { get; set; }
    }
}