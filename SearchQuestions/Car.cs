using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchQuestions
{
    class Car
    {
        public Car()
        {
            distancesHistory = new int[120];
            firstDistanceRun = true;
        }
        public int PLID { get; set; }
        public string CName { get; set; }
        public string playerName { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public int oldX { get; set; }
        public int oldY { get; set; }
        public int oldZ { get; set; }

        public int heading { get; set; }

        public int rawSpeed { get; set; }
        public int speed { get; private set; }
        public int speed2 { get; private set; }

        public double distance { get; set; }
        public int distance2 { get; set; }

        public int carDistance { get; set; }

        public int[] distancesHistory { get; private set; }
        private int distHistIndex;
        private bool firstDistanceRun;

        public void CarCalculations()
        {
            if (rawSpeed > 0)
            {
                speed = rawSpeed * 100 / 32768;

                distance += speed;
                speed = speed * 36 / 10;
            }

            if (oldX == 0 && oldY == 0 && oldZ == 0)
            {

            }
            else
            {
                speed2 = (int)Math.Sqrt(((oldX - X) * (oldX - X) + (oldY - Y) * (oldY - Y) + (oldZ - Z) * (oldZ - Z)));
                if (speed2 < 100)
                {
                    distance2 += speed2;
                    AddDistanceHistory(speed2);

                    speed2 = (int)(speed2 * 3.6);
                }
            }
            oldX = X;
            oldY = Y;
            oldZ = Z;
        }

        public int GetDistanceToAnotherCar(Car car)
        {
            return (int)Math.Sqrt(((X - car.X) * (X - car.X) +
                        (Y - car.Y) * (Y - car.Y) +
                        (Z - car.Z) * (Z - car.Z)));
        }

        private void AddDistanceHistory(int distance)
        {
            if (distHistIndex == 120)
            {
                distHistIndex = 0;
                firstDistanceRun = false;
            }
            distancesHistory[distHistIndex] = distance;
            distHistIndex++;

        }

        public double CalculateAVGSpeed30S()
        {
            double distance = 0;
            if (firstDistanceRun)
            {
                for (int i = 0; i < distHistIndex; i++)
                {
                    distance += distancesHistory[i];
                }
                distance = (distance * 3.6) / (distHistIndex / 4);
            }
            else
            {
                for (int i = 0; i < 120; i++)
                {
                    distance += distancesHistory[i];
                }
                distance = (distance * 3.6) / (120 / 4);
            }
            return distance;
        }

        public int GetAngleToAnotherCar(Car car)
        {
            // Might be good to find which is faster and more accurate. Balancing.

            return (int)(Math.Atan2(car.X - X, car.Y - Y) * 180 / 3.1415926535897);

            //const double TWOPI = 6.2831853071795865;
            //const double RAD2DEG = 57.2957795130823209;
            //// if (a1 = b1 and a2 = b2) throw an error 
            //double theta = Math.Atan2(car.X - X, car.Y - Y);
            //if (theta < 0.0)
            //    theta += TWOPI;
            //return (int)(RAD2DEG * theta);
        }
    }
}
