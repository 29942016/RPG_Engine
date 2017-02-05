using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using RPG.Globals;
using RPG.Extensions;

namespace RPG.UI
{
    public static class Debug
    {
        /// <summary>
        /// Draw this to see player debug information, GenerateDebugInfo must be
        /// called prior.
        /// </summary>
        public static List<Drawable> DebugPanel = new List<Drawable>();

        /// <summary>
        ///  This method populates all of the required UI information.
        /// </summary>
        public static void GenerateDebugInfo(Player player, int fps)
        {
            DebugPanel.Clear();

            _DebugText.DisplayedString = string.Format("Framerate: {0}\nX: {1}\nY: {2}\nTarget: {3}", fps, player.GetPosition().X, player.GetPosition().Y, player.CurrentTargetName());
            _DebugText.WrapText(_DebugBackground.Size, 16);

            DebugPanel.Add(_DebugBackground);
            DebugPanel.Add(_DebugText);
        }

        #region drawables
        private static Text _DebugText = new Text("", Constants.GlobalFont, 16)
        {
            Position = new Vector2f(4, 4),
            Color = Color.Black,
        };

        private static RectangleShape _DebugBackground = new RectangleShape()
        {
            Position = new Vector2f(2, 2),
            Size = new Vector2f(150, 300),
            FillColor = Color.White,
            OutlineColor = Color.Red,
            OutlineThickness = 2,
        };
        #endregion

    }
}
