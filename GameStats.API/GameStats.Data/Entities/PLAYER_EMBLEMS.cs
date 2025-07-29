using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStats.Data.Entities;

public class PLAYER_EMBLEMS
{
    public int PLAYER_ID { get; set; }

    public int GAME_ID { get; set; }

    public string EMBLEM { get; set; } = null!;
}
