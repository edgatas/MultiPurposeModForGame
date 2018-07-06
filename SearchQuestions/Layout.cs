using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchQuestions
{
    class Layout
    {
        public int objectX { get; set; }
        public int objectY { get; set; }
        public int objectZ { get; set; }
        public int objectHeading { get; set; }
        public int objectIndex { get; set; }

        public Layout(int X, int Y, int Z, int heading, int index)
        {
            objectX = X;
            objectY = Y;
            objectZ = Z;
            objectHeading = heading;
            objectIndex = index;
        }
    }
}
