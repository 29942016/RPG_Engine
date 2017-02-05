using System.Linq;
using SFML.Graphics;
using SFML.System;
using RPG.Globals;

namespace RPG.Extensions
{
    public static class TextExtensions
    {
        /// <summary>
        /// Wraps text based on a specified boundy. Wraps by WORD not char.
        /// </summary>
        public static void WrapText(this Text input, Vector2f wrapBoundry, uint fontSize)
        {
            int width = (int)input.GetLocalBounds().Width;

            int totalSize = 0;
            for (int i = 0; i < input.DisplayedString.Length; i++)
            {
                char cPointer = input.DisplayedString.ElementAt(i);
                Glyph cGlyph = Constants.GlobalFont.GetGlyph(cPointer, fontSize, false);
                totalSize += (int)cGlyph.Bounds.Width;

                if (cPointer == '\n')
                {
                    totalSize = 0;
                }
                else if (totalSize > wrapBoundry.X)
                {
                    // Find the closest previous whitespace to the current character, then insert a newline infront of it.
                    int closestWhiteSpace = input.DisplayedString.Substring(0, i).LastIndexOf(' ');
                    input.DisplayedString = input.DisplayedString.Insert(closestWhiteSpace + 1, "\n");

                    totalSize = 0;
                }

            }
        }
    }
}
