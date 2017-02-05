using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using RPG.Data;
using RPG.Interfaces;

namespace RPG
{
    public class Player : ITargetable
    {
        public string Name { get; set; }
        public Sprite Sprite { get; private set; }
        public View View { get; set; }
        public SpellBook MySpellBook = new SpellBook();

        private Texture _Texture;
        private IntRect[][] _SpriteFrame = new IntRect[5][];
        private int _CurrentFrame;
        public int Direction { get; private set; }

        public bool InCombat { get; set; }
        public bool IsDead { get; private set; }
        public ITargetable CurrentTarget;

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
                    MovementSpeed = 0;
                    return;
                }
                _ProtectedHealth = value; 
            } 
        }
        public float MaxHealth = 100;
        public float HealthRegen = 0.1f;

        public float Mana = 900;
        public float MaxMana = 1000;
        public float ManaRegen = 1f;

        public int MovementSpeed = 4;

        public Player(string name)
        {
            Name = name;
            InCombat = true;
        }

        public void LoadPlayer()
        {
            _Texture = new Texture(@"C:\Users\Lombardi\Documents\visual studio 2012\Projects\SFML\SFML\Resources\Sprites\spritesheet.png");
          
            Sprite = new Sprite(_Texture, new IntRect(0, 0, 32, 32));
            Sprite.Position = new Vector2f(0, 0);
            _Texture.Smooth = true;
            View = new View(Sprite.Position, new Vector2f(640, 540));

            _CurrentFrame = 0;
            _SpriteFrame[0] = new[] { new IntRect(0, 0, 32, 32),  new IntRect( 32, 0, 32, 32), new IntRect(64, 0, 32, 32) };
            _SpriteFrame[1] = new[] { new IntRect(0, 32, 32, 32), new IntRect(32, 32, 32, 32), new IntRect(64, 32, 32, 32) };
            _SpriteFrame[2] = new[] { new IntRect(0, 64, 32, 32), new IntRect(32, 64, 32, 32), new IntRect(64, 64, 32, 32) };
            _SpriteFrame[3] = new[] { new IntRect(0, 96, 32, 32), new IntRect(32, 96, 32, 32), new IntRect(64, 96, 32, 32) };
            _SpriteFrame[4] = new[] { new IntRect(0, 129, 32, 32) };

            MySpellBook.Add(Spells.GetSpellByName("Fire Ball"), true);
            MySpellBook.Add(Spells.GetSpellByName("Ice Lance"), false);
            MySpellBook.Add(Spells.GetSpellByName("Phase Shift"), true);

            MySpellBook.EquipSpell(MySpellBook.GetSpellByName("Ice Lance"));

        }

        public void Cast(Spell spell)
        {
            if (IsDead)
                return;

            if(MySpellBook.HasSpell(spell) && MySpellBook.HasSpellEquipped(spell))
            {
                if (Mana >= spell.ManaCost)
                {
                    if (spell.SpellType == Enumerations.SpellType.Self)
                        spell.Cast(this);
                    else if (spell.SpellType == Enumerations.SpellType.Targeted)
                    {
                        if (CurrentTarget == null)
                            return;

                        if (CurrentTarget == this && !spell.CanTargetSelf)
                            return;

                        if (CurrentTarget == this && spell.CanTargetSelf)
                            spell.Cast(this, 25); // Amount to heal
                        
                        if(CurrentTarget != this && !CurrentTarget.IsFriendly(this))
                            spell.Cast(this, CurrentTarget, -25);
                    }

                    Mana -= spell.ManaCost;
                }
                else
                    Console.WriteLine("Insufficient mana / target is dead.");
            }
        }

        
        #region movement
        /// <summary>
        /// Changes the players sprite to look as if he's facing another direction.
        /// </summary>
        public void FaceDirection(Enumerations.Direction d)
        {
            Direction = (int)d;

            if (_CurrentFrame >= _SpriteFrame[(int)d].Length)
            {
                _CurrentFrame = 0;
            }
            else
                Sprite.TextureRect = _SpriteFrame[(int)d][_CurrentFrame++];
        }

        /// <summary>
        /// Moves the player in a specific direction.
        /// </summary>
        public void Move(Enumerations.Direction d, int acceleration = 0)
        {
            Vector2f movement = new Vector2f(0, 0);

            switch (d)
            {
                case Enumerations.Direction.Down:
                    movement = new Vector2f(0, MovementSpeed + acceleration);
                    break;
                case Enumerations.Direction.Up:
                    movement = new Vector2f(0, (MovementSpeed + acceleration) * -1);
                    break;
                case Enumerations.Direction.Left:
                    movement = new Vector2f((MovementSpeed + acceleration) * -1, 0);
                    break;
                case Enumerations.Direction.Right:
                    movement = new Vector2f(MovementSpeed + acceleration, 0);
                    break;
            }

            Sprite.Position += movement;
            View.Center = Sprite.Position;
        }

        /// <summary>
        /// Negates the players current direction.
        /// </summary>
        public void PushAway()
        {
            switch ((Enumerations.Direction)Direction)
            {
                case Enumerations.Direction.Down:
                    Move(Enumerations.Direction.Up);
                    break;
                case Enumerations.Direction.Up:
                    Move(Enumerations.Direction.Down);
                    break;
                case Enumerations.Direction.Left:
                    Move(Enumerations.Direction.Right);
                    break;
                case Enumerations.Direction.Right:
                    Move(Enumerations.Direction.Left);
                    break;
            }
        }
        #endregion
        #region inheritance
        public string CurrentTargetName()
        {
            if (this.CurrentTarget == null)
                return "N/A";
            else
                return this.CurrentTarget.GetName() ;
        }

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

        public float GetHealth()
        {
            return this.Health;
        }

        public bool IsAttackable(ITargetable source)
        {
            return false;
        }

        public Vector2f GetCenter()
        {
            int xOffset = this.Sprite.TextureRect.Width / 2;
            int yOffset = this.Sprite.TextureRect.Height / 2;

            Vector2f center = new Vector2f(this.Sprite.Position.X + xOffset, this.Sprite.Position.Y + yOffset);
            return center;
        }
        #endregion
    }
}
