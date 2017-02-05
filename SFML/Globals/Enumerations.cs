using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public static class Enumerations
    {
        public enum Direction
        {
            Down = 0,
            Left = 1,
            Right = 2,
            Up = 3
        }

        public enum SpellType
        {
            Targeted = 0,
            Cone = 1,
            Area = 2,
            Self = 3
        }

        public enum ParticleType
        {
            Homing = 0,
            Instant = 1,
            Charged = 2
        }
    }
}
