using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace RPG.TileEngine
{
    public  class TileMapEngine : Transformable, Drawable 
    {
        public  VertexArray m_verticesMap = new VertexArray(PrimitiveType.Quads);
        public VertexArray m_verticesObjects = new VertexArray(PrimitiveType.Quads);

        public  Texture[] m_tileset = new Texture[2];
        public Map CurrentMap { get; private set; }

        #region Load Map Base
        private  bool Load(string tileset, Vector2u tileSize, int[] tiles, uint width, uint height)
        {
            m_tileset[0] = new Texture(tileset);
            
            if (m_tileset == null)
                return false;

            m_verticesMap.Resize(width * height * 4);

            for (uint i = 0; i < width; i++)
            {
                for (uint j = 0; j < height; j++)
                {
                    // Current Tile Number
                    int tileNumber = tiles[i + j * width];

                    float tu = tileNumber % (m_tileset[0].Size.X / tileSize.X);
                    float tv = tileNumber / (m_tileset[0].Size.X / tileSize.X);

                    Vertex[] quad = new Vertex[4];

                    uint index = (i + j * width) * 4;

                    m_verticesMap[index] = new Vertex(new Vector2f(i * tileSize.X, j * tileSize.Y))
                    { TexCoords = new Vector2f(tu * tileSize.X, tv * tileSize.Y)};

                    m_verticesMap[index + 1] = new Vertex(new Vector2f((i + 1) * tileSize.X, j * tileSize.Y))
                    { TexCoords = new Vector2f((tu + 1) * tileSize.X, tv * tileSize.Y)};

                    m_verticesMap[index + 2] = new Vertex(new Vector2f(((i + 1) * tileSize.X), (j + 1) * tileSize.Y))
                    { TexCoords = new Vector2f((tu + 1) * tileSize.X, (tv + 1) * tileSize.Y)};

                    m_verticesMap[index + 3] = new Vertex(new Vector2f(i * tileSize.X, (j + 1) * tileSize.Y))
                    {TexCoords = new Vector2f(tu * tileSize.X, (tv + 1) * tileSize.Y)};

                }
            }
            
            

            return true;
        }
        #endregion
        #region Load Object Base
        private bool LoadObjects(string tileset, Vector2u tileSize, int[] tiles, uint width, uint height)
        {
            m_tileset[1] = new Texture(tileset);

            m_verticesObjects.Resize(width * height * 4);

            for (uint i = 0; i < width; i++)
            {
                for (uint j = 0; j < height; j++)
                {
                    int tileNumber = tiles[i + j * width];

                    if (tileNumber == -1)
                        continue;

                    float tu = tileNumber % (m_tileset[1].Size.X / tileSize.X);
                    float tv = tileNumber / (m_tileset[1].Size.X / tileSize.X);

                    Vertex[] quad = new Vertex[4];

                    uint index = (i + j * width) * 4;

                    m_verticesObjects[index] = new Vertex(new Vector2f(i * tileSize.X, j * tileSize.Y)) 
                    { TexCoords = new Vector2f(tu * tileSize.X, tv * tileSize.Y) };

                    m_verticesObjects[index + 1] = new Vertex(new Vector2f((i + 1) * tileSize.X, j * tileSize.Y)) 
                    { TexCoords = new Vector2f((tu + 1) * tileSize.X, tv * tileSize.Y) };

                    m_verticesObjects[index + 2] = new Vertex(new Vector2f(((i + 1) * tileSize.X), (j + 1) * tileSize.Y)) 
                    { TexCoords = new Vector2f((tu + 1) * tileSize.X, (tv + 1) * tileSize.Y) };

                    m_verticesObjects[index + 3] = new Vertex(new Vector2f(i * tileSize.X, (j + 1) * tileSize.Y)) 
                    { TexCoords = new Vector2f(tu * tileSize.X, (tv + 1) * tileSize.Y) };

                    // Add a floatrect to compare for collision checks.
                    FloatRect textRect = new FloatRect(m_verticesObjects[index].Position.X, m_verticesObjects[index].Position.Y, 16, 16);
                    CurrentMap.Collidables.Add(textRect);
                }
            }

            return true;
        }
        #endregion
        
        public void SetMap(Map map)
        {
            if (Load(map.TileSet, map.TileDimension, map.MapArray, map.Width, map.Height))
            {
                CurrentMap = map;

                if (LoadObjects(map.ObjectTileSet,map.TileDimension, map.ObjectArray, map.Width, map.Height))
                {
                    
                }
            }
        }

        public Vector2i EntityTile(Sprite sprite)
        {
            Vector2i tileIndex = new Vector2i((int)sprite.Position.X / 32, (int)sprite.Position.Y / 32);
            return tileIndex;
        }

        public FloatRect GetTileBounds(Vector2i tileIndex)
        {
            FloatRect selectedTileBounds = new FloatRect(tileIndex.X * 32, tileIndex.Y * 32, (tileIndex.X * 32) + 32, (tileIndex.Y * 32) + 32);             
            return selectedTileBounds;
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            states.Texture = m_tileset[0];
            target.Draw(m_verticesMap, states);

            states.Texture = m_tileset[1];
            target.Draw(m_verticesObjects, states);
        }


    }
}
