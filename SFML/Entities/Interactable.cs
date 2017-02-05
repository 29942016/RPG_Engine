using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG.Interfaces;
using SFML.System;
using SFML.Graphics;

namespace RPG.Entities
{
    public class Interactable : IInteractable
    {


        #region inherited functions
        public string GetName()
        {
            return "";
        }

        public float GetHealth()
        {
            return 0;
        }

        public FloatRect GetBoundryBox()
        {
            return new FloatRect();
        }
        public Vector2f GetPosition() 
        {
            return new Vector2f();
        }

        public bool IsFriendly(ITargetable target)
        {
            return false;
        }

        public bool IsAttackable(ITargetable source)
        {
            return false;
        }

        public void ModifyHealth(int amount)
        {
            //this.Health += amount;
        }

        public Vector2f GetCenter()
        {
           /* int xOffset = this.Sprite.TextureRect.Width / 2;
            int yOffset = this.Sprite.TextureRect.Height / 2;

            Vector2f center = new Vector2f(this.Sprite.Position.X + xOffset, this.Sprite.Position.Y + yOffset);

            return center;*/
            return new Vector2f();
        }
    }
    #endregion
}
