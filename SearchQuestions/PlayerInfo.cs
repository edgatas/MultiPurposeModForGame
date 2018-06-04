using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchQuestions
{
    class PlayerInfo
    {
        private String[] name;
        private int[] PLID;

        public PlayerInfo()
        {
            name = new String[256];
            PLID = new int[256]; 
        }

        public void SetName(int UPID, String name)
        {
            this.name[UPID] = name;
        }
        public String GetNameByPLID(int PLID)
        {
            for (int i = 0; i< 256; i++)
            {
                if (this.PLID[i] == PLID)
                {
                    return name[i];
                }
            }
            return "";
        }


        public void SetPLID(int UPID, int PLID)
        {
            this.PLID[UPID] = PLID;
        }
        public int GetPLID(int UPID)
        {
            return PLID[UPID];
        }
        public int GetPLID(String name)
        {
            for (int i = 0; i < 256; i++)
            {
                if (name[i].Equals(""))
                {
                    if (name[i].Equals(name))
                    {
                        return PLID[i];
                    }
                }
            }
            return 0;
        }

        public void RemovePlayer(String name)
        {
            for (int i = 0; i < 256; i++)
            {
                if (name[i].Equals(name))
                {
                    ResetName(i);
                    ResetPLID(i);
                }
            }
        }

        public void RemovePlayer(int PLID)
        {
            for (int i = 0; i < 256; i++)
            {
                if (this.PLID[i] == PLID)
                {
                    ResetName(i);
                    ResetPLID(i);
                }
            }
        }

        private void ResetName(int index)
        {
            name[index] = "";
        }

        private void ResetPLID(int index)
        {
            PLID[index] = -1;
        }

        public void RemovePlayerByUPID(int UPID)
        {
            ResetName(UPID);
            ResetPLID(UPID);
        }

        public bool PlayerExist(String playerName)
        {
            for (int i = 0; i < 256; i++)
            {
                if (name[i].Equals(name))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
