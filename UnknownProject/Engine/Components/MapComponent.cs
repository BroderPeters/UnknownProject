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
    public class MapComponent : DrawableComponent
    {
        private TiledMap map;
        private Texture2D tilesetTexture;

        public String MapName { get { return "desert"; } }

        public Rectangle[] spritePosition;

        public Texture2D[] spriteTextures;
        private GraphicConfiguration graphic;
        private Camera cam;

        public MapComponent(Camera cam, GraphicConfiguration graphic)
        {
            this.graphic = graphic;
            this.cam = cam;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var xx = (double)cam.Point.X / map.TileWidth;
            var yy = (double)cam.Point.Y / map.TileHeight;

            var screenXStart = (int)Math.Floor(xx);
            var screenYStart = (int)Math.Floor(yy);

            var offsetX = (int)((xx - screenXStart) * map.TileWidth);
            var offsetY = (int)((yy - screenYStart) * map.TileHeight);

            var screenWidth = Math.Ceiling((double)graphic.Width / map.TileWidth);
            var screenHeight = Math.Ceiling((double)graphic.Height / map.TileHeight);

            var screenWidthNeededRender = screenWidth + screenXStart + 1;
            var screenHeightNeededRender = screenHeight + screenYStart + 1;

            var finalRenderWidth = (int)(screenWidthNeededRender < map.Width ? screenWidthNeededRender : map.Width);
            var finalRenderHeight = (int)(screenHeightNeededRender < map.Height ? screenHeightNeededRender : map.Height);

            var width = map.TileWidth;
            var height = map.TileHeight;
            foreach (var layer in map.Layers)
            {
                for (int y = screenYStart; y < finalRenderHeight; y++)
                {
                    for (int x = screenXStart; x < finalRenderWidth; x++)
                    {
                        var tileId = layer.Data.Tiles[x, y];
                        if (tileId == 0)
                        {
                            continue;
                        }
                        var xPos = ((x - screenXStart) * width) - offsetX;
                        var yPos = ((y - screenYStart) * height) - offsetY;
                        spriteBatch.Draw(spriteTextures[tileId], new Rectangle(xPos, yPos, width, height), spritePosition[tileId], Color.White);
                    }
                }
            }
        }

        public override void LoadContent(ContentManager contentManager)
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

        public override void Update(GameTime gameTime)
        {

        }
    }
}
