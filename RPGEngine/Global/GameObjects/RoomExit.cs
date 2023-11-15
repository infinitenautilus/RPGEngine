using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.Global.GameObjects
{
    public class RoomExit : GameObject
    {
        public string ExitDirection { get; set; }

        public string ExitFile { get; set; }

        public RoomExit(string exitdir, string exitfile)
        {
            ExitDirection = exitdir;
            ExitFile = exitfile;
        }

        public override void Pulse()
        {
            
        }
    }
}
