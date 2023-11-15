using RPGEngine.Global.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.World
{
    public class TheVoid : GameRoom
    {
        private static readonly Lazy<TheVoid> instance = new Lazy<TheVoid>(() => new TheVoid());
        public static TheVoid Instance = instance.Value;

        public TheVoid()
        {
            SetShortDescription("The Void");
            SetLongDescription("This is the Void, where all of creation begins and may end.");
        }

        public override void Pulse()
        {
            base.Pulse();
        }
    }
}
