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
            distancesHistory = new int[2400];
        }
        public int PLID { get; set; }
        public string CName { get; set; }
        public string playerName { get; set; }

        public double rawX { get; set; }
        public double rawY { get; set; }
        public double rawZ { get; set; }
        private double oldRawX;
        private double oldRawY;
        private double oldRawZ;

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        private int oldX;
        private int oldY;
        private int oldZ;

        public int heading { get; set; }

        public int rawSpeed { get; set; }
        public int speed { get; private set; }
        public int speed2 { get; private set; }

        public double distance { get; set; }
        public int distance2 { private get; set; } // Distance is in milimeters
        public int tickDistance { get; private set; } // Milimeters

        public int carDistance { get; set; }

        public int[] distancesHistory { get; private set; }
        private int distHistIndex;

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
                double tempSpeed = Math.Sqrt(((oldRawX - rawX) * (oldRawX - rawX) + (oldRawY - rawY) * (oldRawY - rawY) + (oldRawZ - rawZ) * (oldRawZ - rawZ)));
                //speed2 = (int)Math.Sqrt(((oldX - X) * (oldX - X) + (oldY - Y) * (oldY - Y) + (oldZ - Z) * (oldZ - Z)));
                if (tempSpeed < 100000)
                {
                    distance2 += (int)tempSpeed;
                    tickDistance = (int)tempSpeed;
                    AddDistanceHistory((int)tempSpeed);

                    speed2 = (int)(tempSpeed * 3.6 * 4 / 1000);
                    //distance2 += speed2;
                    //AddDistanceHistory(speed2);

                    //speed2 = (int)(speed2 * 3.6);
                }
            }
            oldX = X;
            oldY = Y;
            oldZ = Z;

            oldRawX = rawX;
            oldRawY = rawY;
            oldRawZ = rawZ;

        }

        public int GetDistance()
        {
            return distance2 / 1000;
        }

        public int GetDistanceToAnotherCar(Car car)
        {
            return (int)Math.Sqrt(((X - car.X) * (X - car.X) +
                        (Y - car.Y) * (Y - car.Y) +
                        (Z - car.Z) * (Z - car.Z)));
        }

        private void AddDistanceHistory(int distance)
        {
            distancesHistory[distHistIndex] = distance;
            distHistIndex++;
            if (distHistIndex == 2400)
            {
                distHistIndex = 0;
            }
        }

        public void ResetDistances()
        {
            distancesHistory = new int[2400];
            distHistIndex = 0;
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

        public int CalculateAVGSpeed30S()
        {
            double distance = DistanceSum(120);
            return (int)((distance * 3.6) / (120 / 4) / 1000);
        }

        public int CalculateAVGSpeed60S()
        {
            double distance = DistanceSum(240);
            return (int)((distance * 3.6) / (240 / 4) / 1000);
        }

        public int CalculateAVGSpeed120S()
        {
            double distance = DistanceSum(480);
            return (int)((distance * 3.6) / (480 / 4) / 1000);
        }

        public int CalculateAVGSpeed300S()
        {
            double distance = DistanceSum(1200);
            return (int)((distance * 3.6) / (1200 / 4) / 1000);
        }

        public int CalculateAVGSpeed600S()
        {
            double distance = DistanceSum(2400);
            return (int)((distance * 3.6) / (2400 / 4) / 1000);
        }

        private double DistanceSum(int length)
        {
            double sum = 0;
            int arrayIndex = distHistIndex;
            for (int i = 0; i < length; i++)
            {
                if (arrayIndex < 0) { arrayIndex = 2399; }
                sum += distancesHistory[arrayIndex];
                arrayIndex--;
            }
            return sum;
        }
    }
}
