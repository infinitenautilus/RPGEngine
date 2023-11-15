using RPGEngine.Global.GameObjects;
using RPGEngine.Global.GameObjects.GameComponents;
using RPGEngine.Global.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.Global.GameCommands
{
    public class ScoreCommand : IGameCommand
    {
        public string GameCommandName { get; set; }

        public ScoreCommand()
        {
            GameCommandName = "score";
        }

        public void ExecuteGameCommand(string[] args, Actor actor)
        {
            if(actor is Player player)
            {
                StringBuilder output = new();

                HealthComponent hcomp = player.HealthComponent;
                if(hcomp == null)
                {
                    Troubleshooter.Instance.Log($"Health component was null in score command.");
                    return;
                }
            
                output.AppendLine($"Hp: {hcomp.CurrentHealth} / {hcomp.MaxHealth}");
                
                player.MyClient.SendMessage(output.ToString());
            }
        }
    }
}
