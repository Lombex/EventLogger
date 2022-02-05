using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC;
using VRC.Core;

namespace Utils
{
    public static class PlayerUtils
    {
        public static VRCPlayer GetVRCPlayer()
        {
            return VRCPlayer.field_Internal_Static_VRCPlayer_0;
        }
        public static VRCPlayer GetVRCPlayer(this Player player)
        {
            return player.prop_VRCPlayer_0;
        }
    }
}
