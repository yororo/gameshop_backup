using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Enumerations
{
    public enum GamePlatform : short
    {
        Unspecified = 0,
        GameBoy,
        PC,
        PlayStationVita,
        PlayStation2,
        PlayStation3,
        PlayStation4,
        ThreeDS,
        Wii,
        WiiU,
        Xbox360,
        XboxOne,
    }
}
