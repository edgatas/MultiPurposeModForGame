using System;

using InSimDotNet;
using InSimDotNet.Packets;

namespace SearchQuestions
{
    class inSimWork
    {
        private AllCars allCars;
        private Buttons buttons;
        private PlayerInfo players;
        private readonly InSim _inSim;
        private String[] chat;
        private int count;
        private int activePLID;
        bool firstRun;

        Parameters parameters;

        public inSimWork()
        {
            firstRun = true;
            allCars = new AllCars();
            buttons = new Buttons();
            chat = new String[100];
            count = 0;
            players = new PlayerInfo();

            parameters = new Parameters();

            _inSim = new InSim();

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
        }

        private void AddNewLine(String line)
        {
            if (count == 100)
            {
                count = 0;
            }
            chat[count] = line;
            count++;
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

            if (allCars.GetList().Count > 0)
            {
                if (firstRun)
                {
                    activePLID = allCars.GetCarByPLID(mci.Info[0].PLID).PLID;
                    firstRun = false;
                }

                buttons.CarInfo(_inSim, allCars.GetCarByPLID(activePLID));
                if (parameters.showPlayerList) { buttons.CarList(_inSim, allCars.GetList()); } else { buttons.CarListClear(_inSim); }

                Car closest = allCars.ClosestCar(activePLID);
                int distance = allCars.GetCarByPLID(activePLID).GetDistanceToAnotherCar(closest);
                int heading = allCars.GetCarByPLID(activePLID).heading;
                int headingDiff = Math.Abs(heading - closest.heading);
                buttons.ClosestCar(_inSim, closest.playerName, distance);

                bool closing = false;
                if (distance < allCars.GetCarByPLID(activePLID).carDistance)
                {
                    closing = true;
                }
                allCars.SetDistanceToClosestCar(activePLID, distance);
                String color = "^9";
                if (headingDiff < 270 && headingDiff > 90 && distance < 300 && closing) { color = "^3"; }
                if (headingDiff < 210 && headingDiff > 150 && distance < 300 && closing) { color = "^1"; }

                buttons.MenuOnOff(_inSim, parameters);
                if (parameters.showMenu) { buttons.MenuMain(_inSim, parameters); } else { buttons.MenuMainClear(_inSim); }
                if (parameters.showDanger) { buttons.DangerAhead(_inSim, color); } else { buttons.DangerAheadClear(_inSim); }
            }


            if (!parameters.showMenu) { buttons.MenuMainClear(_inSim); }
            if (!parameters.showDanger) { buttons.DangerAheadClear(_inSim); }
            if (!parameters.showNewOnTrack) { buttons.NewestOnTrackClear(_inSim); }
            if (!parameters.showNewOnServer) { buttons.NewestOnServerClear(_inSim); }
        }

        public void PlayerInfo(InSim insim, IS_STA sta)
        {
            Console.WriteLine("IS_STA pack received");
            activePLID = allCars.GetCarByPLID(sta.ViewPLID).PLID;

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

                Console.WriteLine(parameters.showNewOnTrack);
                if (parameters.showNewOnTrack) { buttons.NewestOnTrack(_inSim, players.GetNameByPLID(npl.PLID)); } else { buttons.NewestOnTrackClear(_inSim); }
            }
            else
            {
                allCars.UpdateCarName(npl.PLID, npl.CName);
            }

        }
        
        private void NewConnection(InSim insim, IS_NCN ncn)
        {
            Console.WriteLine("IS_NCN pack received");
            if (parameters.showNewOnServer) { buttons.NewestOnServer(_inSim, ncn.PName); } else { buttons.NewestOnServerClear(_inSim); }
            if (players.GetPLID(ncn.UCID) <= 0)
            {
                players.SetName(ncn.UCID, ncn.PName);
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
            Console.WriteLine("Crash");
            buttons.Crash(_inSim, players.GetNameByPLID(con.A.PLID), players.GetNameByPLID(con.B.PLID));
        }

        private void CarTakeOver(InSim insim, IS_TOC toc)
        {
            Console.WriteLine("IS_TOC pack received");
            players.RemovePlayerByUPID(toc.OldUCID);
            players.SetPLID(toc.NewUCID, toc.PLID);
        }

        private void ButtonPressed(InSim insim, IS_BTC btc)
        {
            Console.WriteLine("IS_BTC pack received");
            parameters.sendID(btc.ClickID);
        }
    }
}
