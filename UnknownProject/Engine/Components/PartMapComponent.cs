using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using UnknownProject.Components.Core;
using UnknownProject.Content.Pipeline.Tiled;
using UnknownProject.Core;

namespace UnknownProject.Engine.Components
{

    public class PartMapComponent
    {
        private const uint NoTileId = 0;
        private TiledMap map;

        public String MapName { get; set; }
        public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }
        public Point MapPosition { get; set; }

        public int TileWidth { get; set; }
        public int TileHeight { get; set; }

        public Rectangle[] spritePosition;

        public Texture2D[] spriteTextures;


        public int Draw(SpriteBatch spriteBatch, int startX, int endX, int startY, int endY, int offsetX, int offsetY, int tileWidthWithZoom, int tileHeightWithZoom)
        {
            var possibleRenderWidth = MapPosition.X + map.Width;
            var possibleRenderHeight = MapPosition.Y + map.Height;

            var finalRenderWidth = endX < possibleRenderWidth ? endX : possibleRenderWidth;
            var finalRenderHeight = endY < possibleRenderHeight ? endY : possibleRenderHeight;

            foreach (var layer in map.Layers)
            {
                for (int y = startY; y < finalRenderHeight; y++)
                {
                    for (int x = startX; x < finalRenderWidth; x++)
                    {
                        var tileId = layer.Data.Tiles[x - MapPosition.X, y - MapPosition.Y];
                        if (tileId == NoTileId)
                        {
                            continue;
                        }
                        var xPos = ((x - startX) * tileWidthWithZoom) + offsetX;
                        var yPos = ((y - startY) * tileHeightWithZoom) + offsetY;
                        spriteBatch.Draw(spriteTextures[tileId], new Rectangle(xPos, yPos, tileWidthWithZoom, tileHeightWithZoom), spritePosition[tileId], Color.White);
                    }
                }
            }
            return finalRenderWidth;
        }

        public void LoadContent(ContentManager contentManager)
        {
            if (MapName == null)
            {
                throw new ArgumentNullException("The MapName may not be null");
            }

            map = contentManager.Load<TiledMap>(MapName);

            var maxCount = 1;
            foreach (var set in map.Tilesets)
            {
                maxCount += set.Tilecount;
            }

            spriteTextures = new Texture2D[maxCount];
            spritePosition = new Rectangle[maxCount];


            foreach (var set in map.Tilesets)
            {
                for (int i = 0; i < set.Tilecount; i++)
                {
                    var rec = new Rectangle();

                    var y = (int)Math.Floor(((decimal)i / (decimal)set.Columns));
                    var x = i % set.Columns;

                    rec.X = x * set.TileWidth + x * set.Spacing + set.Margin;
                    rec.Y = y * set.TileHeight + y * set.Spacing + set.Margin;

                    rec.Width = set.TileWidth;
                    rec.Height = set.TileHeight;

                    spritePosition[set.FirstGid + i] = rec;
                    spriteTextures[set.FirstGid + i] = set.Image.SpriteTexture;
                }
            }

            MapWidth = map.Width;
            MapHeight = map.Height;
            TileWidth = map.TileWidth;
            TileHeight = map.TileHeight;
        }
    }
}
