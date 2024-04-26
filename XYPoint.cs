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
            // TODO: There could be a problem with double overflow in this part, so that would be good to fix
            this.X += pointToAdd.X;
            this.Y += pointToAdd.Y;
        }
        #endregion
    }
}
