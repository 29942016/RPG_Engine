using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace RPG.Data
{
    public class Spells 
    {
        private static List<Spell> _Spells = new List<Spell>();

        static Spells()
        {
            _Spells.AddRange(new Spell[] 
                { 
                    new Data.SpellConstructors.FireBall("Fire Ball", "Hurls a fiery ball of fire!", 25, 
                        new Texture (@"C:\Users\Lombardi\Documents\Visual Studio 2012\Projects\SFML\SFML\Resources\Spells\fireball_visual32.png")),
                    new Data.SpellConstructors.IceLance("Ice Lance", "Projects a glacial spike directly towards the target.", 25, 
                        new Texture (@"C:\Users\Lombardi\Documents\Visual Studio 2012\Projects\SFML\SFML\Resources\Spells\icelance_visual32.png")),
                    new Data.SpellConstructors.PhaseShift("Phase Shift", "Teleports the cast 40 yards in the current direction.", 50 , 
                        new Texture (@"C:\Users\Lombardi\Documents\Visual Studio 2012\Projects\SFML\SFML\Resources\Spells\fireball_visual32.png"))
                }
            );    
        }

        public static Spell GetSpellByName(string name)
        {
            Spell selectedSpell = _Spells.FirstOrDefault(x => x.Name == name);
            return selectedSpell;
        }



    }
}
