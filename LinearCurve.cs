using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discrete_math_final_project
{
    /// <summary>
    /// An oject instantiated from this class represents a linear curve of the form y = mx + b.
    /// </summary>
    internal class LinearCurve
    {
        #region Properties
        /// <summary>
        /// This property indicates the slope of the line.
        /// </summary>
        public double M { get; set; }

        /// <summary>
        /// This property indicates the constant term of the linear equation.
        /// </summary>
        public double B { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// This constructor returns a LinearCurve object representing a curve of the form y = mx + b.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="b"></param>
        public LinearCurve(double m, double b) 
        { 
            this.M = m;
            this.B = b;
        }

        /// <summary>
        /// This constructor returns a LinearCurve object by calculating the slope and constant given two XYPoint objects.
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        public LinearCurve(XYPoint point1, XYPoint point2)
        {
            M = Calculator.GetSlopeBetweenPoints(point1, point2);
            B = -1 * M * point1.X;
        }
        #endregion

        #region Methods
        public double EvaluateAtPoint(double xPoint)
        {
            return M * xPoint + B;
        }


        #endregion
    }
}
