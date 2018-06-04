using System;
using InSimDotNet;
using InSimDotNet.Packets;

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

        Parameters parameters;

        public CruiseEngine()
        {
            _inSim = new InSim();

            allCars = new AllCars();
            buttons = new Buttons();
            chat = new String[100];
            chatBuffer = new String[100];
            count = 0;
            players = new PlayerInfo();

            parameters = new Parameters();

            activePLID = -1;
            activePLID2 = -1;
            active = true;

            lastCrashNameA = "";
            lastCrashNameB = "";

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
            allCars.NewCar(30, "^3Faker", "FZR");
            allCars.UpdateCarCoordinates(30, -650, -300, 9);
            allCars.UpdateCarHeading(30, 0);
            players.SetPLID(200, 30);
            players.SetName(200, "^3Faker");

            RequestSTA();
            while (active)
            {
                if (ShowConnectivityStatus() == false) { active = false; Console.WriteLine(@"Disconecting"); break; }

                buttons.MenuOnOff(_inSim, parameters);
                if (parameters.showMenu) { buttons.MenuMain(_inSim, parameters); } else { buttons.MenuMainClear(_inSim); }

                if (allCars.GetList().Count > 0)
                {
                    if (parameters.showNewOnTrack) { buttons.NewestOnTrack(_inSim, players.GetNameByPLID(newestOnTrackPLID)); }
                    else { buttons.NewestOnTrackClear(_inSim); }
                    if (parameters.showNewOnServer) { buttons.NewestOnServer(_inSim, newestOnSeverName); }
                    else { buttons.NewestOnServerClear(_inSim); }
                    if (parameters.showLastCrash) { buttons.Crash(_inSim, lastCrashNameA, lastCrashNameB); }
                    else { buttons.CrashClear(_inSim); }

                    // There is a bug here. If there are no more players on track and a player is getting into garage. ActivePLID
                    // is not updating fast enough to change it. This will be changed afer receiving new STA packet, but that is too slow.
                    // Need to find a solutions to this problem.
                    if (activePLID != -1 && parameters.showCarInfo) { buttons.CarInfo(_inSim, allCars.GetCarByPLID(activePLID)); }
                    else { buttons.CarInfoClear(_inSim); }

                    if (parameters.showPlayerList) { buttons.CarList(_inSim, allCars.GetList()); } else { buttons.CarListClear(_inSim); }

                    if (parameters.playerIndexChanged)
                    {
                        int player = 0 - (100 - parameters.playerIndexFromList);
                        activePLID2 = allCars.GetCarByIndex(player).PLID;
                        parameters.playerIndexChanged = false;
                    }

                    if (allCars.GetList().Count > 1 && activePLID != -1)
                    {
                        Car closest = allCars.ClosestCar(activePLID);
                        int distance = allCars.GetCarByPLID(activePLID).GetDistanceToAnotherCar(closest);
                        int heading = allCars.GetCarByPLID(activePLID).heading;
                        int headingDiff = Math.Abs(heading - closest.heading);

                        if (parameters.showDistance) { buttons.ClosestCar(_inSim, closest.playerName, distance); }
                        else { buttons.ClosestCarClear(_inSim); }

                        bool closing = false;
                        if (distance < allCars.GetCarByPLID(activePLID).carDistance)
                        {
                            closing = true;
                        }
                        allCars.SetDistanceToClosestCar(activePLID, distance);
                        String color = "^9";
                        if (headingDiff < 270 && headingDiff > 90 && distance < 300 && closing) { color = "^3"; }
                        if (headingDiff < 210 && headingDiff > 150 && distance < 300 && closing) { color = "^1"; }

                        if (parameters.showDanger) { buttons.DangerAhead(_inSim, color); } else { buttons.DangerAheadClear(_inSim); }

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
                            buttons.GPSToCarClear(_inSim);
                        }

                        Console.WriteLine(activePLID + " " + activePLID2);
                    }
                    //else
                    //{
                    //    buttons.GPSToCar(_inSim, "", 0, 0);
                    //}
                }

                if (!parameters.showMenu) { buttons.MenuMainClear(_inSim); }
                if (!parameters.showDanger) { buttons.DangerAheadClear(_inSim); }
                if (!parameters.showNewOnTrack) { buttons.NewestOnTrackClear(_inSim); }
                if (!parameters.showNewOnServer) { buttons.NewestOnServerClear(_inSim); }


                if (parameters.sendToPitMode && parameters.playerPitLane == true)
                {
                    int player = 0 - (100 - parameters.playerIndexFromList);
                    string name = allCars.GetCarByIndex(player).playerName;
                    pitLane(name);
                    parameters.playerPitLane = false;
                }

                ProcessNewMessage();

                // Testing how github sees everything

                Thread.Sleep(250);
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

        public void pitLane(String name)
        {
            _inSim.Send(
                //new IS_MSL { Msg = "/pitlane " + name, ReqI = 3 }
                new IS_MST { Msg = "/pitlane " + name, ReqI = 3 }
            );
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

        private void insim_PacketReceived(object sender, PacketEventArgs e)
        {
            if (e.Packet.Type == PacketType.ISP_MSO)
            {
                IS_MSO mso = (IS_MSO)e.Packet;
                Console.WriteLine("IS_MSO pack received");
            }
        }

        public void SendAnswer(String answer)
        {
            Console.WriteLine(answer);
            _inSim.Send(
                new IS_MSL { Msg = answer, ReqI = 3 }
            );
        }

        public void SendLocalMessage(String message)
        {
            _inSim.Send(
                new IS_MSL { Msg = message, ReqI = 3 }
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
                    allCars.UpdateCarCoordinates(mci.Info[i].PLID, mci.Info[i].X / 65535, mci.Info[i].Y / 65535, mci.Info[i].Z / 65535);
                    allCars.UpdateCarSpeed(mci.Info[i].PLID, mci.Info[i].Speed);
                    allCars.UpdateCarHeading(mci.Info[i].PLID, mci.Info[i].Heading / 182); // Somehow makes it 360 degrees
                    allCars.CarCalculations(mci.Info[i].PLID);
                }
                else
                {
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
            //activePLID = allCars.GetCarByPLID(sta.ViewPLID).PLID; // Why I was using this?

            //Console.WriteLine("Getting IS_STA");
            //Console.WriteLine("Number of Connections: " + sta.NumConns);
            //Console.WriteLine("Player ID: " + sta.ViewPLID);
            //Console.WriteLine("Track: " + sta.Track);
        }

        public void NewPlayer(InSim insim, IS_NPL npl)
        {
            Console.WriteLine("IS_NPL pack received");
            if (players.GetPLID(npl.UCID) <= 0)
            {
                players.SetName(npl.UCID, npl.PName);
                players.SetPLID(npl.UCID, npl.PLID);

                allCars.NewCar(npl.PLID, npl.PName, npl.CName);
                newestOnTrackPLID = npl.PLID;
            }
            else
            {
                allCars.UpdateCarName(npl.PLID, npl.CName);
            }
        }

        private void NewConnection(InSim insim, IS_NCN ncn)
        {
            Console.WriteLine("IS_NCN pack received");
            if (players.GetPLID(ncn.UCID) <= 0)
            {
                players.SetName(ncn.UCID, ncn.PName);
                newestOnSeverName = ncn.PName;
            }
        }

        private void Disconnected(InSim insim, IS_CNL cpr)
        {

            Console.WriteLine("IS_CNL pack received");
            players.RemovePlayerByUPID(cpr.UCID);
        }

        public void Off(bool connectionActive)
        {
            if (!connectionActive) { return; }
        }

        public bool ShowConnectivityStatus()
        {
            return _inSim.IsConnected;
        }

        private void PlayerPits(InSim insim, IS_PLP plp)
        {
            if (allCars.GetCarID(plp.PLID) != -1)
            {
                _inSim.Send(
                   new IS_MSL { Msg = players.GetNameByPLID(plp.PLID) + "^3 went to garage and drove " + (allCars.GetCarByPLID(plp.PLID).distance2) + " meters", ReqI = 1 }
               );
                players.RemovePlayer(plp.PLID);
                allCars.RemoveCarByPLID(plp.PLID);
                // This is to stop updating car info if currently looked at car is pitting. Need time for new STA to arrive.
                if (activePLID == plp.PLID) 
                {
                    activePLID = -1;
                }
            }
        }

        private void PlayerSpectates(InSim insim, IS_PLL pll)
        {
            if (allCars.GetCarID(pll.PLID) != -1)
            {
                _inSim.Send(
                    new IS_MSL { Msg = players.GetNameByPLID(pll.PLID) + "^3 went to spectate and drove " + (allCars.GetCarByPLID(pll.PLID).distance2) + " meters", ReqI = 1 }
                );
                players.RemovePlayer(pll.PLID);
                allCars.RemoveCarByPLID(pll.PLID);
            }

        }

        private void CarReset(InSim insim, IS_CRS crs)
        {
            _inSim.Send(
                new IS_MSL { Msg = players.GetNameByPLID(crs.PLID) + "^3 resetted car", ReqI = 1 }
            );
        }

        private void PITStop(InSim insim, IS_PIT pit)
        {
            _inSim.Send(
                new IS_MSL { Msg = players.GetNameByPLID(pit.PLID) + "^3 made a pitstop", ReqI = 1 }
            );
        }

        private void PITFInished(InSim insim, IS_PSF psf)
        {
            _inSim.Send(
                new IS_MSL { Msg = players.GetNameByPLID(psf.PLID) + "^3 finished pit stop", ReqI = 1 }
            );
        }

        private void Crash(InSim insim, IS_CON con)
        {
            lastCrashNameA = players.GetNameByPLID(con.A.PLID);
            lastCrashNameB = players.GetNameByPLID(con.B.PLID);
        }

        private void CarTakeOver(InSim insim, IS_TOC toc)
        {
            players.RemovePlayerByUPID(toc.OldUCID);
            players.SetPLID(toc.NewUCID, toc.PLID);
        }

        private void ButtonPressed(InSim insim, IS_BTC btc)
        {
            Console.WriteLine("IS_BTC pack received");
            parameters.sendID(btc.ClickID);
        }

        private void RequestSTA()
        {
            _inSim.Send(
                new IS_TINY
                {
                    SubT = TinyType.TINY_SST,
                    ReqI = 1
                }
            );
        }
    }
}
