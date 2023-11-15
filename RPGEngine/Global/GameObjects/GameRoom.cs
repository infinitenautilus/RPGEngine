using RPGEngine.Global.GameObjects.GameComponents;
using RPGEngine.Global.Heartbeat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.Global.GameObjects
{
    public class GameRoom : GameObject
    {
        public InanimateInventory InventoryComponent { get; } = new();
        public List<RoomExit> RoomExits { get; set; } = new();

        public GameRoom()
        {
            AddGameComponent(InventoryComponent);
        }

        public override void Pulse()
        {
            
        }

        public void SetShortDescription(string str)
        {
            ShortName = str;
        }

        public void SetLongDescription(string str)
        {
            Description = str;
        }

        public void AddExit(string direction, string file)
        {
            RoomExit re = new(direction, file);
            RoomExits.Add(re);
        }

        public bool TestExit(string direction)
        {
            foreach(RoomExit re in RoomExits)
            {
                if (re.ExitDirection == direction) return true;
            }

            return false;
        }
    }
}
