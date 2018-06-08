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
            PLID = new int[256];
            name = new String[256];
            
            for (int i = 0; i < 256; i++)
            {
                this.PLID[i] = -1;
                this.name[i] = "";
            }
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
        public String GetNameByUCID(int UCID)
        {
            return name[UCID];
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

        public void RemoveCarByPLID(int PLID)
        {
            for (int i = 0; i < 256; i++)
            {
                if (this.PLID[i] == PLID)
                {
                    ResetPLID(i);
                }
            }
        }

        public void RemoveCarByUCID(int UCID)
        {
            ResetPLID(UCID);
        }

        private void ResetName(int index)
        {
            name[index] = "";
        }

        private void ResetPLID(int index)
        {
            PLID[index] = -1;
        }

        public void Delete(int UCID)
        {
            ResetName(UCID);
            ResetPLID(UCID);
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
