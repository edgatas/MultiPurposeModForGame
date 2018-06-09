using System.Collections.Generic;
using System;

namespace SearchQuestions
{
    class Calculations
    {
        public Calculations()
        {

        }

        private UInt16 DistanceBetweenCars(Car car1, Car car2)
        {
            return (UInt16)Math.Sqrt(((car1.X - car2.X) * (car1.X - car2.X) +
                        (car1.Y - car2.Y) * (car1.Y - car2.Y) +
                        (car1.Z - car2.Z) * (car1.Z - car2.Z)));
        }

        public UInt16[] GetDistanesToCars(List<Car> allCars, Car currentCar)
        {
            int length = allCars.Count;
            UInt16[] distances = new UInt16[length];
            for (int index = 0; index < length; index++)
            {
                distances[index] = DistanceBetweenCars(allCars[index], currentCar);
            }
            return distances;
        }

        public int GetClosestIndex(UInt16[] distances)
        {
            int length = distances.Length;
            int distance = 10000;
            int index = -1;

            for (int i = 0; i < length; i++)
            {
                if (distance > distances[i] && distances[i] != 0)
                {
                    distance = distances[i];
                    index = i;
                }
            }
            return index;
        }
    }
}
