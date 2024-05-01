using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discrete_math_final_project
{
    internal class Calculator
    {
        public static double GetSlopeBetweenPoints(XYPoint x1y1, XYPoint x2y2)
        {
            double rise = x2y2.Y - x1y1.Y;
            double run = x2y2.X - x1y1.X;

            return rise / run;
        }

    }
}
