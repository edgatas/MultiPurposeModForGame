using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchQuestions.Systems
{
    class CarAhead
    {
        public UInt16[] carHeadingsDiff { get; private set; }
        public SByte[] warningColors { get; private set; }

        public CarAhead()
        {
            carHeadingsDiff = new UInt16[50];
            warningColors = new SByte[50];
        }

        public void Execute(AllCars allCars, int activePLID)
        {
            int heading = allCars.GetCarByPLID(activePLID).heading;
            int AmmountOfCars = allCars.Length();
            carHeadingsDiff = new UInt16[AmmountOfCars];

            for (int index = 0; index < AmmountOfCars; index++)
            {
                carHeadingsDiff[index] = (UInt16) Math.Abs(heading - allCars.GetCarByIndex(index).heading);
                int distance = allCars.GetCarByPLID(activePLID).GetDistanceToAnotherCar(allCars.GetCarByIndex(index));

                warningColors[index] = 9;
                if (carHeadingsDiff[index] < 270 && carHeadingsDiff[index] > 90 && distance < 300) { warningColors[index] = 3; }
                if (carHeadingsDiff[index] < 210 && carHeadingsDiff[index] > 150 && distance < 300) { warningColors[index] = 1; }

            }

            //if (parameters.showDanger) { buttons.DangerAhead(_inSim, color); } else { buttons.DangerAheadClear(_inSim); }
        }
    }
}
