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

            _DebugPlayerText.DisplayedString = string.Format("Framerate:{0}\nX: {1}\nY: {2}\nTarget: {3}", fps, player.GetPosition().X, player.GetPosition().Y, player.CurrentTargetName());
            _DebugPlayerText.WrapText(_DebugBackground.Size, 16);

            DebugPanel.Add(_DebugBackground);
            DebugPanel.Add(_DebugHeader);
            DebugPanel.Add(_DebugPanelTitle);
            DebugPanel.Add(_DebugPlayerText);
        }

        #region drawables
        private static RectangleShape _DebugBackground = new RectangleShape()
        {
            Position = new Vector2f(5, 5),
            Size = new Vector2f(150, 300),
            FillColor = Color.Black,
            OutlineColor = Color.Blue,
            OutlineThickness = 2,
        };

        private static RectangleShape _DebugHeader = new RectangleShape()
        {
            Position = new Vector2f(_DebugBackground.GetGlobalBounds().Left, _DebugBackground.GetGlobalBounds().Top),
            Size = new Vector2f(_DebugBackground.GetGlobalBounds().Width, 20),
            FillColor = Color.Black,
            OutlineColor = Color.Blue,
            OutlineThickness = 2,
        };

        private static Text _DebugPlayerText = new Text(string.Empty, Constants.GlobalFont, 16)
        {
            Position = new Vector2f(_DebugBackground.GetGlobalBounds().Left + 3 ,_DebugHeader.GetGlobalBounds().Height),
            Color = Color.White,
        };

        private static Text _DebugPanelTitle = new Text("Debug Info", Constants.GlobalFont, 16)
        {
            Position = new Vector2f(_DebugBackground.GetGlobalBounds().Left + 25 ,_DebugHeader.GetGlobalBounds().Top + 2),
            Color = Color.White,
        };

        #endregion

    }
}
