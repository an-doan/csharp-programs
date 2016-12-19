//Created by An Doan

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment1Better
{
    class Program
    {
        /// <summary>
        /// Prompts the user for x and y values of two points
        /// Finds the distance and angle between two points
        /// prints out the distance and angle to the console
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome! This program will find the distance and angle between points 1 and 2.");

            //Prompts the user for the x and y values of the two points
            Console.Write("Point 1 X: ");
            float point1X = float.Parse(Console.ReadLine());

            Console.Write("Point 1 Y: ");
            float point1Y = float.Parse(Console.ReadLine());

            Console.Write("Point 2 X: ");
            float point2X = float.Parse(Console.ReadLine());

            Console.Write("Point 2 Y: ");
            float point2Y = float.Parse(Console.ReadLine());

            //calculate the difference (delta) of the two y values and the two x values
            float deltaY = point2Y - point1Y;
            float deltaX = point2X - point1X;

            //use calculateDistance method to find the distance
            float distance = calculateDistance(deltaY, deltaX);

            //use calculateAngle method to find angle
            float angle = calculateAngle(deltaY, deltaX);

            //Prints out the distance and the angle to the console
            Console.WriteLine("Distance between points: " + distance.ToString("F3"));
            Console.WriteLine("Angle between points: " + angle.ToString("F3") + " degrees");
            Console.WriteLine();
        }

        /// <summary>
        /// calculate the distance between the two points using Pythagorean Theorem (a^2 + b^2 = c^2)
        /// </summary>
        /// <param name="deltaY">The difference between the y values of the two points</param>
        /// <param name="deltaX">The difference between the x values of the two points</param>
        /// <returns>the shortest distance between the two points</returns>
        static float calculateDistance(float deltaY, float deltaX)
        {
            //returning the distance using Pythagorean Theorem
            return (float)Math.Sqrt(Math.Pow(deltaY, 2) + Math.Pow(deltaX, 2));
        }

        /// <summary>
        /// calculate the angle needed to move from point 1 to point 2 using the Aten2 method
        /// </summary>
        /// <param name="deltaY">The difference between the y values of the two points</param>
        /// <param name="deltaX">The difference between the x values of the two points</param>
        /// <returns>the angle between two points in degrees</returns>
        static float calculateAngle(float deltaY, float deltaX)
        {
            //using the Aten2 method to calculate angle in radians
            float angleInRadians = (float)Math.Atan2(deltaY, deltaX);

            //convert the angle measure from radians to degrees and return it
            return angleInRadians * (float)(180 / Math.PI);
        }
    }
}