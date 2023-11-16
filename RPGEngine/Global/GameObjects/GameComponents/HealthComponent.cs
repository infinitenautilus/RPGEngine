using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.Global.GameObjects.GameComponents
{
    public class HealthComponent : GameComponent
    {
        private int currentHealth;

        public int CurrentHealth
        {
            get 
            {
                if (currentHealth > MaxHealth) return MaxHealth;

                return currentHealth;
            }

            set
            {
                if (value > MaxHealth) value = MaxHealth;
                if (value < 0) value = 0;

                currentHealth = value;
            }

        }

        public int MaxHealth { get; set; }

        public HealthComponent()
        {
            ComponentName = "Health";
        }


        public override void Pulse()
        {
            if(CurrentHealth < MaxHealth)
            {
                CurrentHealth++;
            }
        }
    }
}
