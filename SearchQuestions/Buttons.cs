using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InSimDotNet.Packets;

namespace SearchQuestions
{
    class Buttons
    {

        private int numberOfLines;
        bool first;

        public Buttons()
        {
            first = true;
        }

        // 001 - "Closest Car name and distance"t

        // 003 - "Danger Button"
        // 004 - Newest player on Server Button
        // 005 - Newest player on Track Button
        // 006 - Latest Crash Button
        // 007 - 013 - Car info Buttons
        // 014 - GPS to Car Button
        // 015 - 020 - Average Speeds

        // 050 - 099  Menu

        // 040 - Menu On Off
        // 041 - Event Menu On Off
        // 042 - Drag Menu

        // Main Menu Indexes 51 - 70
        // 051 - Menu Layout Empty
        // 052 - Car ahead warning "Danger" On Off
        // 053 - Newest Player in Server On Off
        // 054 - Newest Player on Track On Off
        // 055 - List of Player on Track On Off
        // 056 - Last Crash On Off
        // 057 - Car info On Off
        // 060 - Closest car to player On Off
        // 059 - GPSToCar On Off
        // 060 - Send to pit lane mode On Off
        // 061 - Track Player Mode On Off
        // 062 - Turn on drag lights On Off
        // 063 - Show distance to all players On Off
        // 064 - Distance event mode On Off
        // 065 - Distance event run On Off
        // 066 - Turn on layout loop On Off
        // 067 - Turn on Average Speeds On Off

        // Event Menu Indexes
        // 051 - Event Menu Layout
        // 052 - Drag Menu On Off
        // 053 - Treasure Hunt On Off


        // Drag Menu Indexes
        // 070 - Drag Menu Layout
        // 071 - Drag Instructions
        // 072 - Drag New Drag
        // 073 - Player 1
        // 074 - Player 2
        // 075 - Start Drag


        // 099 - "Player On Track"
        // 100 - 150 "List of Players"
        // 150 - 200 "Lost of Distances"


        public void MenuOnOff(InSimDotNet.InSim _inSim, Parameters parameters)
        {
            ButtonStyles tempBStyle;
            if (parameters.showMenu)
            {
                tempBStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_CLICK;
            }
            else
            {
                tempBStyle = (byte)2 + ButtonStyles.ISB_CLICK; // Got to find what that 2 means.
            }

            _inSim.Send(
                new IS_BTN
                {
                    BStyle = tempBStyle,
                    ClickID = 40,
                    UCID = 0,
                    ReqI = 40,
                    L = 157,
                    W = 8,
                    T = 0,
                    H = 5,
                    Text = "^3Meniu",
                    Inst = 128
                    
                }
            );
        }

        public void MenuMain(InSimDotNet.InSim _inSim, Parameters parameters)
        {

            _inSim.Send(
                new IS_BTN
                {
                    BStyle = ButtonStyles.ISB_DARK,
                    ClickID = 51,
                    UCID = 0,
                    ReqI = 51,
                    L = 50,
                    W = 100,
                    T = 50,
                    H = 55,
                    Text = ""
                }
            );

            String tempText;
            if (parameters.showDanger)
            {
                tempText = "Incoming Car Warning ^2On";
            }
            else
            {
                tempText = "Incoming Car Warning ^1Off";
            }

            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_CLICK,
                    ClickID = 52,
                    UCID = 0,
                    ReqI = 52,
                    L = 50,
                    W = 50,
                    T = 50,
                    H = 5,
                    Text = tempText
                }
            );

            if (parameters.showNewOnServer)
            {
                tempText = "Newest Player on Server: ^2On";
            }
            else
            {
                tempText = "Newest Player on Server: ^1Off";
            }

            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_CLICK,
                    ClickID = 53,
                    UCID = 0,
                    ReqI = 53,
                    L = 50,
                    W = 50,
                    T = 55,
                    H = 5,
                    Text = tempText
                }
            );

            if (parameters.showNewOnTrack)
            {
                tempText = "Newest Player on Track: ^2On";
            }
            else
            {
                tempText = "Newest Player on Track: ^1Off";
            }

            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_CLICK,
                    ClickID = 54,
                    UCID = 0,
                    ReqI = 54,
                    L = 50,
                    W = 50,
                    T = 60,
                    H = 5,
                    Text = tempText
                }
            );

            if (parameters.showPlayerList)
            {
                tempText = "List of Player on Track: ^2On";
            }
            else
            {
                tempText = "List of Player on Track: ^1Off";
            }

            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_CLICK,
                    ClickID = 55,
                    UCID = 0,
                    ReqI = 55,
                    L = 50,
                    W = 50,
                    T = 65,
                    H = 5,
                    Text = tempText
                }
            );

            if (parameters.showLastCrash)
            {
                tempText = "Last Crash: ^2On";
            }
            else
            {
                tempText = "Last Crash: ^1Off";
            }
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_CLICK,
                    ClickID = 56,
                    UCID = 0,
                    ReqI = 56,
                    L = 50,
                    W = 50,
                    T = 70,
                    H = 5,
                    Text = tempText
                }
            );

            if (parameters.showCarInfo)
            {
                tempText = "Car info: ^2On";
            }
            else
            {
                tempText = "Car info: ^1Off";
            }
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_CLICK,
                    ClickID = 57,
                    UCID = 0,
                    ReqI = 57,
                    L = 50,
                    W = 50,
                    T = 75,
                    H = 5,
                    Text = tempText
                }
            );

            if (parameters.showDistance)
            {
                tempText = "Closest Car: ^2On";
            }
            else
            {
                tempText = "Closest Car: ^1Off";
            }
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_CLICK,
                    ClickID = 58,
                    UCID = 0,
                    ReqI = 58,
                    L = 50,
                    W = 50,
                    T = 80,
                    H = 5,
                    Text = tempText
                }
            );

            if (parameters.showGPSToCar)
            {
                tempText = "GPS to Car: ^2On";
            }
            else
            {
                tempText = "GPS to Car: ^1Off";
            }
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_CLICK,
                    ClickID = 59,
                    UCID = 0,
                    ReqI = 59,
                    L = 50,
                    W = 50,
                    T = 85,
                    H = 5,
                    Text = tempText
                }
            );

            if (parameters.sendToPitMode)
            {
                tempText = "Send Pit Lane Mode: ^2On";
            }
            else
            {
                tempText = "Send Pit Lane Mode: ^1Off";
            }
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_CLICK,
                    ClickID = 60,
                    UCID = 0,
                    ReqI = 60,
                    L = 50,
                    W = 50,
                    T = 90,
                    H = 5,
                    Text = tempText
                }
            );

            if (parameters.trackPlayerMode)
            {
                tempText = "Track Player Mode: ^2On";
            }
            else
            {
                tempText = "Track Player Mode: ^1Off";
            }
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_CLICK,
                    ClickID = 61,
                    UCID = 0,
                    ReqI = 61,
                    L = 50,
                    W = 50,
                    T = 95,
                    H = 5,
                    Text = tempText
                }
            );

            if (parameters.distancesList)
            {
                tempText = "Show distances: ^2On";
            }
            else
            {
                tempText = "Show distances: ^1Off";
            }
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_CLICK,
                    ClickID = 63,
                    UCID = 0,
                    ReqI = 63,
                    L = 100,
                    W = 50,
                    T = 50,
                    H = 5,
                    Text = tempText
                }
            );

            if (parameters.distanceEventMode)
            {
                tempText = "Distance event mode: ^2On";
            }
            else
            {
                tempText = "Distance event mode: ^1Off";
            }
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_CLICK,
                    ClickID = 64,
                    UCID = 0,
                    ReqI = 64,
                    L = 100,
                    W = 50,
                    T = 55,
                    H = 5,
                    Text = tempText
                }
            );

            if (parameters.distanceEventActive)
            {
                tempText = "Distance event active: ^2On";
            }
            else
            {
                tempText = "Distance event active: ^1Off";
            }
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_CLICK,
                    ClickID = 65,
                    UCID = 0,
                    ReqI = 65,
                    L = 100,
                    W = 50,
                    T = 60,
                    H = 5,
                    Text = tempText
                }
            );

            if (parameters.createObject)
            {
                tempText = "Create Object: ^2On";
            }
            else
            {
                tempText = "Create Object: ^1Off";
            }
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_CLICK,
                    ClickID = 66,
                    UCID = 0,
                    ReqI = 66,
                    L = 100,
                    W = 50,
                    T = 65,
                    H = 5,
                    Text = tempText
                }
            );

            if (parameters.displayAverageSpeeds)
            {
                tempText = "Show Average Speeds: ^2On";
            }
            else
            {
                tempText = "Show Average Speeds: ^1Off";
            }
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_CLICK,
                    ClickID = 67,
                    UCID = 0,
                    ReqI = 67,
                    L = 100,
                    W = 50,
                    T = 70,
                    H = 5,
                    Text = tempText
                }
            );
        }

        public void MenuMainClear(InSimDotNet.InSim _inSim)
        {
            _inSim.Send(
                new IS_BFN
                {
                    ReqI = 0,
                    SubT = ButtonFunction.BFN_DEL_BTN,
                    UCID = 0,
                    ClickID = 51,
                    ClickMax = 69
                }
            );
        }

        public void EventMenu(InSimDotNet.InSim _inSim, Parameters parameters)
        {
            ButtonStyles tempBStyle;
            if (parameters.showEventMenu)
            {
                tempBStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_CLICK;
            }
            else
            {
                tempBStyle = (byte)2 + ButtonStyles.ISB_CLICK; // Got to find what that 2 means.
            }

            _inSim.Send(
                new IS_BTN
                {
                    BStyle = tempBStyle,
                    ClickID = 41,
                    UCID = 0,
                    ReqI = 41,
                    L = 165,
                    W = 8,
                    T = 0,
                    H = 5,
                    Text = "^3Events",
                    Inst = 128

                }
            );
        }

        public void ShowEventMenu(InSimDotNet.InSim _inSim, Parameters parameters)
        {
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = ButtonStyles.ISB_DARK,
                    ClickID = 51,
                    UCID = 0,
                    ReqI = 51,
                    L = 50,
                    W = 50,
                    T = 50,
                    H = 55,
                    Text = ""
                }
            );

            String tempText;

            if (parameters.dragMode)
            {
                tempText = "Drag Menu: ^2On";
            }
            else
            {
                tempText = "Drag Menu: ^1Off";
            }
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_CLICK,
                    ClickID = 52,
                    UCID = 0,
                    ReqI = 52,
                    L = 50,
                    W = 50,
                    T = 50,
                    H = 5,
                    Text = tempText
                }
            );


            if (parameters.treasureHuntMode)
            {
                tempText = "Treasure Hunt Menu: ^2On";
            }
            else
            {
                tempText = "Treasure Hunt Menu: ^1Off";
            }
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_CLICK,
                    ClickID = 53,
                    UCID = 0,
                    ReqI = 53,
                    L = 50,
                    W = 50,
                    T = 55,
                    H = 5,
                    Text = tempText
                }
            );
        }


        public void ShowDragMenu(InSimDotNet.InSim _inSim, Parameters parameters, string player1, string player2, int dragDistance)
        {
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = ButtonStyles.ISB_DARK,
                    ClickID = 70,
                    UCID = 0,
                    ReqI = 70,
                    L = 0,
                    W = 30,
                    T = 30,
                    H = 30,
                    Text = ""
                }
            );

            String tempText;

            tempText = "^7Drag Menu";
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_CLICK,
                    ClickID = 71,
                    UCID = 0,
                    ReqI = 71,
                    L = 0,
                    W = 30,
                    T = 30,
                    H = 5,
                    Text = tempText
                }
            );

            tempText = "^2New Drag";
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_CLICK,
                    ClickID = 72,
                    UCID = 0,
                    ReqI = 72,
                    L = 0,
                    W = 30,
                    T = 35,
                    H = 5,
                    Text = tempText
                }
            );

            tempText = "^7Player 1: " + player1;
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + (byte)ButtonStyles.ISB_CLICK + ButtonStyles.ISB_LEFT,
                    ClickID = 73,
                    UCID = 0,
                    ReqI = 73,
                    L = 0,
                    W = 30,
                    T = 40,
                    H = 5,
                    Text = tempText
                }
            );

            tempText = "^7Player 2: " + player2;
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + (byte)ButtonStyles.ISB_CLICK + ButtonStyles.ISB_LEFT,
                    ClickID = 74,
                    UCID = 0,
                    ReqI = 74,
                    L = 0,
                    W = 30,
                    T = 45,
                    H = 5,
                    Text = tempText
                }
            );

            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + (byte)ButtonStyles.ISB_CLICK + ButtonStyles.ISB_LEFT,
                    ClickID = 75,
                    UCID = 0,
                    ReqI = 75,
                    L = 0,
                    W = 15,
                    T = 50,
                    H = 5,
                    Text = Enums.LFSColors.WHITE + "Atstumas:"
                }
            );

            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + (byte)ButtonStyles.ISB_CLICK + ButtonStyles.ISB_LEFT,
                    TypeIn = 7, // Up to 100 kilometers - 1 milimeter
                    ClickID = 76,
                    UCID = 0,
                    ReqI = 76,
                    L = 15,
                    W = 15,
                    T = 50,
                    H = 5,
                    Text = "\0" + "Įrašyti naują atstumą (milimetrais)" + "\0" + Enums.LFSColors.WHITE + dragDistance.ToString()
                }
            );

            if (parameters.dragReady) { tempText = Enums.LFSColors.GREEN; }
                else { tempText = Enums.LFSColors.RED; }

            tempText += "Start Drag";
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_CLICK,
                    ClickID = 77,
                    UCID = 0,
                    ReqI = 77,
                    L = 0,
                    W = 30,
                    T = 55,
                    H = 5,
                    Text = tempText
                }
            );

            tempText = "^7Status: ";

            if (parameters.dragPickPlayer1) { tempText += "^3Pick Player 1"; }
            if (parameters.dragPickPlayer2) { tempText += "^3Pick Player 2"; }
            if (parameters.dragReady) { tempText += "^2Drag Ready"; }
            if (parameters.dragStarted) { tempText += "^3Drag Lights"; }
            if (parameters.dragRunning) { tempText += "^1Drag Running"; }

            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_LEFT,
                    ClickID = 78,
                    UCID = 0,
                    ReqI = 78,
                    L = 0,
                    W = 30,
                    T = 60,
                    H = 5,
                    Text = tempText
                }
            );
        }

        public void DangerAhead(InSimDotNet.InSim _inSim, String color)
        {
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = ButtonStyles.ISB_DARK,
                    ClickID = 3,
                    UCID = 0,
                    ReqI = 3,
                    L = 75,
                    W = 50,
                    T = 30,
                    H = 10,
                    Text = color + "Danger"
                }
            );
        }

        public void DangerAheadClear(InSimDotNet.InSim _inSim)
        {
            _inSim.Send(
                new IS_BFN
                {
                    ReqI = 0,
                    SubT = ButtonFunction.BFN_DEL_BTN,
                    UCID = 0,
                    ClickID = 3
                }
            );
        }


        public void NewestOnServer(InSimDotNet.InSim _inSim, String text)
        {
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = ButtonStyles.ISB_DARK,
                    ClickID = 4,
                    UCID = 0,
                    ReqI = 4,
                    L = 150,
                    W = 50,
                    T = 85,
                    H = 5,
                    Text = "Newest Player On Server: " + text
                }
            );
        }

        public void NewestOnServerClear(InSimDotNet.InSim _inSim)
        {
            _inSim.Send(
                new IS_BFN
                {
                    ReqI = 0,
                    SubT = ButtonFunction.BFN_DEL_BTN,
                    UCID = 0,
                    ClickID = 4
                }
            );
        }

        public void NewestOnTrack(InSimDotNet.InSim _inSim, String text)
        {
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = ButtonStyles.ISB_DARK,
                    ClickID = 5,
                    UCID = 0,
                    ReqI = 5,
                    L = 150,
                    W = 50,
                    T = 95,
                    H = 5,
                    Text = "Newest Player On Track: " + text
                }
            );
        }

        public void NewestOnTrackClear(InSimDotNet.InSim _inSim)
        {
            _inSim.Send(
                new IS_BFN
                {
                    ReqI = 0,
                    SubT = ButtonFunction.BFN_DEL_BTN,
                    UCID = 0,
                    ClickID = 5
                }
            );
        }

        public void ClosestCar(InSimDotNet.InSim _inSim, String name, int distance)
        {
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_LEFT,
                    ClickID = 1,
                    UCID = 0,
                    ReqI = 1,
                    L = 150,
                    W = 25,
                    T = 150,
                    H = 5,
                    Text = "Distance to " + name + ": ^8" + distance
                }
            );
        }

        public void ClosestCarClear(InSimDotNet.InSim _inSim)
        {
            _inSim.Send(
                new IS_BFN
                {
                    ReqI = 0,
                    SubT = ButtonFunction.BFN_DEL_BTN,
                    UCID = 0,
                    ClickID = 1,
                    ClickMax = 2
                }
            );
        }

        public void CarList(InSimDotNet.InSim _inSim, List<Car> allCars, Parameters parameters)
        {
            if (first) { numberOfLines = allCars.Count; first = false; }

            int difference = numberOfLines - allCars.Count;
            if (difference > 0)
            {
                _inSim.Send(
                    new IS_BFN
                    {
                        ReqI = 0,
                        SubT = ButtonFunction.BFN_DEL_BTN,
                        UCID = 0,
                        ClickID = 100,
                        ClickMax = 150
                    }
                );
            }

            numberOfLines = allCars.Count;

            _inSim.Send(
                new IS_BTN
                {
                    BStyle = ButtonStyles.ISB_DARK,
                    ClickID = 99,
                    UCID = 0,
                    ReqI = 99,
                    L = 0,
                    W = 25,
                    T = 115,
                    H = 5,
                    Text = "On Track (" + allCars.Count + "):"
                }
            );

            ButtonStyles buttonType;
            // When player clicks the button, cars stops to accelerate when using mouse.
            // This is to make buttons unclickable so they wouldn't bother mouse drivers, when not needed.
            if (parameters.sendToPitMode || parameters.trackPlayerMode || parameters.dragMode)
            {
                buttonType = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_CLICK;
            }
            else
            {
                buttonType = ButtonStyles.ISB_DARK;
            }

            // Reserving 50 players for now. Click ID from 100 to 150, ID 100 to 150
            for (int i = 0; i < allCars.Count; i++)
            {
                _inSim.Send(
                    new IS_BTN
                    {
                        BStyle = buttonType,
                        ClickID = (byte)(100 + i),
                        UCID = 0,
                        ReqI = (byte)(100 + 1),
                        L = 0,
                        W = 25,
                        T = (byte)(120 + 4 * i),
                        H = 4,
                        Text = allCars[i].playerName
                    }
                );
            }
        }

        public void CarListClear(InSimDotNet.InSim _inSim)
        {
            _inSim.Send(
                new IS_BFN
                {
                    ReqI = 0,
                    SubT = ButtonFunction.BFN_DEL_BTN,
                    UCID = 0,
                    ClickID = 99,
                    ClickMax = 150
                }
            );
        }

        public void DistanesToCars(InSimDotNet.InSim _inSim, UInt16[] distances)
        {
            _inSim.Send(
                new IS_BFN
                {
                    ReqI = 0,
                    SubT = ButtonFunction.BFN_DEL_BTN,
                    UCID = 0,
                    ClickID = 150,
                    ClickMax = 200
                }
            );

            for (int i = 0; i < distances.Length; i++)
            {
                _inSim.Send(
                    new IS_BTN
                    {
                        // Adding click for an event
                        BStyle = (byte)ButtonStyles.ISB_DARK + (byte)ButtonStyles.ISB_LEFT + ButtonStyles.ISB_CLICK,
                        ClickID = (byte)(150 + i),
                        UCID = 0,
                        ReqI = (byte)(150 + 1),
                        L = 25,
                        W = 5,
                        T = (byte)(120 + 4 * i),
                        H = 4,
                        Text = Convert.ToString(distances[i])
                    }
                );
            }
        }

        public void DistancesToCarsClear(InSimDotNet.InSim _inSim)
        {
            _inSim.Send(
                new IS_BFN
                {
                    ReqI = 0,
                    SubT = ButtonFunction.BFN_DEL_BTN,
                    UCID = 0,
                    ClickID = 150,
                    ClickMax = 200
                }
            );
        }

        public void Crash(InSimDotNet.InSim _inSim, String name1, String name2)
        {
            String data = DateTime.Now.ToString("h:mm:ss");
            String tempText;
            if (name1 == "") { tempText = "Crash: No data yet"; }
                else { tempText = "Crash: " + name1 + "^9 and " + name2 + " ^9" + data; }

            _inSim.Send(
                new IS_BTN
                {
                BStyle = ButtonStyles.ISB_DARK,
                ClickID = 6,
                UCID = 0,
                ReqI = 6,
                L = 150,
                W = 50,
                T = 110,
                H = 5,
                Text = tempText
                }
            );
        }

        public void CrashClear(InSimDotNet.InSim _inSim)
        {
            _inSim.Send(
                new IS_BFN
                {
                    ReqI = 0,
                    SubT = ButtonFunction.BFN_DEL_BTN,
                    UCID = 0,
                    ClickID = 6,
                }
            );
        }

        public void CarInfo(InSimDotNet.InSim _inSim, Car car)
        {
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = ButtonStyles.ISB_DARK,
                    ClickID = 7,
                    UCID = 0,
                    ReqI = 7,
                    L = 150,
                    W = 25,
                    T = 130,
                    H = 5,
                    Text = "PLID: " + car.PLID + " " + car.CName
                }
            );

            _inSim.Send(
                new IS_BTN
                {
                    BStyle = ButtonStyles.ISB_DARK,
                    ClickID = 8,
                    UCID = 0,
                    ReqI = 8,
                    L = 175,
                    W = 25,
                    T = 130,
                    H = 5,
                    Text = "Name: " + car.playerName
                }
            );

            _inSim.Send(
                new IS_BTN
                {
                    BStyle = ButtonStyles.ISB_DARK,
                    ClickID = 9,
                    UCID = 0,
                    ReqI = 9,
                    L = 150,
                    W = 50,
                    T = 135,
                    H = 5,
                    Text = "X: " + car.X + " Y: " + car.Y + " Z: " + car.Z + " H: " + car.heading
                }
            );

            _inSim.Send(
                new IS_BTN
                {
                    BStyle = ButtonStyles.ISB_DARK,
                    ClickID = 10,
                    UCID = 0,
                    ReqI = 10,
                    L = 150,
                    W = 25,
                    T = 140,
                    H = 5,
                    Text = "Raw Speed: " + car.speed
                }
            );

            _inSim.Send(
                new IS_BTN
                {
                    BStyle = ButtonStyles.ISB_DARK,
                    ClickID = 11,
                    UCID = 0,
                    ReqI = 11,
                    L = 175,
                    W = 25,
                    T = 140,
                    H = 5,
                    Text = "Distance1: " + car.distance
                }
            );

            _inSim.Send(
            new IS_BTN
                {
                    BStyle = ButtonStyles.ISB_DARK,
                    ClickID = 12,
                    UCID = 0,
                    ReqI = 12,
                    L = 175,
                    W = 25,
                    T = 145,
                    H = 5,
                    Text = "Distance2: " + (car.GetDistance())
                }
            );

            _inSim.Send(
                new IS_BTN
                {
                    BStyle = ButtonStyles.ISB_DARK,
                    ClickID = 13,
                    UCID = 0,
                    ReqI = 13,
                    L = 150,
                    W = 25,
                    T = 145,
                    H = 5,
                    Text = "Speed2: " + (car.speed2)
                }
            );
        }


        public void CarInfoClear(InSimDotNet.InSim _inSim)
        {
            _inSim.Send(
                new IS_BFN
                {
                    ReqI = 0,
                    SubT = ButtonFunction.BFN_DEL_BTN,
                    UCID = 0,
                    ClickID = 7,
                    ClickMax = 13
                }
            );
        }

        public void GPSToCar(InSimDotNet.InSim _inSim, String name1, int angle, int localAngle)
        {
            bool inFront = false;

            byte buttonPlace = 0;

            // Angle from -180 to -90 Fully Done
            if (angle < -90 && angle >= -180)
            {
                if (angle + localAngle < -90)
                {
                    if (localAngle < 90)
                    {
                        //Console.WriteLine("State 1.1.1");
                        buttonPlace = (byte)(90 - (180 + angle + localAngle));
                    }
                    else
                    {
                        //Console.WriteLine("State 1.1.2");
                        buttonPlace = (byte)(90 - (angle + localAngle - 180));
                    }
                }
                else
                {
                    if (localAngle > 180)
                    {
                        if (localAngle > 270)
                        {
                            //Console.WriteLine("State 1.2.1.1");
                            buttonPlace = (byte)(90 - (angle + localAngle - 180));
                        }
                        else
                        {
                            //Console.WriteLine("State 1.2.1.2");
                            buttonPlace = (byte)(90 + (angle + localAngle));
                        }
                    }
                    else
                    {
                        inFront = true;
                        //Console.WriteLine("State 1.2.2");
                        buttonPlace = (byte)(90 + (angle + localAngle));
                    }
                }
            }

            // Angle from 90 to 180
            if (angle > 90 && angle <= 180)
            {
                if (localAngle > 90 && localAngle <= 270)
                {
                    inFront = true;
                    //Console.WriteLine("State 2.1");
                    buttonPlace = (byte)(90 - ((360 - localAngle) - angle));
                }
                else
                {
                    if (localAngle <= 180)
                    {
                        //Console.WriteLine("State 2.2.1");
                        buttonPlace = (byte)(90 - (localAngle + angle - 180));
                    }
                    else
                    {
                        //Console.WriteLine("State 2.2.2");
                        //buttonPlace = (byte)(95 - (localAngle + angle));
                        buttonPlace = (byte)(90 + (540 - localAngle - angle));
                    }

                }
            }

            // Angle from 0 to 90
            if (angle >= 0 && angle <= 90)
            {
                if (localAngle > 270 || localAngle < 90)
                {
                    inFront = true;
                    //Console.WriteLine("State 3.1");
                    if (localAngle < 90)
                    {
                        buttonPlace = (byte)(90 + (localAngle + angle));
                    }
                    else
                    {
                        buttonPlace = (byte)(90 - (360 - localAngle - angle));
                    }
                }
                else
                {
                    //Console.WriteLine("State 3.2");
                    buttonPlace = (byte)(90 + (180 - localAngle - angle));
                }
            }

            // Angle from -90 to 0
            if (angle >= -90 && angle <= 0)
            {
                if (localAngle > 270 || localAngle < 90)
                {
                    inFront = true;
                    //Console.WriteLine("State 4.1");
                    if (localAngle < 90)
                    {
                        buttonPlace = (byte)(90 + (localAngle + angle));
                    }
                    else
                    {
                        buttonPlace = (byte)(90 - (360 - localAngle - angle));
                    }
                }
                else
                {
                    //Console.WriteLine("State 4.2");
                    buttonPlace = (byte)(90 + (180 - (localAngle + angle)));
                }
            }

            String tempText;
            if (name1 == "") { tempText = "Name: No data yet"; }

            else {

                char c1;
                while (true)
                { 
                    if (buttonPlace < 25) { c1 = Convert.ToChar(8592); break; }
                    if (buttonPlace < 50) { c1 = Convert.ToChar(8592); break; }
                    if (buttonPlace < 75) { c1 = Convert.ToChar(8598); break; }
                    if (buttonPlace < 125) { c1 = Convert.ToChar(8593); break; }
                    if (buttonPlace < 150) { c1 = Convert.ToChar(8599); break; }
                    if (buttonPlace < 200) { c1 = Convert.ToChar(8594); break; }
                    c1 = 'X';
                    break;
                }
               
                tempText = "^3" + c1; }

            _inSim.Send(
                new IS_BTN
                {
                    BStyle = ButtonStyles.ISB_C1,
                    ClickID = 14,
                    UCID = 0,
                    ReqI = 14,
                    L = 90,
                    W = 20,
                    T = 50,
                    H = 20,
                    Text = tempText
                }
            );

            _inSim.Send(
                new IS_BTN
                {
                    BStyle = ButtonStyles.ISB_DARK,
                    ClickID = 15,
                    UCID = 0,
                    ReqI = 15,
                    L = 150,
                    W = 50,
                    T = 80,
                    H = 5,
                    Text = angle + " " + (localAngle) + " " + (angle + localAngle)
                }
            );
        }

        public void GPSToCarClear(InSimDotNet.InSim _inSim)
        {
            _inSim.Send(
                new IS_BFN
                {
                    ReqI = 0,
                    SubT = ButtonFunction.BFN_DEL_BTN,
                    UCID = 0,
                    ClickID = 14
                }
            );
        }


        public void AverageSpeeds(InSimDotNet.InSim _inSim, double[] averages)
        {
            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + (byte)ButtonStyles.ISB_CLICK + ButtonStyles.ISB_LEFT,
                    ClickID = 15,
                    UCID = 0,
                    ReqI = 15,
                    L = 135,
                    W = 25,
                    T = 5,
                    H = 5,
                    Text = "Average Speeds: "
                }
            );

            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_LEFT,
                    ClickID = 16,
                    UCID = 0,
                    ReqI = 16,
                    L = 135,
                    W = 25,
                    T = 10,
                    H = 5,
                    Text = "030s: " + averages[0] + "km/h"
                }
            );

            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_LEFT,
                    ClickID = 17,
                    UCID = 0,
                    ReqI = 17,
                    L = 135,
                    W = 25,
                    T = 15,
                    H = 5,
                    Text = "060s: " + averages[1] + "km/h"
                }
            );

            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_LEFT,
                    ClickID = 18,
                    UCID = 0,
                    ReqI = 18,
                    L = 135,
                    W = 25,
                    T = 20,
                    H = 5,
                    Text = "120s: " + averages[2] + "km/h"
                }
            );

            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_LEFT,
                    ClickID = 19,
                    UCID = 0,
                    ReqI = 19,
                    L = 135,
                    W = 25,
                    T = 25,
                    H = 5,
                    Text = "300s: " + averages[3] + "km/h"
                }
            );

            _inSim.Send(
                new IS_BTN
                {
                    BStyle = (byte)ButtonStyles.ISB_DARK + ButtonStyles.ISB_LEFT,
                    ClickID = 20,
                    UCID = 0,
                    ReqI = 20,
                    L = 135,
                    W = 25,
                    T = 30,
                    H = 5,
                    Text = "600s: " + averages[4] + "km/h"
                }
            );

        }

        public void AverageSpeedsClear(InSimDotNet.InSim _inSim)
        {
            _inSim.Send(
                new IS_BFN
                {
                    ReqI = 0,
                    SubT = ButtonFunction.BFN_DEL_BTN,
                    UCID = 0,
                    ClickID = 15,
                    ClickMax = 20
                }
            );
        }

    }
}
