using System;

using System.Threading;

namespace SearchQuestions.Events
{
    class Drag
    {
        private int dragPlayer1PLID;
        private int player1Distance;
        private int dragPlayer2PLID;
        private int player2Distance;

        public int distanceToDrive { private get; set; }

        public Drag()
        {
            dragPlayer1PLID = -1;
            dragPlayer2PLID = -1;
            distanceToDrive = 100000;
        }

        public void Execute(Parameters parameters, AllCars allCars, Commands commands, InSimDotNet.InSim _inSim, Buttons buttons)
        {
            if (parameters.dragMode)
            {
                if (parameters.dragPickPlayer1)
                {
                    dragPlayer1PLID = -1;
                    dragPlayer2PLID = -1;
                    player1Distance = 0;
                    player2Distance = 0;
                }

                if (parameters.dragPickPlayer1 && parameters.playerIndexChanged)
                {
                    int listID = parameters.playerIndexFromList;
                    dragPlayer1PLID = allCars.GetCarByIndex(listID).PLID;

                    parameters.dragPickPlayer1 = false;
                    parameters.dragPickPlayer2 = true;
                    parameters.playerIndexChanged = false;
                }

                if (parameters.dragPickPlayer2 && parameters.playerIndexChanged)
                {
                    int listID = parameters.playerIndexFromList;
                    dragPlayer2PLID = allCars.GetCarByIndex(listID).PLID;

                    parameters.dragPickPlayer2 = false;
                    parameters.dragReady = true;
                    parameters.playerIndexChanged = false;
                }

                if (parameters.dragStarted)
                {
                    bool goodStart = true;
                    if (allCars.GetCarByPLID(dragPlayer1PLID).rawSpeed > 100)
                    {
                        string name = allCars.GetCarByPLID(dragPlayer1PLID).playerName;
                        commands.SendCommandMessage(_inSim, "/msg ^7Blogas Startas: ^8" + name);
                        goodStart = false;
                    }
                    if (allCars.GetCarByPLID(dragPlayer2PLID).rawSpeed > 100)
                    {
                        string name = allCars.GetCarByPLID(dragPlayer2PLID).playerName;
                        commands.SendCommandMessage(_inSim, "/msg ^7Blogas Startas: ^8" + name);
                        goodStart = false;
                    }
                    if (!goodStart)
                    {
                        parameters.dragStarted = false;
                    }
                }

                if (parameters.dragRunning)
                {
                    player1Distance += allCars.GetCarByPLID(dragPlayer1PLID).tickDistance;
                    player2Distance += allCars.GetCarByPLID(dragPlayer2PLID).tickDistance;

                    if (player1Distance > distanceToDrive || player2Distance > distanceToDrive)
                    {
                        if (player1Distance > player2Distance)
                        {
                            String output = "/msg " + allCars.GetCarByPLID(dragPlayer1PLID).playerName + "^7 Laimėjo";
                            commands.SendCommandMessage(_inSim, output);
                            parameters.dragRunning = false;
                        }
                        else
                        {
                            String output = "/msg " + allCars.GetCarByPLID(dragPlayer2PLID).playerName + "^7 Laimėjo";
                            commands.SendCommandMessage(_inSim, output);
                            parameters.dragRunning = false;
                        }
                    }
                }

                if (parameters.dragPrintPlayer1)
                {
                    string name = "None";
                    if (dragPlayer1PLID != -1)
                    {
                        name = allCars.GetCarByPLID(dragPlayer1PLID).playerName;
                    }
                    commands.SendCommandMessage(_inSim, "/msg ^7Dalyvis Nr1: ^8" + name);
                    parameters.dragPrintPlayer1 = false;
                }

                if (parameters.dragPrintPlayer2)
                {
                    string name = "None";
                    if (dragPlayer1PLID != -1)
                    {
                        name = allCars.GetCarByPLID(dragPlayer2PLID).playerName;
                    }
                    commands.SendCommandMessage(_inSim, "/msg ^7Dalyvis Nr2: ^8" + name);
                    parameters.dragPrintPlayer2 = false;
                }

                if (parameters.dragPrintDistance)
                {
                    commands.SendCommandMessage(_inSim, "/msg ^7Drago atstumas: ^3" + distanceToDrive/1000 + "^7 metrų");
                    parameters.dragPrintDistance = false;
                }

                if (parameters.dragLights)
                {
                    parameters.dragLights = false;
                    new Thread(() =>
                    {
                        Thread.CurrentThread.IsBackground = true;
                        DragLights(parameters, commands, _inSim);
                        parameters.dragStarted = false;
                    }).Start();
                }

                string dragPlayerName1 = "None";
                if (dragPlayer1PLID != -1)
                {
                    dragPlayerName1 = allCars.GetCarByPLID(dragPlayer1PLID).playerName;
                }

                string dragPlayerName2 = "None";
                if (dragPlayer2PLID != -1)
                {
                    dragPlayerName2 = allCars.GetCarByPLID(dragPlayer2PLID).playerName;
                }
                buttons.ShowDragMenu(_inSim, parameters, dragPlayerName1, dragPlayerName2, distanceToDrive);
            }
        }

        private void DragLights(Parameters parameters, Commands commands, InSimDotNet.InSim _inSim)
        {
            char symbol = Convert.ToChar(9679);
            String symbols = "";

            for (int i = 0; i < 4; i++)
            {
                symbols += symbol;
            }

            commands.RCM_SetMessage(_inSim, Enums.LFSColors.RED + symbols);
            commands.RCM_ShowAll(_inSim);
            Thread.Sleep(3000);

            int repeat = 3;
            for (int i = 0; i < repeat; i++)
            {
                symbols += symbol;
                symbols += symbol;
                commands.RCM_SetMessage(_inSim, Enums.LFSColors.YELLOW + symbols);
                commands.RCM_ShowAll(_inSim);
                Thread.Sleep(1000);
            }

            parameters.dragStarted = false;
            parameters.dragRunning = true;
            commands.RCM_SetMessage(_inSim, Enums.LFSColors.GREEN + symbols);
            commands.RCM_ShowAll(_inSim);
            Thread.Sleep(2000);
            commands.RCC_RemoveAll(_inSim);
        }
    }
}
