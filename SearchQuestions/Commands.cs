using InSimDotNet;
using InSimDotNet.Packets;

using System.Collections.Generic;

namespace SearchQuestions
{
    class Commands
    {
        public int objectX { get; set; }
        public int objectY { get; set; }
        public int objectZ { get; set; }
        public int objectHeading { get; set; }
        public int objectIndex { get; set; }
        private int objectClock;

        public Commands()
        {
            objectX = -710;
            objectY = 950;
            objectZ = 25;
            objectHeading = -180;
            objectIndex = 174;
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
                IS_AXM axmDelete = new IS_AXM
                {
                    PMOAction = ActionFlags.PMO_DEL_OBJECTS
                };

                ObjectInfo simpleObjectDelete = new ObjectInfo { X = (short)(objectX * 16), Y = (short)(objectY * 16), Zbyte = (byte)(objectZ * 4), Heading = (byte)objectHeading, Index = (byte)objectIndex, Flags = 0 };
                axmDelete.Info.Add(simpleObjectDelete);
                _inSim.Send(
                    axmDelete
                );

                objectHeading += 1;


                IS_AXM axmCreate = new IS_AXM
                {
                    PMOAction = ActionFlags.PMO_ADD_OBJECTS
                };

                ObjectInfo simpleObjectCreate = new ObjectInfo { X = (short)(objectX * 16), Y = (short)(objectY * 16), Zbyte = (byte)(objectZ * 4), Heading = (byte)objectHeading, Index = (byte)objectIndex, Flags = 0 };
                axmCreate.Info.Add(simpleObjectCreate);
                _inSim.Send(
                    axmCreate
                );
                objectClock = 0;
            }
            else
            {
                objectClock++;
            }
        }

    }
}
