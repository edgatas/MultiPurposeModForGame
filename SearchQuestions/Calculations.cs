using System.Collections.Generic;
using System;

namespace SearchQuestions
{
    class Calculations
    {
        public Calculations()
        {

        }

        private int DistanceBetweenCars(Car car1, Car car2)
        {
            return (int)Math.Sqrt(((car1.X - car2.X) * (car1.X - car2.X) +
                        (car1.Y - car2.Y) * (car1.Y - car2.Y) +
                        (car1.Z - car2.Z) * (car1.Z - car2.Z)));
        }

        public int[] GetDistanesToCars(List<Car> allCars, Car currentCar)
        {
            int length = allCars.Count;
            int[] distances = new int[length];
            for (int index = 0; index < length; index++)
            {
                distances[index] = DistanceBetweenCars(allCars[index], currentCar);
            }
            return distances;
        }
    }
}
