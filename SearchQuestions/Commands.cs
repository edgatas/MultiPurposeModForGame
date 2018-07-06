using InSimDotNet;
using InSimDotNet.Packets;

using System;

using System.Collections.Generic;

namespace SearchQuestions
{
    class Commands
    {
        private Layout[] layouts;
        private int objectClock;
        private Random generator;

        public Commands()
        {
            generator = new Random();
            layouts = new Layout[6];
            layouts[0] = new Layout(-708, 946, 16, -180, 112);
            layouts[1] = new Layout(-708, 950, 16, -180, 112);
            layouts[2] = new Layout(-708, 954, 16, -180, 112);
            layouts[3] = new Layout(-712, 946, 16, -180, 112);
            layouts[4] = new Layout(-712, 950, 16, -180, 112);
            layouts[5] = new Layout(-712, 954, 16, -180, 112);
            objectClock = 0;
        }

        public void RCM_SetMessage(InSim _inSim, string text)
        {
            _inSim.Send(
                new IS_MST { Msg = "/rcm " + text, ReqI = 1 }
            );
        }

        public void RCM_ShowAll(InSim _inSim)
        {
            _inSim.Send(
                new IS_MST { Msg = "/rcm_all", ReqI = 1 }
            );
        }

        public void RCC_RemoveAll(InSim _inSim)
        {
            _inSim.Send(
                new IS_MST { Msg = "/rcc_all", ReqI = 1 }
            );
        }

        public void SendLocalMessage(InSim _inSim, string message)
        {
            _inSim.Send(
                new IS_MSL { Msg = message, ReqI = 1 }
            );
        }

        public void SendCommandMessage(InSim _inSim, string message)
        {
            _inSim.Send(
                new IS_MST { Msg = message, ReqI = 1 }
            );
        }

        public void RequestSTA(InSim _inSim)
        {
            _inSim.Send(
                new IS_TINY
                {
                    SubT = TinyType.TINY_SST,
                    ReqI = 1
                }
            );
        }

        public void RequestPlayersOnTrack(InSim _inSim)
        {
            _inSim.Send(
                new IS_TINY
                {
                    SubT = TinyType.TINY_NPL,
                    ReqI = 1
                }
            );
        }

        public void RequestAllConnections(InSim _inSim)
        {
            _inSim.Send(
                new IS_TINY
                {
                    SubT = TinyType.TINY_NCN,
                    ReqI = 1
                }
            );
        }

        public void SendToPitLane(InSim _inSim, string name)
        {
            _inSim.Send(
                new IS_MST { Msg = "/pitlane " + name, ReqI = 1 }
            );
        }


        public void create1Object(InSim _inSim)
        {
            if (objectClock > 3)
            {
                for (int i = 0; i < 6; i++)
                {

                    IS_AXM axmDelete = new IS_AXM
                    {
                        PMOAction = ActionFlags.PMO_DEL_OBJECTS
                    };

                    ObjectInfo simpleObjectDelete = new ObjectInfo { X = (short)(layouts[i].objectX * 16), Y = (short)(layouts[i].objectY * 16), Zbyte = (byte)(layouts[i].objectZ * 4), Heading = (byte)layouts[i].objectHeading, Index = (byte)layouts[i].objectIndex, Flags = 0 };
                    axmDelete.Info.Add(simpleObjectDelete);
                    _inSim.Send(
                        axmDelete
                    );

                    layouts[i].objectHeading += generator.Next(6) + 3;


                    IS_AXM axmCreate = new IS_AXM
                    {
                        PMOAction = ActionFlags.PMO_ADD_OBJECTS
                    };

                    ObjectInfo simpleObjectCreate = new ObjectInfo { X = (short)(layouts[i].objectX * 16), Y = (short)(layouts[i].objectY * 16), Zbyte = (byte)(layouts[i].objectZ * 4), Heading = (byte)layouts[i].objectHeading, Index = (byte)layouts[i].objectIndex, Flags = 0 };
                    axmCreate.Info.Add(simpleObjectCreate);
                    _inSim.Send(
                        axmCreate
                    );
                    objectClock = 0;
                }
            }
            else
            {
                objectClock++;
            }
        }

    }
}
