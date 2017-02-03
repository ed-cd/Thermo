using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thermo
{
    public class Temperature
    {
        public enum TempType
        {
            C,
            F
        } //potentially more types like K

        private readonly TempType tt;

        public Temperature(TempType tt, double value)
        {
            this.tt = tt;
            Value = value;
        }

        private double value; // everything is internally stored as C

        public double Value
        {
            get
            {
                switch (tt)
                {
                    case TempType.C:
                        return value;
                    case TempType.F:
                        return value * 1.8 + 32;
                    default:
                        throw new ArgumentOutOfRangeException(); //in case K is added, without an implementation
                }
            }
            set
            {
                switch (tt)
                {
                    case TempType.C:
                        this.value = value;
                        break;
                    case TempType.F:
                        this.value = (value - 32) * 0.5556;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(); //in case K is added, without an implementation
                }
            }
        }

        public static bool operator ==(Temperature t1, Temperature t2)
        {
            if (ReferenceEquals(t1, null) && ReferenceEquals(t2, null)) return true;
            if (ReferenceEquals(t1, null) || ReferenceEquals(t2, null)) return false;
            return Math.Abs(Math.Abs(t1.value) - Math.Abs(t2.Value)) < 0.001;
            //good enough epsilon for temperature
        }

        public static bool operator !=(Temperature t1, Temperature t2)
        {
            return !(t1 == t2);
        }

        public static bool operator >(Temperature t1, Temperature t2)
        {
            return t1.value > t2.value;
        }

        public static bool operator <(Temperature t1, Temperature t2)
        {
            return t1.value < t2.value;
        }

        public override string ToString()
        {
            return $"{Value}{tt}";
        }
    }
}