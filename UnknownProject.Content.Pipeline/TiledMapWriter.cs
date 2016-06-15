using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content.Pipeline;
using UnknownProject.Content.Pipeline.Tiled;

namespace UnknownProject.Content.Pipeline
{
    [ContentTypeWriter]
    public class TiledMapWriter : ContentTypeWriter<TiledMapProcessor.TiledMapProcessorResult>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(TiledMapReader).AssemblyQualifiedName;
        }

        protected override void Write(ContentWriter writer, TiledMapProcessor.TiledMapProcessorResult value)
        {
            var map = value.Map;
            writer.Write(map.Height);
            writer.Write(map.Width);
            writer.Write(map.TileHeight);
            writer.Write(map.TileWidth);

            WriteTileset(writer, map);

            WriteLayers(writer, map);

            // ISSUE #25
        }

        private void WriteTileset(ContentWriter writer, TiledMap map)
        {
            writer.Write(map.Tilesets.Count);
            foreach (var tileset in map.Tilesets)
            {
                // ISSUE #24
                writer.Write(tileset.Image.Source);
                writer.Write(tileset.Image.Width);
                writer.Write(tileset.Image.Height);
                writer.Write(tileset.FirstGid);
                writer.Write(tileset.TileWidth);
                writer.Write(tileset.TileHeight);
                writer.Write(tileset.Spacing);
                writer.Write(tileset.Margin);
                writer.Write(tileset.Tilecount);
                writer.Write(tileset.Columns);


                writer.Write(tileset.Tiles.Count);
                foreach (var tile in tileset.Tiles)
                {
                    writer.Write(tile.Id);
                    // ISSUE #25
                }
            }
        }

        private void WriteLayers(ContentWriter writer, TiledMap map)
        {
            writer.Write(map.Layers.Count);
            foreach (var layer in map.Layers)
            {
                writer.Write(layer.Name);
                writer.Write(layer.Width);
                writer.Write(layer.Height);

                writer.Write(layer.Data.Tiles.Length);
                var tileArray = layer.Data.Tiles;
                for (int y = 0; y < tileArray.GetLength(1); ++y)
                {
                    for (int x = 0; x < tileArray.GetLength(0); ++x)
                    {
                        writer.Write(tileArray[x, y]);
                    }
                }
            }
        }
    }
}
