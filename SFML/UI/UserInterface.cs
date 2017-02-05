using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SysDrawing = System.Drawing;
using RPG.Globals;

namespace RPG
{
    class UserInterface
    {
        public View View;
        public bool bInventory = false;
        public Sprite Inventory { get; set; }

        private Texture _InventoryTexture;

        public Sprite sHealthBar { get; set; }
        private Texture _HealthBarFrameTexture;
        private Text _HealthBarText;
       
        public SpellBar SpellBar { get; set; }
        private Texture _SpellBarTexture;
       
        public UserInterface(Player player)
        {
            View = new View();

            // -- TEXTURES
            _InventoryTexture       = new Texture(@"C:\Users\Lombardi\Documents\visual studio 2012\Projects\SFML\SFML\Resources\UserInterface\InventorySlots.png");
            _HealthBarFrameTexture  = new Texture(@"C:\Users\Lombardi\Documents\visual studio 2012\Projects\SFML\SFML\Resources\UserInterface\HealthBarFrame.png");
            _SpellBarTexture        = new Texture(@"C:\Users\Lombardi\Documents\Visual Studio 2012\Projects\SFML\SFML\Resources\UserInterface\Spellbar380x64.png");
           
            // -- INVENTORY
            Inventory = new Sprite(_InventoryTexture);
            Inventory.Position = new Vector2f(View.Size.X - Inventory.Texture.Size.X,
                                                View.Size.Y - Inventory.Texture.Size.Y);

            // -- SPELLBAR
            Vector2f SpellBarSize = new Vector2f((View.Size.X / 2) - (_SpellBarTexture.Size.X / 2), View.Size.Y - _SpellBarTexture.Size.Y * 1.5f);
            List<Spell> equipedSpells = player.MySpellBook.Where(x => x.Value == true).Select(x => x.Key).ToList();  // Create a list of all spells which are tagged as equiped from our spellbook.
            SpellBar = new SpellBar(_SpellBarTexture, SpellBarSize, equipedSpells);
           }

        #region status bars
        /// <summary>
        /// Returns the full collection of drawing objects 
        /// required to display our players health bar.
        /// </summary>
        public Drawable[] StatusBarStructure(Vector2f position, float healthPercentage, float manaPercentage)
        {
            List<Drawable> HealthBarObjects = new List<Drawable>()
            {
                HealthBar(position, healthPercentage),
                ManaBar(position, manaPercentage),
                HealthBarFrame(position, healthPercentage),
                HealthBarText(position, healthPercentage)
            };

            return HealthBarObjects.ToArray();
        }

        private Sprite HealthBarFrame(Vector2f position, float healthPercentage)
        {
            sHealthBar = new Sprite(_HealthBarFrameTexture, new IntRect(0, 0, 66, 25)) 
            {
                Position = new Vector2f(position.X , position.Y - 29),
            };
            sHealthBar.Scale = (sHealthBar.Scale / 2);

            return sHealthBar;
        }
        private RectangleShape HealthBar(Vector2f position, float healthPercentage)
        {
            RectangleShape healthbar = new RectangleShape(new Vector2f(64, 16)) 
            {
                FillColor = Color.Red,
                Position = new Vector2f(position.X, position.Y - 29),
                Scale = new Vector2f(healthPercentage, 1.0f),
            };
            healthbar.Scale = (healthbar.Scale / 2);

            return healthbar;
        }
        private RectangleShape ManaBar(Vector2f position, float ManaPercentage)
        {
            RectangleShape manaBar = new RectangleShape(new Vector2f(64, 8))
            {
                FillColor = new Color(0, 162, 255),
                Position = new Vector2f(position.X , position.Y - 21),
                Scale = new Vector2f(ManaPercentage, 1.0f),
            };
            manaBar.Scale = (manaBar.Scale / 2);

            return manaBar;
        }
        private Text HealthBarText(Vector2f position,  float HealthPercentage)
        {
            string healthString = string.Format("{0:0.0}%", HealthPercentage * 100);
            _HealthBarText = new Text(healthString, Objects.GlobalFont, 14)
            {
                Position = new Vector2f(position.X + 5 , position.Y - 29),
                Color = Color.Black,
                Font = Objects.GlobalFont
            };
            _HealthBarText.Scale = _HealthBarText.Scale / 2;

            return _HealthBarText ;
        }
        
        #endregion

        #region spellbar

        #endregion

    }

}
