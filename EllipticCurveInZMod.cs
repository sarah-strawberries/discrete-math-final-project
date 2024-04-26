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
        public EllipticCurveInZMod(int n, int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Mod = n;
            base.A = rand.Next() % n;
            base.B = (int)(Math.Pow(y, 2) - Math.Pow(x, 3) - A * x);
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
