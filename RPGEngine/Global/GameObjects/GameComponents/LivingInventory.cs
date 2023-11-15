using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGEngine.Global.GameObjects;

namespace RPGEngine.Global.GameObjects.GameComponents
{
    public class LivingInventory : GameComponent
    {
        public List<GameObject> MyGameObjects { get; set; } = new();
        
        public LivingInventory()
        {
            ComponentName = "Living Inventory";
        }

        public override void Pulse()
        {
        }
    }
}
