using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Management;
using System.Threading;
using MyCollections;
using Thermo;

namespace MyCollections
{
    public class Test
    {
        // Test the ListWithChangedEvent class.
        public static void Main()
        {
            var impl = new LocalCSVTempEventNotifierImpl();
            new Thermometer(impl, processThresholdAlert,
                new List<Threshold>()
                {
                    new Threshold()
                    {
                        Name = "0 C Both Ways One Time",
                        TargetTemperature = new Temperature(Temperature.TempType.C, 0),
                        OneTime = true
                    }
                });
            impl.getData(@"..\..\sample_data.csv");
        }

        private static void processThresholdAlert(string thresholdName, bool reachedFromAbove)
        {
            var direction = reachedFromAbove ? "above" : "below";
            Console.WriteLine($"The threshold {thresholdName} has been reached from {direction}");
        }
    }
}