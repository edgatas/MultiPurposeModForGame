using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchQuestions
{
    class Parameters
    {
        public Parameters()
        {
            //showPlayerList = false;
            //showCarInfo = false;
            //showDistance = false;
            //showNewOnServer = false;
            //showNewOnTrack = false;
            //showDanger = false;
            playerPitLane = false;
            showGPSToCar = true;
            showDistance = true;
            playerIndexFromList = -1;
        }

        public bool showPlayerList { get; set; }
        public bool showCarInfo { get; set; }
        public bool showDistance { get; set; }
        public bool showNewOnServer { get; set; }
        public bool showNewOnTrack { get; set; }
        public bool showDanger { get; set; }
        public bool showMenu { get; set; }
        public bool showLastCrash { get; set; }
        public bool showGPSToCar { get; set; }

        public int playerIndexFromList { get; set; }
        public bool playerIndexChanged { get; set; }

        public bool sendToPitMode { get; set; }
        public bool playerPitLane { get; set; }
        
        public bool trackPlayerMode { get; set; }
        


        public void sendID(int clickID)
        {
            if (clickID > 99 && clickID < 200)
            {
                playerIndexFromList = clickID;
                if (sendToPitMode == true) { playerPitLane = true; }
                playerIndexChanged = true;
            }

            Console.WriteLine("Getting ID " + clickID);
            switch(clickID)
            {
                case 50:
                    if (showMenu) { showMenu = false; }
                    else { showMenu = true; }
                    break;
                case 52:
                    if (showDanger) { showDanger = false; }
                    else { showDanger = true; }
                    break;
                case 53:
                    if (showNewOnServer) { showNewOnServer = false; }
                    else { showNewOnServer = true; }
                    break;
                case 54:
                    if (showNewOnTrack) { showNewOnTrack = false; }
                    else { showNewOnTrack = true; }
                    break;
                case 55:
                    if (showPlayerList) { showPlayerList = false; }
                    else { showPlayerList = true; }
                    break;
                case 56:
                    if (showLastCrash) { showLastCrash = false; }
                    else { showLastCrash = true; }
                    break;
                case 57:
                    if (showCarInfo) { showCarInfo = false; }
                    else { showCarInfo = true; }
                    break;
                case 58:
                    if (showDistance) { showDistance = false; }
                    else { showDistance = true; }
                    break;
                case 59:
                    if (showGPSToCar) { showGPSToCar = false; }
                    else { showGPSToCar = true; }
                    break;
                case 60:
                    if (sendToPitMode) { sendToPitMode = false; }
                    else { sendToPitMode = true; }
                    break;
                case 61:
                    if (trackPlayerMode) { trackPlayerMode = false; }
                    else { trackPlayerMode = true; }
                    break;
            }
        }
    }
}
