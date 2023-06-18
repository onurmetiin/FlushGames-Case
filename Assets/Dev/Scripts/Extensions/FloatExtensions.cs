using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Extension
{
    public static class FloatExtensions
    {
        public static string ToShortFloat(this Single value)
        {
            if (value < 1000)
            {
                return value.ToString();
            }
            if (value >= 1000 && value < 1000000)
            {
                float x = (float)value / (float)1000;

                return $"{ x.ToString("0.0")}K";
            }

            if (value >= 1000000)
            {
                return $"{((float)(value / 1000000)).ToString("0.0")}M";
            }

            return value.ToString("0.0");
        }
    }
}
