using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using RPG.Interfaces;
using SFML.Window;

namespace RPG.Globals
{
    static class Funcs
    {
      

        public static int OnSpellBarButton(Vector2f mouse, SpellBar uiBounds)
        {
            for (int i = 0; i < uiBounds.SpellBarDrawables.Count - 1; i++)
            {
                Sprite spellIcon = uiBounds.SpellBarDrawables[i] as Sprite;

                if(spellIcon.GetGlobalBounds().Contains(mouse.X, mouse.Y))
                {
                    return i;
                }
            }

            return -1;
        }

        public static ITargetable SetTarget(Vector2f mousePos, List<ITargetable> entityList)
        {
            foreach (ITargetable entity in entityList)
            {
                if (entity.GetBoundryBox().Contains(mousePos.X, mousePos.Y))
                {
                    return entity;
                }
            }

            return null;
        }

        public static bool CollidesBoundry(Enumerations.Direction direction, FloatRect playerBounds, FloatRect targetBounds)
        {
            if (playerBounds.Intersects(targetBounds))
                return true;

            return false ;
        }

        public static float GetPercentage(float lowerVal, float higherVal)
        {
            return lowerVal / higherVal;
        }

        public static float GetRotation(Vector2f position, Vector2f destination)
        {
            Vector2f vector = new Vector2f(position.X - destination.X, position.Y - destination.Y);
            float rotation = (float)(Math.Atan2(vector.Y, vector.X)) * 180 / Objects.PI;
           
            return rotation;
        }



    }
}
