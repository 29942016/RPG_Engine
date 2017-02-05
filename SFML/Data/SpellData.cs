using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using RPG.Interfaces;
using RPG.Entities;
using SFML.System;

namespace RPG.Data
{
    public class SpellConstructors
    {
        public class FireBall : Spell
        {
            public FireBall(string name, string description, uint manaCost, Texture visualEntityTexture)
                : base(name, description, manaCost, visualEntityTexture)
            {
                SpellIcon = new Texture(@"C:\Users\Lombardi\Documents\Visual Studio 2012\Projects\SFML\SFML\Resources\Spells\fireball64.png");
                ManaCost = manaCost;
                CanTargetSelf = false;
                SpellType = Enumerations.SpellType.Targeted;
            }

            public override void Cast(Player player, ITargetable target, int damage)
            {

                this.VisualEntity = new SpellParticle(this.SpellTexture, player.GetCenter(), target.GetCenter(), Enumerations.ParticleType.Homing);
                target.ModifyHealth(damage);
            }
        }
        public class IceLance : Spell
        {
            public IceLance(string name, string description, uint manaCost, Texture visualEntityTexture)
                : base(name, description, manaCost, visualEntityTexture) 
            {
                SpellIcon = new Texture(@"C:\Users\Lombardi\Documents\Visual Studio 2012\Projects\SFML\SFML\Resources\Spells\icelance64.png");
                ManaCost = manaCost;
                CanTargetSelf = false;
                SpellType = Enumerations.SpellType.Targeted;
            }

            public override void Cast(Player player, ITargetable target, int damage)
            {
                this.VisualEntity = new SpellParticle(this.SpellTexture, player.GetCenter(), target.GetCenter(), Enumerations.ParticleType.Homing);
                target.ModifyHealth(damage);
            }
        }
        public class PhaseShift : Spell
        {
            public PhaseShift(string name, string description, uint manaCost, Texture visualEntityTexture)
                : base(name, description, manaCost, visualEntityTexture) 
            {
                SpellIcon = new Texture(@"C:\Users\Lombardi\Documents\Visual Studio 2012\Projects\SFML\SFML\Resources\Spells\PhaseShift64.png");
                ManaCost = manaCost;
                SpellType = Enumerations.SpellType.Self;
            }

            public override void Cast(Player player)
            {
                player.Move((Enumerations.Direction)player.Direction, 50);
            }
        }
    }
}
