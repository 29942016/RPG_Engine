using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace RPG.Globals
{
    public class Objects
    {
        public static float PI = 3.14159265f;
        public static Font GlobalFont = new Font(@"C:\Windows\Fonts\Arial.ttf");
        public static CircleShape TargetRing = new CircleShape(14, 32)
        {
            OutlineColor = Color.Black,
            FillColor = Color.Transparent,
            OutlineThickness = 2
        };
    }
}
