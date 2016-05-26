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
        public String[,] MapNames;
        private GraphicConfiguration graphic;
        private Camera cam;
        private Func<PartMapComponent> partMapProvider;

        private PartMapComponent[,] partMaps;
        private int[] mapHeights;
        private int[] mapWidths;

        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }

        public MapComponent(Func<PartMapComponent> partMapProvider, Camera cam, GraphicConfiguration graphic)
        {
            this.graphic = graphic;
            this.cam = cam;
            this.partMapProvider = partMapProvider;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            var tileWidth = (int)(TileWidth * cam.Zoom);
            var tileHeight = (int)(TileHeight * cam.Zoom);

            var screenStartXWithOffset = (double)cam.Point.X / tileWidth;
            var screenStartYWithOffset = (double)cam.Point.Y / tileHeight;

            var screenStartX = (int)Math.Floor(screenStartXWithOffset);
            var screenStartY = (int)Math.Floor(screenStartYWithOffset);

            var offsetX = (int)((screenStartXWithOffset - screenStartX) * tileWidth);
            var offsetY = (int)((screenStartYWithOffset - screenStartY) * tileHeight);

            var screenTileWidth = Math.Ceiling((double)graphic.Width / tileWidth);
            var screenTileHeight = Math.Ceiling((double)graphic.Height / tileHeight);

            var screenWidthNeededRender = (int)screenTileWidth + screenStartX + 1;
            var screenHeightNeededRender = (int)screenTileHeight + screenStartY + 1;

            int ySize = MapNames.GetLength(0);
            int xSize = MapNames.GetLength(1);

            int currentScreenX = screenStartX;
            int currentScreenY = screenStartY;
            int currentHeight = 0;
            for (int y = getMapHeight(currentScreenY); y < ySize; y++)
            {
                for (int x = getMapWidth(currentScreenX); x < xSize; x++)
                {
                    var part = partMaps[y, x];
                    var pos = part.MapPosition;

                    if (pos.X < screenWidthNeededRender && pos.Y < screenHeightNeededRender)
                    {
                        var finalOffsetX = (currentScreenX - screenStartX) * tileWidth - offsetX;
                        var finalOffsetY = (currentScreenY - screenStartY) * tileHeight - offsetY;
                        currentScreenX = part.Draw(spriteBatch, currentScreenX, screenWidthNeededRender, currentScreenY, screenHeightNeededRender, finalOffsetX, finalOffsetY, tileWidth, tileHeight);

                    }
                    currentHeight = pos.Y + part.MapHeight;
                    if (currentScreenX == screenWidthNeededRender)
                    {
                        break;
                    }
                }
                currentScreenY = currentHeight;
                currentScreenX = screenStartX;
                if (currentScreenY >= screenHeightNeededRender)
                {
                    break;
                }
            }
        }

        private int getMapHeight(int min)
        {
            int lastHeight = 0;
            for (int i = 0; i < mapHeights.Length; i++)
            {
                var height = mapHeights[i];
                if (min == height)
                {
                    return i;
                }
                else if (min < height)
                {
                    return --i;
                }
                lastHeight = height;
            }
            return mapHeights.Length - 1;
        }

        private int getMapWidth(int min)
        {
            int lastWidth = 0;
            for (int i = 0; i < mapWidths.Length; i++)
            {
                var height = mapWidths[i];
                if (min == height)
                {
                    return i;
                }
                else if (min < height)
                {
                    return --i;
                }
                lastWidth = height;
            }
            return mapWidths.Length - 1;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            if (MapNames == null)
            {
                throw new ArgumentNullException("MapNames may not be null");
            }

            int ySize = MapNames.GetLength(0);
            int xSize = MapNames.GetLength(1);

            partMaps = new PartMapComponent[ySize, xSize];
            mapHeights = new int[ySize];
            mapWidths = new int[xSize];

            var pointX = 0;
            var pointY = 0;
            int currentTileWidth = -1;
            int currentTileHeight = -1;

            for (int y = 0; y < ySize; y++)
            {
                int? currentHeight = null;
                for (int x = 0; x < xSize; x++)
                {
                    var partMap = partMapProvider();
                    partMap.MapName = MapNames[y, x];
                    partMap.LoadContent(contentManager);
                    partMap.MapPosition = new Point(pointX, pointY);
                    partMaps[y, x] = partMap;

                    if (y == 0)
                    {
                        mapWidths[x] = pointX;
                    }

                    pointX += partMap.MapWidth;
                    if (currentHeight == null)
                    {
                        currentHeight = partMap.MapHeight;
                    }

                    if (currentHeight != partMap.MapHeight)
                    {
                        throw new ArgumentException("The MapHeight inside a map row may not be different.");
                    }

                    if (currentTileWidth == -1)
                    {
                        currentTileWidth = partMap.TileWidth;
                        currentTileHeight = partMap.TileHeight;
                    }
                    else if (currentTileWidth != partMap.TileWidth || currentTileHeight != partMap.TileHeight)
                    {
                        throw new ArgumentException("The tilewidth and height may not differ.");
                    }
                }
                mapHeights[y] = pointY;
                pointY += partMaps[y, 0].MapHeight;

                pointX = 0;
            }

            TileWidth = currentTileWidth;
            TileHeight = currentTileHeight;
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
