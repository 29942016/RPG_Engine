using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using RPG.Entities;
using SFML.Graphics;

namespace RPG.TileEngine
{
    public class Map
    {
        public string Name;
        public int[] MapArray;
        public int[] ObjectArray;

        public string TileSet, 
                      ObjectTileSet;

        public Vector2u TileDimension;
        public uint Height, Width;

        public List<FloatRect> Collidables = new List<FloatRect>();

        public List<NPC> NpcCollection = new List<NPC>();

        public Map()
        {
 
        }

        public Map(string name, string tileSet, string objectTileSet , int[] mapStructure, int[] objectArray,  Vector2u tileDimensions, uint width, uint height)
        {
            Name = name;
            MapArray = mapStructure;
            TileSet = tileSet;
            ObjectTileSet = objectTileSet;
            TileDimension = tileDimensions;
            Height = height;
            Width = width;
            ObjectArray = objectArray;
        }

    }
}
