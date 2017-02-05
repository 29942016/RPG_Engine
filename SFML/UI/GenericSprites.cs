using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using RPG.Globals;

namespace RPG
{
    static class GenericSprites
    {
        
    }

    public class ToolTip
    {
        public List<Drawable> DrawableLayers = new List<Drawable>();

        private RectangleShape _BackPanel;
        private Text _SpellName, _ManaCost, _Description;

        public ToolTip(Spell spell) 
        {
            _BackPanel = new RectangleShape() 
            {
                Size = new Vector2f(200, 150),
                FillColor = Color.White,
                OutlineColor = Color.Black,
                OutlineThickness = 2,
                Position = new Vector2f(0,0)
            };

            _SpellName = new Text(spell.Name, Objects.GlobalFont, 16) 
            { 
                Style = Text.Styles.Bold,
                Color = Color.Black,
                Position = new Vector2f(_BackPanel.Position.X + 3, _BackPanel.Position.Y + 3)
            };
            _Description = new Text(spell.Description, Objects.GlobalFont, 14)
            {
                Color = Color.Black,
                Position = new Vector2f(_BackPanel.Position.X + 3, _BackPanel.Position.Y + 15)
            };
            _ManaCost = new Text(string.Format("{0} MP", spell.ManaCost), Objects.GlobalFont, 16) 
            { 
                Color = Color.Cyan,
                Style = Text.Styles.Bold,
                Position = new Vector2f((_BackPanel.Position.X + _BackPanel.Size.X) - 50, _BackPanel.Position.Y + 3)
            };
            
            AddLayers();
        }

        public void SetPosition(Vector2f pos)
        {
            _BackPanel.Position = pos;
            _SpellName.Position = new Vector2f(_BackPanel.Position.X + 3, _BackPanel.Position.Y + 3);
            _Description.Position = new Vector2f(_BackPanel.Position.X + 3, _BackPanel.Position.Y + 30);
            _ManaCost.Position = new Vector2f((_BackPanel.Position.X + _BackPanel.Size.X) - 52, _BackPanel.Position.Y + 3);
        }

        private void AddLayers()
        {
            DrawableLayers.Add(_BackPanel);
            DrawableLayers.Add(_SpellName);
            DrawableLayers.Add(_Description);
            DrawableLayers.Add(_ManaCost);
        }
    }
}
