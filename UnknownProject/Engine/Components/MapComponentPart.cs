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
    public class MapComponentPart
    {
        private TiledMap map;
        private Texture2D tilesetTexture;

        public String MapName { get; set; }

        public Rectangle[] spritePosition;

        public Texture2D[] spriteTextures;
        private GraphicConfiguration graphic;
        private Camera cam;

        public Vector2 Position { get; set; }

        public MapComponentPart(Camera cam, GraphicConfiguration graphic)
        {
            this.graphic = graphic;
            this.cam = cam;
            Position = new Vector2();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, MapComponent.NeededToRender toRender)
        {
         

            var finalRenderWidth = (int)(toRender.NeededRenderWidth < Position.X + map.Width ? toRender.NeededRenderWidth : Position.X + map.Width);
            var finalRenderHeight = (int)(toRender.NeededRenderHeight < Position.Y + map.Height ? toRender.NeededRenderHeight : Position.Y + map.Height);

            var width = map.TileWidth;
            var height = map.TileHeight;

            var yStart = toRender.RenderedHeight - (int) Position.Y;
            var xStart = toRender.RenderedWidth - (int) Position.X;

            var yEnd = finalRenderHeight - Position.Y;
            var xEnd = finalRenderWidth - Position.X;

            if(xStart < 0 || yStart < 0) { return; }

            


            foreach (var layer in map.Layers)
            {
                for (int y = yStart; y < yEnd; y++)
                {
                    for (int x = xStart; x < xEnd; x++)
                    {
                        var tileId = layer.Data.Tiles[x, y];
                        if (tileId == 0)
                            continue;
                        var xPos = (x) * width - 0;
                        var yPos = (y) * height - 0;
                        spriteBatch.Draw(spriteTextures[tileId], new Rectangle(xPos, yPos, width, height), spritePosition[tileId], Color.White);
                    }
                }
            }
            toRender.RenderedWidth = finalRenderWidth;
        }

        public void LoadContent(ContentManager contentManager)
        {
            map = contentManager.Load<TiledMap>(MapName);

            // ISSUE #24 texture should get loaded in tileset
            tilesetTexture = contentManager.Load<Texture2D>("tmw_desert_spacing");

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
                    spriteTextures[set.FirstGid + i] = tilesetTexture; // ISSUE #24 they should be loaded in the tileset
                }
            }
        }
    }
}
