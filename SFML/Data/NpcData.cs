using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG.Entities;
using SFML.System;
using SFML.Graphics;

namespace RPG.Data
{
    class NpcData
    {
        public class Bandit : NPC
        {
            public Bandit(string name, Texture texture, Vector2f position) : base(name, texture, position)
            {

            }
        }
    }
}
