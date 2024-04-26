using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace discrete_math_final_project
{
    internal class EllipticCurveInZMod : EllipticCurve
    {
        #region Constructor
        // base parameter b is set to equal y^2 - x^3 - a*x
        public EllipticCurveInZMod(int n)
        {
            this.X = rand.Next(1, 101);
            this.Y = rand.Next(1, 101);
            this.Mod = n;
            base.A = rand.Next() % n;
            base.B = (int)(Math.Pow(Y, 2) - Math.Pow(X, 3) - A * X);
            //The following line is for testing purposes
            if (!this.ExistsAtPoint(this.X))
            {
                throw new Exception("The function does not exist at point x, and some debugging needs to be done to fix this.");
            }
        }
        #endregion

        #region Properties
        static Random rand = new Random();
        public double Mod = 0;

        public int X;
        public int Y;
        #endregion

        #region Methods
        #endregion
    }
}
