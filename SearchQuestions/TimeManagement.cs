using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchQuestions
{
    class TimeManagement
    {
        Stopwatch clock;
        private int startTimeStamp { get; set; }
        private long ElapsedTime;
        private int ElapsedIndex;
        public bool Elapsed30 { get; private set; }
        public bool Elapsed60 { get; private set; }
        public bool Elapsed120 { get; private set; }
        public bool Elapsed300 { get; private set; }
        public bool Elapsed600 { get; private set; }

        public TimeManagement()
        {
            clock = new Stopwatch();
            clock.Start();
        }

        public void Execute()
        {
            ElapsedTime += clock.ElapsedMilliseconds;
            clock.Restart();

            if (ElapsedTime > 30000)
            {
                ElapsedIndex++;
                ElapsedTime -= 30000;
                SetConditions();
            }
        }

        private void SetConditions()
        {
            Elapsed30 = true;
            if (ElapsedIndex % 2 == 0) { Elapsed60 = true; }
            if (ElapsedIndex % 4 == 0) { Elapsed120 = true; }
            if (ElapsedIndex % 10 == 0) { Elapsed300 = true; }
            if (ElapsedIndex % 20 == 0) { Elapsed600 = true; ElapsedIndex = 0; }
        }

        public void ResetConditions()
        {
            Elapsed30 = false;
            Elapsed60 = false;
            Elapsed120 = false;
            Elapsed300 = false;
            Elapsed600 = false;
        }
    }
}
