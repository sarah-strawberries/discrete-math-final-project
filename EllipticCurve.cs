using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discrete_math_final_project
{
    internal class EllipticCurve
    {
        #region Constructors
        public EllipticCurve() { }
        #endregion

        #region Properties
        public int A { get; protected set; }
        public int B { get; protected set; }
        #endregion

        #region Methods
        public override string ToString()
        {
            string equation = "y^2 = x^3 ";
            if (A < 0)
            {
                equation += $"- {A}x ";
            }
            else
            {
                equation += $"+ {A}x ";
            }

            if (B < 0)
            {
                equation += $"- {B}";
            }
            else
            {
                equation += $"+ {B}";
            }

            return equation;
        }

        public Tuple<double, double> GetValuesAtPoint(double xPoint)
        {
            if (ExistsAtPoint(xPoint))
            {
                double answer1 = Math.Sqrt(Math.Pow(xPoint, 3) + A * xPoint + B);
                double answer2 = -1 * Math.Sqrt(Math.Pow(xPoint, 3) + A * xPoint + B);
                Tuple<double, double> answers = new Tuple<double, double>(answer1,answer2);
                return answers;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public bool ExistsAtPoint(double xPoint)
        {
            if ((Math.Pow(xPoint, 3) + A * xPoint + B) > double.MaxValue)
            {
                throw new ArgumentException($"Sorry, but the curve cannot be evaluated at point the point x = {xPoint} because of an overflow error.");
                // TODO: There is probably a way to refactor this so that it square-roots the value if possible before setting it to functionAtPoint.
                //       This would provide opportunity for larger x values to be used.
            }

            double functionAtPoint = Math.Pow(xPoint, 3) + A * xPoint + B; // this would be square-rooted to solve for y

            if (functionAtPoint < 0)
            {
                return false; // you can't take the square root of a negative and stay in the set of real numbers
            }
            else
            {
                return true;
            }
        }
        #endregion
    }
}
