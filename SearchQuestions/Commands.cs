using InSimDotNet;
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
    }
}
