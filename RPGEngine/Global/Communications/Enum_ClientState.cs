using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.Global.Communications
{
    public enum Enum_ClientState
    {
        Connecting = 0,
        Authenticating = 1,
        Authenticated = 2,
        Playing = 3
    }
}
