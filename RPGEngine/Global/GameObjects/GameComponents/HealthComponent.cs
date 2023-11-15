using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.Global.GameObjects.GameComponents
{
    public class HealthComponent : GameComponent
    {
        private int current = 100, max = 100;

        public int CurrentHealth
        {
            get
            {
                current = Math.Max(0, Math.Min(current, max));

                return current;
            }

            set
            {
                current = value;
            }
        }

        public int MaxHealth { get; set; }

        public HealthComponent()
        {
            ComponentName = "Health";
        }


        public override void Pulse()
        {
            current++;
        }
    }
}
