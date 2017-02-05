using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using System.Drawing;

namespace RPG
{
    class SpellBar
    {
        public List<Drawable> SpellBarDrawables = new List<Drawable>();

        private Sprite _SpellBarFrame;
        private List<Spell> _SpellList = new List<Spell>();
        private Vector2f[] _SpellUIPosition = new Vector2f[] 
        {
            new Vector2f(10, 0),
            new Vector2f(84, 0),
            new Vector2f(158, 0),
            new Vector2f(232, 0),
            new Vector2f(306, 0)
        };

        public SpellBar(Texture SpellBarFrame, Vector2f position, List<Spell> spellList) 
        {
            _SpellBarFrame = new Sprite(SpellBarFrame) { Position = position };
            _SpellList = spellList;

            ApplySpellsToUI();
        }

        public void SetSpellAtIndex(Spell spell, int index)
        {
            _SpellList[index] = spell;    
        }

        public Spell GetSpellAtIndex(int index)
        {
            return _SpellList[index];
        }

        public Vector2f GetPosition()
        {
            return _SpellBarFrame.Position;
        }

        private void ApplySpellsToUI()
        {
            SpellBarDrawables.Clear();

            for (int i = 0; i < _SpellList.Count; i++)
            {
                Sprite spellSprite = new Sprite(_SpellList[i].SpellIcon);
                spellSprite.Position = new Vector2f(_SpellBarFrame.Position.X + _SpellUIPosition[i ].X, 
                                                    _SpellBarFrame.Position.Y + _SpellUIPosition[i ].Y);

                SpellBarDrawables.Add(spellSprite); 
            }

            SpellBarDrawables.Add(_SpellBarFrame); 
        }
    }
}
