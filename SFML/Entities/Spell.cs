using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using RPG.Interfaces;
using RPG.Entities;

namespace RPG
{
    public class Spell
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public uint ManaCost { get;  set; }
        public Texture SpellIcon { get; set; }
        public ToolTip ToolTip { get; set; }
        public Enumerations.SpellType SpellType { get; set; }
        public bool CanTargetSelf { get; set; }

        public Texture SpellTexture;
        public SpellParticle VisualEntity { get; set; }

        public Spell(string name, string description, uint manaCost, Texture spellParticleTexture)
        {
            Name = name;
            Description = description;
            ManaCost = manaCost;
            SpellTexture = spellParticleTexture;

            ToolTip = new ToolTip(this);
        }

        /// <summary>
        /// Override with the spell's effect.
        /// </summary>
        public virtual void Cast()
        { 
            // Do not fill.
        }

        public virtual void Cast(ITargetable target)
        {
            // Do not fill. 
        }

        public virtual void Cast(ITargetable target, int modifier)
        {
            // Do not fill. 
        }

        public virtual void Cast(Player player, ITargetable target, int modifier)
        { 
        
        }
  
        public virtual void Cast(Player player)
        {
            // Do not fill. 
        }
    }
}
