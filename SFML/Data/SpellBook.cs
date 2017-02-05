using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Data
{
    public class SpellBook : Dictionary<Spell, bool>
    {
        private const int MAX_EQUIPPED_SPELLS = 4;

        public void EquipSpell(Spell spell)
        {
            int currentlyEquippedCount = this.Where(x => x.Value == true).Count();
            bool hasLearnedSpell = this.Select(x => x.Key).ToList().Contains(spell);

            if (currentlyEquippedCount < MAX_EQUIPPED_SPELLS && hasLearnedSpell)
            {
                this[spell] = true;
            }
        }

        public Spell GetSpellByName(string name)
        {
            Spell selectedSpell = this.Select(x => x.Key).Where(y => y.Name == name).FirstOrDefault();
            return selectedSpell;
        }

        public bool HasSpell(Spell spell)
        {
            if (this.Select(x => x.Key == spell) != null)
                return true;
            else
            {
                Console.WriteLine("Person does not have spell: " + spell.Name);
                return false;
            }
        }

        public bool HasSpellEquipped(Spell spell)
        {
            return this.FirstOrDefault(x => x.Key == spell).Value == true;
        }
    }
}
