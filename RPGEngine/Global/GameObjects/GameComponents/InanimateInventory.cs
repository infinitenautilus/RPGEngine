using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.Global.GameObjects.GameComponents
{
    public class InanimateInventory : GameComponent
    {
        public List<GameObject> MyGameObjects { get; set; } = new();

        public InanimateInventory()
        {
            ComponentName = "Inanimate Inventory";
        }

        public override void Pulse()
        {
            
        }
    }
}
