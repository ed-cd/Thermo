using System;
using System.Linq;
using System.Threading;
using static System.Double;

namespace Thermo
{
    public class LocalCSVTempEventNotifierImpl : ITempEventNotifier
    {
        public Action<Temp> OnTempChanged { get; set; }

        private void onTempChanged(Temp t)
        {
            OnTempChanged?.Invoke(t);
        }

        public void getData(string fileName, int timeout = 333)
        {
            var text = System.IO.File.ReadAllText(fileName);
            var d = text.Split(',').Select(tmp =>
            {
                var data = tmp.Split(' ');
                Enum.TryParse(data[1], out Temp.TempType tempType);
                TryParse(data[0], out double temp);
                return new Temp(tempType, temp);
            });

            foreach (var temp in d)
            {
                onTempChanged(temp);
                Thread.Sleep(timeout);
            }
        }
    }
}