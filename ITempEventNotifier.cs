using System;

namespace Thermo
{
    public interface ITempEventNotifier
    {
        Action<Temp> OnTempChanged { get; set; }
    }
}