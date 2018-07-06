using System;
using InSimDotNet;
using InSimDotNet.Packets;

using System.Diagnostics;
using System.Threading;

namespace SearchQuestions
{
    class CruiseEngine
    {
        private AllCars allCars;
        private Buttons buttons;
        private PlayerInfo players;
        private readonly InSim _inSim;
        private String[] chat;
        private String[] chatBuffer;
        private int count;
        private int activePLID;
        private int activePLID2;

        private int newestOnTrackPLID;
        private String newestOnSeverName;
        private String lastCrashNameA;
        private String lastCrashNameB;

        bool active;

        private UInt16[] distancesToCars;

        private double[] avgSpeeds;

        Parameters parameters;
        Commands commands;
        Calculations calculations;
        TimeManagement _time;
        Events.Drag drag;
        Systems.CarAhead carAheadWarning;

        public CruiseEngine()
        {
            _inSim = new InSim();
            _time = new TimeManagement();

            allCars = new AllCars();
            buttons = new Buttons();
            chat = new String[100];
            chatBuffer = new String[100];
            count = 0;
            players = new PlayerInfo();

            parameters = new Parameters();
            commands = new Commands();
            calculations = new Calculations();
            drag = new Events.Drag();
            carAheadWarning = new Systems.CarAhead();

            activePLID = -1;
            activePLID2 = -1;
            active = true;

            lastCrashNameA = "";
            lastCrashNameB = "";

            avgSpeeds = new double[5];

            _inSim.Bind<IS_MSO>(MessageOut);
            _inSim.Bind<IS_MCI>(CarDataIn);
            _inSim.Bind<IS_STA>(PlayerInfo);
            _inSim.Bind<IS_NPL>(NewPlayer);
            _inSim.Bind<IS_NCN>(NewConnection);
            _inSim.Bind<IS_CNL>(Disconnected);
            _inSim.Bind<IS_PLP>(PlayerPits);
            _inSim.Bind<IS_PLL>(PlayerSpectates);
            _inSim.Bind<IS_CRS>(CarReset);
            _inSim.Bind<IS_PIT>(PITStop);
            _inSim.Bind<IS_PSF>(PITFInished);
            _inSim.Bind<IS_CON>(Crash);
            _inSim.Bind<IS_TOC>(CarTakeOver);
            _inSim.Bind<IS_BTC>(ButtonPressed);
            _inSim.Bind<IS_BTT>(ButtonText);

            // Initialize InSim
            _inSim.Initialize(new InSimSettings
            {
                Host = "127.0.0.1",
                Port = 29999,
                Admin = "ed",
                Interval = 250,
                Flags = InSimFlags.ISF_MCI + (int)InSimFlags.ISF_LOCAL + (int)InSimFlags.ISF_CON
            });

            // For now, this will give enough time to make a connection without trying to run any inSim commands
            Thread.Sleep(500); 
            run();
        }

        public void run()
        {
            Stopwatch clock = new Stopwatch();

            /*
             * One of the bigger problems is that every loop clear commands are being calls as the loops runs, instead of 1 time.
             * Fastest way to deal with this is to create a parameters for single events for each butotn clear. This way I could
             * send the command to the game only once instead of every time.
             **/

            // Simulating non-existing player
            //allCars.NewCar(250, "^3Faker", "FZR");
            //allCars.UpdateCarCoordinates(250, -164, -494, 7);
            //allCars.UpdateCarHeading(250, 0);
            //players.SetPLID(200, 250);
            //players.SetName(200, "^3Faker");

            commands.RequestAllConnections(_inSim);
            commands.RequestPlayersOnTrack(_inSim);
            commands.RequestSTA(_inSim);
            while (active)
            {
                clock.Start();
                _time.Execute();
                //if (_time.Elapsed30) { Console.WriteLine("GOT 30"); }
                //if (_time.Elapsed60) { Console.WriteLine("GOT 60"); }
                //if (_time.Elapsed120) { Console.WriteLine("GOT 120"); }
                //if (_time.Elapsed300) { Console.WriteLine("GOT 300"); }
                //if (_time.Elapsed600) { Console.WriteLine("GOT 600"); }
                


                //activePLID = 250; // To focus non existing player

                if (ShowConnectivityStatus() == false) { active = false; Console.WriteLine(@"Disconecting"); break; }

                buttons.MenuOnOff(_inSim, parameters);
                if (parameters.showMenu) { buttons.MenuMain(_inSim, parameters); } else { buttons.MenuMainClear(_inSim); }

                buttons.EventMenu(_inSim, parameters);
                if (parameters.showEventMenu) { buttons.ShowEventMenu(_inSim, parameters); }

                if (allCars.GetList().Count > 0)
                {
                    if (parameters.showNewOnTrack) { buttons.NewestOnTrack(_inSim, players.GetNameByPLID(newestOnTrackPLID)); }
                    else { buttons.NewestOnTrackClear(_inSim); }
                    if (parameters.showNewOnServer) { buttons.NewestOnServer(_inSim, newestOnSeverName); }
                    else { buttons.NewestOnServerClear(_inSim); }
                    if (parameters.showLastCrash && parameters.crashHappened) { buttons.Crash(_inSim, lastCrashNameA, lastCrashNameB); parameters.crashHappened = false; }
                    else { buttons.CrashClear(_inSim); }

                    // There is a bug here. If there are no more players on track and a player is getting into garage. ActivePLID
                    // is not updating fast enough to change it. This will be changed afer receiving new STA packet, but that is too slow.
                    // Need to find a solutions to this problem.
                    if (activePLID != -1 && parameters.showCarInfo) { buttons.CarInfo(_inSim, allCars.GetCarByPLID(activePLID)); }
                    else { buttons.CarInfoClear(_inSim); }

                    if (parameters.showPlayerList) { buttons.CarList(_inSim, allCars.GetList(), parameters); } else { buttons.CarListClear(_inSim); }

                    //if (parameters.playerIndexChanged)
                    //{
                    //    int player = parameters.playerIndexFromList;
                    //    activePLID2 = allCars.GetCarByIndex(player).PLID;
                    //    parameters.playerIndexChanged = false;
                    //}

                    if (allCars.GetList().Count > 1 && activePLID != -1)
                    {
                        carAheadWarning.Execute(allCars, activePLID);
                        distancesToCars = new UInt16[allCars.Length()];
                        distancesToCars = calculations.GetDistanesToCars(allCars.GetList(), allCars.GetCarByPLID(activePLID));

                        Car closest = allCars.GetCarByIndex(calculations.GetClosestIndex(distancesToCars));
                        int distance = allCars.GetCarByPLID(activePLID).GetDistanceToAnotherCar(closest);

                        if (parameters.showDistance) { buttons.ClosestCar(_inSim, closest.playerName, distance); }
                        else { buttons.ClosestCarClear(_inSim); }

                        // This is testing. Check another time when coming back

                        if (parameters.trackPlayerMode)
                        {
                            if (activePLID2 != -1)
                            {
                                int angle = allCars.GetCarByPLID(activePLID).GetAngleToAnotherCar(allCars.GetCarByPLID(activePLID2));
                                buttons.GPSToCar(_inSim, players.GetNameByPLID(activePLID2), angle, allCars.GetCarByPLID(activePLID).heading);
                            }
                            else
                            {
                                int angle = allCars.GetCarByPLID(activePLID).GetAngleToAnotherCar(closest);
                                buttons.GPSToCar(_inSim, players.GetNameByPLID(closest.PLID), angle, allCars.GetCarByPLID(activePLID).heading);
                            }
                        }
                        else
                        {
                            buttons.GPSToCarClear(_inSim); //Need to make a single call instead of every loop
                        }

                        if (parameters.distancesList && parameters.showPlayerList) // Show
                        {
                            //int[] distancesToCars = new int[allCars.Length()];
                            //distancesToCars = calculations.GetDistanesToCars(allCars.GetList(), allCars.GetCarByPLID(activePLID));
                            buttons.DistanesToCars(_inSim, carAheadWarning.carHeadingsDiff, carAheadWarning.warningColors);
                            //buttons.DistanesToCars(_inSim, distancesToCars);

                            if (parameters.distanceEventChanged)
                            {
                                allCars.GetCarByIndex(parameters.distanceEventID);
                                string text = "/msg ^3Arčiausiai yra: " + allCars.GetCarByIndex(parameters.distanceEventID).playerName + "^3. Atstumas: ^7" + distancesToCars[parameters.distanceEventID] + "^3 m.";
                                commands.SendCommandMessage(_inSim, text);
                                parameters.distanceEventChanged = false;
                        }
                        }
                        else // Clear
                        {
                            buttons.DistancesToCarsClear(_inSim); //Need to make a single call instead of every loop
                        }




                        //Console.WriteLine(activePLID + " " + activePLID2);
                    }
                    //else
                    //{
                    //    buttons.GPSToCar(_inSim, "", 0, 0);
                    //}
                }

                //if (!parameters.showMenu) { buttons.MenuMainClear(_inSim); }
                if (!parameters.showDanger) { buttons.DangerAheadClear(_inSim); }
                if (!parameters.showNewOnTrack) { buttons.NewestOnTrackClear(_inSim); }
                if (!parameters.showNewOnServer) { buttons.NewestOnServerClear(_inSim); }


                if (parameters.sendToPitMode && parameters.playerPitLane == true)
                {
                    int player = parameters.playerIndexFromList;
                    string name = allCars.GetCarByIndex(player).playerName;
                    commands.SendToPitLane(_inSim, name);
                    parameters.playerPitLane = false;
                }


                if (parameters.createObject)
                {
                    commands.create1Object(_inSim);
                    //parameters.createObject = false;
                }
                //ProcessNewMessage();


                if (parameters.displayAverageSpeeds && allCars.GetCarByPLID(activePLID) != null)
                {
                    avgSpeeds[0] = allCars.GetCarByPLID(activePLID).CalculateAVGSpeed30S();
                    avgSpeeds[1] = allCars.GetCarByPLID(activePLID).CalculateAVGSpeed60S();
                    avgSpeeds[2] = allCars.GetCarByPLID(activePLID).CalculateAVGSpeed120S();
                    avgSpeeds[3] = allCars.GetCarByPLID(activePLID).CalculateAVGSpeed300S();
                    avgSpeeds[4] = allCars.GetCarByPLID(activePLID).CalculateAVGSpeed600S();
                    buttons.AverageSpeeds(_inSim, avgSpeeds);
                    if (parameters.averagePrint)
                    {
                        int temp = 0;
                        switch(parameters.averageSpeedIndex)
                        {
                        case 0:
                            temp = 30;
                            break;
                        case 1:
                            temp = 60;
                            break;
                        case 2:
                            temp = 120;
                            break;
                        case 3:
                            temp = 300;
                            break;
                        case 4:
                            temp = 600;
                            break;
                        case 5:
                            temp = 600;
                            break;
                        }
                        String output = "/msg " + allCars.GetCarByPLID(activePLID).playerName +
                                        " ^7vidutinis greitis ^3" +
                                        avgSpeeds[parameters.averageSpeedIndex] +
                                        " ^7km/h per " + temp + "s.";
                        commands.SendCommandMessage(_inSim, output);
                        parameters.averagePrint = false;
                    }
                }
                else
                {
                    buttons.AverageSpeedsClear(_inSim);
                }


                drag.Execute(parameters, allCars, commands, _inSim, buttons);

                _time.ResetConditions();
                clock.Stop();
                int wait = (int)clock.ElapsedMilliseconds;
                clock.Reset();
                Thread.Sleep(250 - wait);
            }
        }

        public void ProcessNewMessage()
        {
            //if (chatBufferCount > 0)
            //{
            //    Console.WriteLine("Processing " + chatBuffer[chatBufferCount - 1]);
            //    String message = chatBuffer[chatBufferCount-1];

            //    try
            //    {
            //        String[] parts = message.Split(' ');
            //        if (parts[0][0].CompareTo('[') == 0)
            //        {
            //            Console.WriteLine("MOD MESSAGE");
            //        }
            //        else
            //        {

            //        }
            //        if (players.PlayerExist(parts[0]))
            //        {
            //            Console.WriteLine(parts[0] + " said: " + parts[2]);
            //        }
            //    }
            //    catch(Exception)
            //    {

            //    }

            //    chatBuffer[chatBufferCount-1] = "";
            //    chatBufferCount--;
            //}

        }

        private void AddNewLine(String line)
        {
            if (count == 100)
            {
                count = 0;
            }
            chat[count] = line;
            count++;

            //chatBuffer[chatBufferCount] = line;
            //chatBufferCount++;
            //Console.WriteLine("GOT NEW STUFF");
        }
        public void ClearHistory()
        {
            chat = new String[100];
            count = 0;
        }

        public void ClearLine(int index)
        {
            chat[index] = "";
        }

        public String[] GetChat()
        {
            return chat;
        }

        public void CloseConnection()
        {
            if (_inSim != null && _inSim.IsConnected) { _inSim.Disconnect(); }
        }

        public void SendWelcomeMessage()
        {
            _inSim.Send(
                new IS_MSL { Msg = "System Connneted", ReqI = 3 }
            );
        }

        //private void insim_PacketReceived(object sender, PacketEventArgs e)
        //{
        //    if (e.Packet.Type == PacketType.ISP_MSO)
        //    {
        //        IS_MSO mso = (IS_MSO)e.Packet;
        //        Console.WriteLine("IS_MSO pack received");
        //    }
        //}

        public void SendAnswer(String answer)
        {
            Console.WriteLine(answer);
            _inSim.Send(
                new IS_MSL { Msg = answer, ReqI = 3 }
            );
        }

        private void MessageOut(InSim insim, IS_MSO mso)
        {
            AddNewLine(mso.Msg);
        }

        private void CarDataIn(InSim insim, IS_MCI mci)
        {
            //Console.WriteLine("IS_MSI pack received");
            for (int i = 0; i < mci.NumC; i++)
            {
                if (allCars.GetCarID(mci.Info[i].PLID) != -1)
                {
                    // Milimeters precision
                    allCars.UpdateCarRawCoordinates(mci.Info[i].PLID, mci.Info[i].X / 65.535, mci.Info[i].Y / 65.535, mci.Info[i].Z / 65.535);
                    // Meters precision
                    allCars.UpdateCarCoordinates(mci.Info[i].PLID, mci.Info[i].X / 65535, mci.Info[i].Y / 65535, mci.Info[i].Z / 65535);
                    allCars.UpdateCarSpeed(mci.Info[i].PLID, mci.Info[i].Speed);
                    allCars.UpdateCarHeading(mci.Info[i].PLID, mci.Info[i].Heading / 182); // Somehow makes it 360 degrees. Will come back later
                    allCars.CarCalculations(mci.Info[i].PLID);
                }
                else
                {
                    // This will spam a bit while starting program, but necessary later on;
                    Console.WriteLine("Requesting info");
                    _inSim.Send(
                        new IS_TINY
                        {
                            SubT = TinyType.TINY_NPL,
                            ReqI = 1
                        }
                    );
                }
            }
        }

        public void PlayerInfo(InSim insim, IS_STA sta)
        {
            Console.WriteLine("IS_STA pack received");
            activePLID = sta.ViewPLID;
        }

        public void NewPlayer(InSim insim, IS_NPL npl)
        {
            Console.WriteLine("IS_NPL pack received" + npl.PName);
            if (players.GetPLID(npl.UCID) == -1)
            {
                players.SetName(npl.UCID, npl.PName);
                players.SetPLID(npl.UCID, npl.PLID);

                allCars.NewCar(npl.PLID, npl.PName, npl.CName);
                newestOnTrackPLID = npl.PLID;

                //allCars.GetCarByPLID(npl.PLID).ResetDistances();
            }
            else
            {
                allCars.UpdateCarName(npl.PLID, npl.CName);
                //allCars.GetCarByPLID(npl.PLID).ResetDistances();
            }
        }

        private void NewConnection(InSim insim, IS_NCN ncn)
        {
            Console.WriteLine("IS_NCN pack received" + ncn.PName);
            players.SetName(ncn.UCID, ncn.PName);
            newestOnSeverName = ncn.PName;
        }

        private void Disconnected(InSim insim, IS_CNL cpr)
        {
            Console.WriteLine("IS_CNL pack received");
            players.Delete(cpr.UCID);
        }

        public void Off(bool connectionActive)
        {
            if (!connectionActive) { return; }
        }

        public bool ShowConnectivityStatus()
        {
            return _inSim.IsConnected;
        }

        // Removes PLID from player and removes the car with same PLID
        private void PlayerPits(InSim insim, IS_PLP plp)
        {
            if (allCars.GetCarID(plp.PLID) != -1)
            {
                _inSim.Send(
                   new IS_MSL { Msg = players.GetNameByPLID(plp.PLID) + "^3 went to garage and drove " + (allCars.GetCarByPLID(plp.PLID).GetDistance()) + " meters", ReqI = 1 }
               );
                players.RemoveCarByPLID(plp.PLID);
                allCars.RemoveCarByPLID(plp.PLID);
                // This is to stop activing current car functions and wait for new STA packet to arrive to update current car.
                if (activePLID == plp.PLID) 
                {
                    activePLID = -1;
                }
            }
        }

        // Removes PLID from player and removes the car with same PLID
        private void PlayerSpectates(InSim insim, IS_PLL pll)
        {
            if (allCars.GetCarID(pll.PLID) != -1)
            {
                string text = players.GetNameByPLID(pll.PLID) + "^3 went to spectate and drove " + (allCars.GetCarByPLID(pll.PLID).GetDistance()) + " meters";
                commands.SendLocalMessage(_inSim, text);
                players.RemoveCarByPLID(pll.PLID);
                allCars.RemoveCarByPLID(pll.PLID);
            }
        }

        private void CarReset(InSim insim, IS_CRS crs)
        {
            string text = players.GetNameByPLID(crs.PLID) + "^3 resetted car";
            commands.SendLocalMessage(_inSim, text);
        }

        private void PITStop(InSim insim, IS_PIT pit)
        {
            string text = players.GetNameByPLID(pit.PLID) + "^3 made a pitstop";
            commands.SendLocalMessage(_inSim, text);
        }

        private void PITFInished(InSim insim, IS_PSF psf)
        {
            string text = players.GetNameByPLID(psf.PLID) + "^3 finished pit stop";
            commands.SendLocalMessage(_inSim, text);
        }

        private void Crash(InSim insim, IS_CON con)
        {
            lastCrashNameA = players.GetNameByPLID(con.A.PLID);
            lastCrashNameB = players.GetNameByPLID(con.B.PLID);
            parameters.crashHappened = true;
        }

        // Updates PLID (car) with new player name. Removes PLID from lending player and adds that PLID to new player
        private void CarTakeOver(InSim insim, IS_TOC toc)
        {
            allCars.UpdateCarPlayerName(toc.PLID, players.GetNameByUCID(toc.NewUCID));
            players.RemoveCarByUCID(toc.OldUCID);
            players.SetPLID(toc.NewUCID, toc.PLID);
        }

        private void ButtonPressed(InSim insim, IS_BTC btc)
        {
            parameters.sendID(btc.ClickID);
        }
        private void ButtonText(InSim insim, IS_BTT btt)
        {
            try
            {
                drag.distanceToDrive = Convert.ToInt32(btt.Text);
            }
            catch(Exception e)
            {
                drag.distanceToDrive = 0;
                Console.WriteLine("Could not convert number. Letters or symbols present in text");
                Console.WriteLine(e);
            }
        }
    }
}
