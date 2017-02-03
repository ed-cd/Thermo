using System;
using System.Collections.Generic;

namespace Thermo
{
    public class Thermometer
    {
        private readonly ITempEventNotifier tempEventNotifier;
        private readonly Action<string, bool> thresholdReachedAction;
        private Temp currentTemperature;
        public List<Threshold> Thresholds { get; set; }

        public Thermometer(ITempEventNotifier tempEventNotifier,Action<string, bool> thresholdReachedAction, List<Threshold> thresholds)
        {            
            this.tempEventNotifier = tempEventNotifier;
            this.tempEventNotifier.OnTempChanged = processTempData;
            this.thresholdReachedAction = thresholdReachedAction;
            Thresholds = thresholds;
        }

        private void processTempData(Temp newTemperature)
        {
            if (currentTemperature == null) currentTemperature = newTemperature;
            Thresholds.ForEach(threshold =>
            {
                
                if (newTemperature == threshold.TargetTemperature && !threshold.Reached)
                {
                    if (newTemperature == currentTemperature) return;//temp unchanged
                    if (threshold.TriggerOnDecreasingTemperature && newTemperature < currentTemperature)
                    {
                        thresholdReachedAction.Invoke(threshold.Name,true);
                        threshold.Reached = true;
                        return;
                    }
                    if (threshold.TriggerOnIncreasingTemperature && newTemperature > currentTemperature)
                    {
                        thresholdReachedAction(threshold.Name, false);
                        threshold.Reached = true;
                    }
                }
            });
        }
    }
}