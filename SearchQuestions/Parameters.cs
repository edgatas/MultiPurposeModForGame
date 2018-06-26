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
            //playerPitLane = false;
            //showGPSToCar = true;
            //showDistance = true;
            //trackPlayerMode = true;

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

        public bool distancesList { get; set; }
        
        public bool crashHappened { get; set; }

        public bool distanceEventMode { get; set; }
        public bool distanceEventActive { get; set; }


        public int distanceEventID { get; set; }
        public bool distanceEventChanged { get; set; }

        public bool createObject { get; set; }

        public bool displayAverageSpeeds { get; set; }
        public bool resetTimes { get; set; }

        // All events are in here
        public bool showEventMenu { get; set; }

        public bool dragMode { get; set; }
        public bool treasureHuntMode { get; set; }


        // Drag Mode
        public bool dragPickPlayer1 { get; set; }
        public bool dragPickPlayer2 { get; set; }
        public bool dragReady { get; set; }
        public bool dragStarted { get; set; }

        public bool dragPrintPlayer1 { get; set; }
        public bool dragPrintPlayer2 { get; set; }
        public bool dragLights { get; set; }

        public void sendID(int clickID)
        {
            if (clickID > 99 && clickID < 150)
            {
                playerIndexFromList = clickID - 100;
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
                case 15:
                    if (resetTimes) { resetTimes = false; }
                    else { resetTimes = true; }
                    break;
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
                    if (showEventMenu) { if (dragMode) { dragMode = false; } else { dragMode = true; } }
                    break;
                case 53:
                    if (showMenu) { if (showNewOnServer) { showNewOnServer = false; } else { showNewOnServer = true; } }
                    if (showEventMenu) { if (treasureHuntMode) { treasureHuntMode = false; } else { treasureHuntMode = true; } }
                    break;
                case 54:
                    if (showMenu) { if (showNewOnTrack) { showNewOnTrack = false; } else { showNewOnTrack = true; } }
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
                case 66:
                    if (showMenu) { if (createObject) { createObject = false; } else { createObject = true; } }
                    break;
                case 67:
                    if (showMenu) { if (displayAverageSpeeds) { displayAverageSpeeds = false; } else { displayAverageSpeeds = true; } }
                    break;
                case 72:
                    if (dragMode)
                    {
                        dragPickPlayer1 = true;
                        dragPickPlayer2 = false;
                        dragReady = false;
                        dragStarted = false;
                    }
                    break;
                case 73:
                    if (dragMode) { dragPrintPlayer1 = true; }
                    break;
                case 74:
                    if (dragMode) { dragPrintPlayer2 = true; }
                    break;
                case 75:
                    if (dragMode)
                    {
                        if (dragReady)
                        {
                            dragLights = true;
                            dragReady = false;
                            dragStarted = true;
                        }
                    }
                    break;
            }
        }
    }
}
