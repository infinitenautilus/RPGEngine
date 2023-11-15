using RPGEngine.Global.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.World.ArlithCity.Rooms
{
    public class BlacksmithForge : GameRoom
    {
        public BlacksmithForge()
        {
            SetShortDescription("A Blacksmith's Forge");
            SetLongDescription("A magnificent forge sits aside a large tree stump with an anvil mounted atop it.  A large pair of bellows jut from either side of the great stone forge." +
                "Tongs, hammers, and other various tools adorn the walls.  You can see through the doorway, Central Avenue bustling to the south.");
        }
    }
}
