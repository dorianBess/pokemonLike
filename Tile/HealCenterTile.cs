using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokemonLike
{
    internal class HealCenterTile : Tile
    {
        public HealCenterTile() : base(TypeTile.healCenter) 
        {

        }

        public void healTeam(Player player)
        { 
            foreach (Hero hero in player.team.Values) 
            {
                hero.currenthp = hero.hp;
            }
        }

    }
}
