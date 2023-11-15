using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGEngine.Global.GameObjects.GameComponents;

namespace RPGEngine.Global.GameObjects
{
    public abstract class GameObject
    {
        private static int _id = 0;
        
        public int Id { get { return _id; } }

        public string ShortName { get; set; } = "Game Object";
        public string Description { get; set; } = "This is the game object class itself. Ooops.";

        public List<GameComponent> MyGameComponents { get; set; } = new();

        public GameObject()
        {
            _id++;
        }

        public GameObject(string shortname, string description)
        {
            ShortName = shortname;
            Description = description;
            _id++;
        }

        public void AddGameComponent(GameComponent component)
        {
            MyGameComponents.Add(component);
        }

        public void RemoveGameComponent(GameComponent component)
        {
            if(MyGameComponents.Contains(component))
            {
                MyGameComponents.Remove(component);
            }
        }

        public GameComponent? GetGameComponent(string name)
        {
            foreach(GameComponent component in MyGameComponents)
            {
                if (string.Equals(component.ComponentName, name, StringComparison.OrdinalIgnoreCase))
                {
                    return component;
                }
            }

            return null;
        }

        public abstract void Pulse();

    }
}
