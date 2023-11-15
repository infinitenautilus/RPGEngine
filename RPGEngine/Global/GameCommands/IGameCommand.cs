using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGEngine.Global.GameObjects;

namespace RPGEngine.Global.GameCommands
{
    public interface IGameCommand
    {
        public string GameCommandName { get; set; }

        public void ExecuteGameCommand(string[] args, Actor actor);
    }
}
