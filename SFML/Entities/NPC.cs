using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using RPG.Interfaces;


// Note: Movement speed disabled.
namespace RPG.Entities
{
    public class NPC : IInteractable 
    {
        public string Name;
        private float _ProtectedHealth = 100;
        public float Health
        {
            get { return _ProtectedHealth; }
            set 
            {
                if (value <= 0)
                {
                    _ProtectedHealth = 0;
                    IsDead = true;
                    Sprite.TextureRect = _SpriteFrame[4][0];
                    //_MovementSpeed = 0;
                    return;
                }
                _ProtectedHealth = value;
            }
        }
        public float MaxHealth = 100;

        public int Direction { get; set; }
        public Sprite Sprite;
        private IntRect[][] _SpriteFrame = new IntRect[5][];
        //private int _CurrentFrame = 0;
        //private int _MovementSpeed = 4;

        public bool InCombat { get; set; }
        public bool IsDead { get; set; }

        public NPC(string name, Texture texture, Vector2f position)
        {
            Name = name;
            Sprite = new Sprite(texture, new IntRect(0, 0, 32, 32));
            Sprite.Position = position;

            _SpriteFrame[0] = new[] { new IntRect(0, 0, 32, 32), new IntRect(32, 0, 32, 32), new IntRect(64, 0, 32, 32) };
            _SpriteFrame[1] = new[] { new IntRect(0, 32, 32, 32), new IntRect(32, 32, 32, 32), new IntRect(64, 32, 32, 32) };
            _SpriteFrame[2] = new[] { new IntRect(0, 64, 32, 32), new IntRect(32, 64, 32, 32), new IntRect(64, 64, 32, 32) };
            _SpriteFrame[3] = new[] { new IntRect(0, 96, 32, 32), new IntRect(32, 96, 32, 32), new IntRect(64, 96, 32, 32) };
            _SpriteFrame[4] = new[] { new IntRect(0, 129, 32, 32) };

            Console.WriteLine("Npc spawned: " + Name);
        }

        #region Inherite methds
        public void ModifyHealth(int amount)
        {
            this.Health += amount;
        }

        public bool IsFriendly(ITargetable source)
        {
            return (source == this);
        }

        public FloatRect GetBoundryBox()
        {
            return this.Sprite.GetGlobalBounds();
        }

        public string GetName()
        {
            return this.Name;
        }

        public Vector2f GetPosition()
        {
            return this.Sprite.Position;
        }
        
        public Vector2f GetCenter()
        {
            int xOffset = this.Sprite.TextureRect.Width / 2;
            int yOffset = this.Sprite.TextureRect.Height / 2;

            Vector2f center = new Vector2f(this.Sprite.Position.X + xOffset, this.Sprite.Position.Y + yOffset);

            return center;
        }

        public float GetHealth()
        {
            return this.Health;
        }

        public bool IsAttackable(ITargetable source)
        {
            return (source == this);
        }
        #endregion
    }
}
