///Created by An Doan

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment1
{
    class Program
    {
       0 /// <summary>
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

            //calculate the distance between the two points using Pythagorean Theorem (a^2 + b^2 = c^2)
            float distance = (float) Math.Sqrt(Math.Pow(deltaY, 2) + Math.Pow(deltaX, 2));

            //calculate the angle needed to move from point 1 to point 2 using the Aten2 method
            float angleInRadians = (float)Math.Atan2(deltaY, deltaX);

            //convert the angle measure from radians to degrees
            float angleInDegrees = angleInRadians * (float) (180 / Math.PI);

            //Prints out the distance and the angle to the console
            Console.WriteLine("Distance between points: " + distance.ToString("F3"));
            Console.WriteLine("Angle between points: " + angleInDegrees.ToString("F3") + " degrees");
            Console.WriteLine();
        }
    }
}
