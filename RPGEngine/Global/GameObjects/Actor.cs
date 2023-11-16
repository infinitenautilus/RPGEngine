using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGEngine.Global.GameObjects.GameComponents;
using RPGEngine.Global.Heartbeat;
using RPGEngine.Global.Logging;

namespace RPGEngine.Global.GameObjects
{
    public class Actor : GameObject
    {
        public HealthComponent HealthComponent { get; } = new();
        public LivingInventory InventoryComponent { get; } = new();

        public Actor() : base()
        {
            ShortName = "Actor Class";
            Description = "This is the Actor Class.";
            AddComponents();
        }
        
        public Actor(string shortname)
        {
            ShortName = shortname;
        }

        public override void Pulse()
        {

        }

        private void AddComponents()
        {
            MyGameComponents.Add(HealthComponent);
            MyGameComponents.Add(InventoryComponent);
        }
    }
}
