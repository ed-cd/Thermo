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
            var thermometer = new Thermometer(new LocalCSVTempEventNotifierImpl(), (thresholdName, reachedFromAbove) =>
            {

            },new List<Threshold>());                       
            var impl = new LocalCSVTempEventNotifierImpl() {OnTempChanged = Console.WriteLine};
            impl.getData(@"..\..\sample_data.csv");
        }

        public static void  processThresholdAlert(string thresholdName, bool reachedFromAbove)
        {
            
        }
    }
}