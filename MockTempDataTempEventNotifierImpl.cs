using System;
using System.Linq;
using System.Threading;
using static System.Double;

namespace Thermo
{
    public class MockReadGPIOTempDataTempEventNotifierImpl : ITempEventNotifier
    {
        public Action<Temperature> OnTempChanged { get; set; }


        public void getData()
        {
            while (true) //could potentially always be polling a GPIO connected sensor
            {
                // read the file that maps to those GPIO pins
                //process into a temp 
                //OnTempChanged()
            }
        }
    }

    public class MockInternetConnectedSocketTempDataTempEventNotifierImpl : ITempEventNotifier
    {
        public Action<Temperature> OnTempChanged { get; set; }
        
        //Set up a REST Endpoint to recieve tempereture data from an internet enabled sensor
        //when recieving a request, just call OnTempChanged 
    }
}