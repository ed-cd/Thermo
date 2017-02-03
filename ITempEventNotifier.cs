using System;

namespace Thermo
{
    public interface ITempEventNotifier
    {
        Action<Temperature> OnTempChanged { get; set; }
    }
}