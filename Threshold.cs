namespace Thermo
{
    public class Threshold
    {
        public string Name { get; set; }
        public Temp TargetTemperature;
        public bool TriggerOnDecreasingTemperature { get; set; } = true;
        public bool TriggerOnIncreasingTemperature { get; set; } = true;
        public bool OneTime { get; set; }
        public bool Reached { get; set; }
    }
}