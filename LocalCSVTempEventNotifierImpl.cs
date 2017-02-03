using System;
using System.Linq;
using System.Threading;
using static System.Double;

namespace Thermo
{
    public class LocalCSVTempEventNotifierImpl : ITempEventNotifier
    {
        public Action<Temperature> OnTempChanged { get; set; }

        private void onTempChanged(Temperature t)
        {
            OnTempChanged?.Invoke(t);
        }

        public void getData(string fileName, int timeout = 333)
        {
            var text = System.IO.File.ReadAllText(fileName);
            var d = text.Split(',').Select(tmp =>
            {
                var data = tmp.Split(' ');
                Enum.TryParse(data[1], out Temperature.TempType tempType);
                TryParse(data[0], out double temp);
                return new Temperature(tempType, temp);
            });

            foreach (var temp in d)
            {
                onTempChanged(temp);
                Thread.Sleep(timeout);
            }
        }
    }
}