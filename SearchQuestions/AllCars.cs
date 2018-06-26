using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchQuestions
{
    class AllCars
    {
        private List<Car> allCars;
        public AllCars()
        {
            allCars = new List<Car>();
        }

        public void NewCar(int newPLID, String newPName, String newCName)
        {
            Car car = new Car { PLID = newPLID, playerName = newPName, CName = newCName };
            allCars.Add(car);
        }

        public List<Car> GetList()
        {
            return allCars;
        }

        public Car GetCarByIndex(int index)
        {
            return allCars[index];
        }

        public Car GetCarByPLID(int PLID)
        {
            foreach(Car car in allCars)
            {
                if (car.PLID == PLID) { return car; }
            }
            return null;
        }
        public int GetCarID(int PLID)
        {
            for (int i = 0; i < allCars.Count; i++)
            {
                if (allCars[i].PLID == PLID)
                {
                    return i;
                }
            }
            return -1;
        }

        public int GetHeading(int PLID)
        {
            {
                for (int i = 0; i < allCars.Count; i++)
                {
                    if (allCars[i].PLID == PLID)
                    {
                        return allCars[i].heading;
                    }
                }
                return -1;
            }
        }

        public void RemoveCarByPLID(int PLID)
        {
            for (int i = 0; i < allCars.Count; i++)
            {
                if (allCars[i].PLID == PLID)
                {
                    allCars.RemoveAt(i);
                }
            }
        }

        public void UpdateCarCoordinates(int PLID, int X, int Y, int Z)
        {
            for (int i = 0; i < allCars.Count; i++)
            {
                if (allCars[i].PLID == PLID)
                {
                    allCars[i].X = X;
                    allCars[i].Y = Y;
                    allCars[i].Z = Z;
                }
            }
        }

        public void UpdateCarRawCoordinates(int PLID, double X, double Y, double Z)
        {
            for (int i = 0; i < allCars.Count; i++)
            {
                if (allCars[i].PLID == PLID)
                {
                    allCars[i].rawX = X;
                    allCars[i].rawY = Y;
                    allCars[i].rawZ = Z;
                }
            }
        }

        public void UpdateCarSpeed(int PLID, int speed)
        {
            for (int i = 0; i < allCars.Count; i++)
            {
                if (allCars[i].PLID == PLID)
                {
                    allCars[i].rawSpeed = speed;
                }
            }
        }

        public void UpdateCarHeading(int PLID, int heading)
        {
            for (int i = 0; i < allCars.Count; i++)
            {
                if (allCars[i].PLID == PLID)
                {
                    allCars[i].heading = heading;
                }
            }
        }

        public void CarCalculations(int PLID)
        {
            {
                for (int i = 0; i < allCars.Count; i++)
                {
                    if (allCars[i].PLID == PLID)
                    {
                        allCars[i].CarCalculations();
                    }
                }
            }
        }


        public void UpdateCarName(int PLID, String CName)
        {
            for (int i = 0; i < allCars.Count; i++)
            {
                if (allCars[i].PLID == PLID)
                {
                    allCars[i].CName = CName;
                }
            }
        }

        public void UpdateCarPlayerName(int PLID, string name)
        {
            for (int i = 0; i < allCars.Count; i++)
            {
                if (allCars[i].PLID == PLID)
                {
                    allCars[i].playerName = name;
                }
            }
        }

        public Car ClosestCar(int PLID)
        {
            int distance = 0;
            bool first = true;
            int carIndex = 0;

            int index = GetCarID(PLID);

            for (int i = 0; i < allCars.Count; i++)
            {
                if (i != index)
                {
                    int testDistance = (int)Math.Sqrt(((allCars[i].X - allCars[index].X) * (allCars[i].X - allCars[index].X) +
                                            (allCars[i].Y - allCars[index].Y) * (allCars[i].Y - allCars[index].Y) +
                                            (allCars[i].Z - allCars[index].Z) * (allCars[i].Z - allCars[index].Z)));

                    if (first)
                    {
                        distance = testDistance;
                        carIndex = i;
                        first = false;
                    }
                    else
                    {
                        if (testDistance < distance)
                        {
                            distance = testDistance;
                            carIndex = i;
                        }
                    }
                }
            }
            return allCars[carIndex];
        }

        private int GetIndexByPLID(int PLID)
        {
            for (int i = 0; i < allCars.Count; i++)
            {
                if (allCars[i].PLID == PLID)
                {
                    return i;
                }
            }
            return -1;
        }

        public void SetDistanceToClosestCar(int PLID, int distance)
        {
            int index = GetIndexByPLID(PLID);
            if (index != -1)
            {
                allCars[index].carDistance = distance;
            }
        }

        public int Length()
        {
            return allCars.Count;
        }
    }
}
