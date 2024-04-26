using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discrete_math_final_project
{
    internal class XYPoint
    {
        #region Constructor
        public XYPoint(double x, double y)
        {
            this.X = x; 
            this.Y = y;
        }
        #endregion

        #region Properties
        public double X { get; set; }
        public double Y { get; set; }
        #endregion

        #region Methods
        public void SumWithPoint(Point pointToAdd)
        {
            if ((this.X + pointToAdd.X) > double.MaxValue)
            {
                throw new ArgumentException("The points could not be added because to do so would result in overflow.");
            }
            if ((this.Y + pointToAdd.Y) > double.MaxValue)
            {
                throw new ArgumentException("The points could not be added because to do so would result in overflow.");
            }
            this.X += pointToAdd.X;
            this.Y += pointToAdd.Y;
        }
        #endregion
    }
}
