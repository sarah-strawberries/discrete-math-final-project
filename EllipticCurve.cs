using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
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

        bool lineAtPointAIsAboveCurve;
        bool lineAtPointBIsAboveCurve;

        // The following 6 bools are set in the method FigureOutWhetherLineIsAboveCurveAtPoints().
        bool lineToLeftOfPoint1IsAboveCurve;
        bool lineToRightOfPoint1IsAboveCurve;

        bool lineToLeftOfPoint2IsAboveCurve;
        bool lineToRightOfPoint2IsAboveCurve;

        bool point1IsTangentToCurve;
        bool point2IsTangentToCurve;

        #endregion

        #region Methods
        public override string ToString()
        {
            string equation = "y^2 = x^3 ";
            if (A < 0)
            {
                equation += $"- {Math.Abs(A)}x ";
            }
            else
            {
                equation += $"+ {A}x ";
            }

            if (B < 0)
            {
                equation += $"- {Math.Abs(B)}";
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
                throw new ArgumentException($"The curve does not exist at point {xPoint}.");
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

        public XYPoint GetSumOfPoints(XYPoint point1, XYPoint point2)
        {
            XYPoint thirdPoint = GetThirdPointOnCurveGivenTwoPoints(point1, point2);

            Tuple<double, double> yValuesOfCurveAtThirdPointsXValue = this.GetValuesAtPoint(thirdPoint.X);

            // Get the Y value that is not equivalent to thirdPoint's Y value
            if (yValuesOfCurveAtThirdPointsXValue.Item1 != thirdPoint.Y)
            {
                return new XYPoint(thirdPoint.X, yValuesOfCurveAtThirdPointsXValue.Item1);
            }
            else if (yValuesOfCurveAtThirdPointsXValue.Item2 != thirdPoint.Y)
            {
                return new XYPoint(thirdPoint.X, yValuesOfCurveAtThirdPointsXValue.Item2);
            }
            else
            {
                throw new Exception("There is something wrong with the logic such that the two Y values of the equation at the X point are the same.");
            }
        }

        /// <summary>
        /// Make sure you put the points in order of leftmost and lowest to rightmost and highest!!
        /// </summary>
        /// <param name="firstPoint"></param>
        /// <param name="secondPoint"></param>
        /// <returns></returns>
        public static XYPoint GetThirdPointOnCurveGivenTwoPoints(XYPoint firstPoint, XYPoint secondPoint)
        {
            // Find slope between point1 and point2
            double slope = Calculator.GetSlopeBetweenPoints(firstPoint, secondPoint);
            // guess third point using binary search

            // if there isn't one, that means it's tangent and we will just choose different points because I don't
            // want to do implicit differentiation in C#

        }

        public double BinarySearchForExistingXValueBetweenPoints(XYPoint point1, XYPoint point2)
        {
            if (point1.X == point2.X)
            {
                throw new ArgumentException("X values of the two points are the same.");
            }

            LinearCurve lineBetweenPoints = new(point1, point2);

            double greaterX = point1.X > point2.X ? point1.X : point2.X;
            double lesserX = point1.X < point2.X ? point1.X : point2.X;
            double testDistanceFromLesserX = (greaterX - lesserX) / 2;
            double deltaX = testDistanceFromLesserX / 2;
            double testX = lesserX + testDistanceFromLesserX;
            bool testXPointShouldIncrease;
            double differenceA = -1;
            double differenceB = -1;

            FigureOutWhetherLineIsAboveCurveAtPoints(point1, point2, deltaX, lineBetweenPoints);

            while (differenceA != differenceB || /*starting condition --> */(differenceA == differenceB && differenceA == -1))
            {
                // Recursive Loop:

                // ======= STEP 1 =======
                // Measure the distance from the line to the curve on either side of the test point.

                //   1a) Get points to check

                //     Get y values of points on line on either side of test point
                double lineYValueAtPointA = lineBetweenPoints.EvaluateAtPoint(testX - deltaX);
                double lineYValueAtPointB = lineBetweenPoints.EvaluateAtPoint(testX + deltaX);

                //     Get y values of points on curve on either side of test point
                //     Check to make sure the y point being used is the right one of the two possible values since there are 2 possible y values on the curve
                Tuple<double, double> curveValuesAtPointA = GetValuesAtPoint(testX - deltaX);
                double curveYValueAtPointA = PointIsBetweenOtherPoints(curveValuesAtPointA.Item1, point1.Y, point2.Y) ? curveValuesAtPointA.Item1 : curveValuesAtPointA.Item2;

                Tuple<double, double> curveValuesAtPointB= GetValuesAtPoint(testX + deltaX);
                double curveYValueAtPointB = PointIsBetweenOtherPoints(curveValuesAtPointB.Item1, point1.Y, point2.Y) ? curveValuesAtPointB.Item1 : curveValuesAtPointB.Item2;

                //   1b) Adding "sanity check"

                //     This line is for the purpose of determining whether we're moving closer to the correct point (the 3rd, not 1st or 2nd) based on whether the line is above or below the curve
                FigureOutWhetherLineIsAboveCurveAtPointsAandB(lineYValueAtPointA, lineYValueAtPointB,);
                
                //     Get difference between y points by subtracting the smaller from the greater
                //     and see which difference is greater

                //   1c) Calculating the x-distances between the line and the curve
                
                differenceA = Math.Abs(curveYValueAtPointA - lineYValueAtPointA);
                differenceB = Math.Abs(curveYValueAtPointB - lineYValueAtPointB);

                // choose the side where the distance is less
                // but only if it isn't moving towards point 1 or 2.

                // If one of the points is not a tangent
                // adjust the test point
                // keep doing that until...? the distance is equal?
                // if it's equal and the sum of the distances without abs is 0, it is not a point where the line is tangent. Otherwise, it is.
                double testXPoint = lesserX + testDistanceFromLesserX;

            }
        }

        public void FigureOutWhetherLineIsAboveCurveAtPointsAandB(double linePointA, double linePointB, double curvePointA, double curvePointB)
        {
            lineAtPointAIsAboveCurve = linePointA > curvePointA;
            lineAtPointBIsAboveCurve = linePointB > curvePointB;
        }

        public void FigureOutWhetherLineIsAboveCurveToLeftAndRightOfPoint1(double curveAtPointToLeftOfPoint1, double curveAtPointToRightOfPoint1, double lineAtPointToLeftOfPoint1, double lineAtPointToRightOfPoint1)
        {
            lineToLeftOfPoint1IsAboveCurve = lineAtPointToLeftOfPoint1 > curveAtPointToLeftOfPoint1;
            lineToRightOfPoint1IsAboveCurve = lineAtPointToRightOfPoint1 > curveAtPointToRightOfPoint1;

            if (lineToLeftOfPoint1IsAboveCurve && lineToRightOfPoint1IsAboveCurve || !(lineToLeftOfPoint1IsAboveCurve && lineToRightOfPoint1IsAboveCurve))
            {
                point1IsTangentToCurve = true;
            }
            // TODO: add conditionals for if point doesn't exist on curve
        }

        public void FigureOutWhetherLineIsAboveCurveToLeftAndRightOfPoint2(double curveAtPointToLeftOfPoint2, double curveAtPointToRightOfPoint2, double lineAtPointToLeftOfPoint2, double lineAtPointToRightOfPoint2)
        {
            lineToLeftOfPoint2IsAboveCurve = lineAtPointToLeftOfPoint2 > curveAtPointToLeftOfPoint2;
            lineToRightOfPoint2IsAboveCurve = lineAtPointToRightOfPoint2 > curveAtPointToRightOfPoint2;
            // TODO: add conditionals for if point doesn't exist on curve
        }

        public bool PointIsBetweenOtherPoints(double pointToCheck, double pointA, double pointB)
        {
            // Sort points A and B by which one is greater
            double greaterPoint = pointA > pointB ? pointA : pointB;
            double lesserPoint = pointB > pointA ? pointB : pointA;

            // Check to see if pointToCheck is between other points
            return (pointToCheck > lesserPoint && pointToCheck < greaterPoint);
        }

        public void FigureOutWhetherLineIsAboveCurveAtPoints(XYPoint point1, XYPoint point2, double deltaX, LinearCurve lineBetweenPoints)
        {
            // Get points to left and right of point1 on the curve
            try 
            { 
                Tuple<double, double> curveYValuesAtPointToLeftOfPoint1 = GetValuesAtPoint(point1.X - deltaX / 10);
            }
            catch
            {
                // Point
            }

            double curveAtPointToLeftOfPoint1 = curveYValuesAtPointToLeftOfPoint1.Item1 > curveYValuesAtPointToLeftOfPoint1.Item2 ? 
                curveYValuesAtPointToLeftOfPoint1.Item1 : curveYValuesAtPointToLeftOfPoint1.Item2;

            Tuple<double, double> curveYValuesAtPointToRightOfPoint1 = GetValuesAtPoint(point1.X + deltaX / 10);

            double curveAtPointToRightOfPoint1 = curveYValuesAtPointToRightOfPoint1.Item1 > curveYValuesAtPointToRightOfPoint1.Item2 ?
                curveYValuesAtPointToRightOfPoint1.Item1 : curveYValuesAtPointToRightOfPoint1.Item2; ;

            // Perform checks

            FigureOutWhetherLineIsAboveCurveToLeftAndRightOfPoint1(curveAtPointToLeftOfPoint1, curveAtPointToRightOfPoint1, lineBetweenPoints.EvaluateAtPoint(point1.X - deltaX / 10), lineBetweenPoints.EvaluateAtPoint(point1.X + deltaX / 10));

            // Get points to left and right of point2 on the curve
            Tuple<double, double> curveYValuesAtPointToLeftOfPoint2 = GetValuesAtPoint(point2.X - deltaX / 10);

            double curveAtPointToLeftOfPoint2 = curveYValuesAtPointToLeftOfPoint2.Item1 > curveYValuesAtPointToLeftOfPoint2.Item2 ?
                curveYValuesAtPointToLeftOfPoint2.Item1 : curveYValuesAtPointToLeftOfPoint2.Item2;

            Tuple<double, double> curveYValuesAtPointToRightOfPoint2 = GetValuesAtPoint(point2.X + deltaX / 10);

            double curveAtPointToRightOfPoint2 = curveYValuesAtPointToRightOfPoint2.Item1 > curveYValuesAtPointToRightOfPoint2.Item2 ?
                curveYValuesAtPointToRightOfPoint2.Item1 : curveYValuesAtPointToRightOfPoint2.Item2; ;

            // Perform checks
            FigureOutWhetherLineIsAboveCurveToLeftAndRightOfPoint2(curveAtPointToLeftOfPoint2, curveAtPointToRightOfPoint2, lineBetweenPoints.EvaluateAtPoint(point2.X - deltaX / 10), lineBetweenPoints.EvaluateAtPoint(point2.X + deltaX / 10));

        }
        #endregion
    }
}