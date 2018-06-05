﻿using InSimDotNet;
using InSimDotNet.Packets;

namespace SearchQuestions
{
    class Commands
    {
        public Commands()
        {

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

        public void SendToPitLane(InSim _inSim, string name)
        {
            _inSim.Send(
                new IS_MST { Msg = "/pitlane " + name, ReqI = 1 }
            );
        }


    }
}
