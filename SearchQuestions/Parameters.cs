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
            trackPlayerMode = true;
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

        public bool distancesList { get; set; }
        
        public bool crashHappened { get; set; }

        public bool distanceEventMode { get; set; }
        public bool distanceEventActive { get; set; }


        public int distanceEventID { get; set; }
        public bool distanceEventChanged { get; set; }



        // All events are in here
        public bool showEventMenu { get; set; }
        public bool eventMode { get; set; }

        public bool dragLights { get; set; }

        public void sendID(int clickID)
        {
            if (clickID > 99 && clickID < 150)
            {
                playerIndexFromList = clickID;
                if (sendToPitMode == true) { playerPitLane = true; }
                playerIndexChanged = true;
            }

            if (clickID >= 150 && clickID < 200)
            {
                distanceEventID = clickID - 150;
                distanceEventChanged = true;
            }

            Console.WriteLine("Getting ID " + clickID);
            switch(clickID)
            {
                case 40:
                    if (showMenu) { showMenu = false; }
                    else { showMenu = true; }
                    showEventMenu = false;
                    break;
                case 41:
                    if (showEventMenu) { showEventMenu = false; }
                    else { showEventMenu = true; }
                    showMenu = false;
                    break;
                case 52:
                    if (showMenu) { if (showDanger) { showDanger = false; } else { showDanger = true; } }
                    if (showEventMenu) { if (eventMode) { eventMode = false; } else { eventMode = true; } }
                    break;
                case 53:
                    if (showMenu) { if (showNewOnServer) { showNewOnServer = false; } else { showNewOnServer = true; } }
                    if (showEventMenu) { if (dragLights) { dragLights = false; } else { dragLights = true; } }
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
                case 62:
                    if (dragLights) { dragLights = false; }
                    else { dragLights = true; }
                    break;
                case 63:
                    if (distancesList) { distancesList = false; }
                    else { distancesList = true; }
                    break;
                case 64:
                    if (distanceEventMode) { distanceEventMode = false; }
                    else { distanceEventMode = true; }
                    break;
                case 65:
                    if (distanceEventActive) { distanceEventActive = false; }
                    else { distanceEventActive = true; }
                    break;
            }
        }
    }
}
