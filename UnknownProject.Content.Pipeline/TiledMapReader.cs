using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnknownProject.Content.Pipeline.Tiled;

namespace UnknownProject.Content.Pipeline
{
    public class TiledMapReader : ContentTypeReader<TiledMap>
    {
        protected override TiledMap Read(ContentReader reader, TiledMap existingInstance)
        {
            var tiledMap = new TiledMap(
                height: reader.ReadInt32(),
                width: reader.ReadInt32(),
                tileHeight: reader.ReadInt32(),
                tileWidth: reader.ReadInt32()
            );

            ReadTilesets(reader, tiledMap);

            ReadLayers(reader, tiledMap);

            return tiledMap;
        }

        private void ReadTilesets(ContentReader reader, TiledMap tiledMap)
        {
            var tilesetCount = reader.ReadInt32();

            for (var i = 0; i < tilesetCount; i++)
            {
                
                var tileset = new TiledTileset();
                tileset.FirstGid = reader.ReadInt32();
                tileset.TileWidth = reader.ReadInt32();
                tileset.TileHeight = reader.ReadInt32();
                tileset.Spacing = reader.ReadInt32();
                tileset.Margin = reader.ReadInt32();
                tileset.Tilecount = reader.ReadInt32();
                tileset.Columns = reader.ReadInt32();
                // ISSUE #24 read tileset source


                var tileSetTileCount = reader.ReadInt32();
                for (var j = 0; j < tileSetTileCount; j++)
                {
                    var tileId = reader.ReadInt32();
                    tileId = tileset.FirstGid + tileId - 1;

                    var tiledTile = new TiledTile();
                    tiledTile.Id = tileId;
                    // ISSUE #24
                    tileset.Tiles.Add(tiledTile);
                }

                tiledMap.Tilesets.Add(tileset);
            }
        }

        private void ReadLayers(ContentReader reader, TiledMap tiledMap)
        {
            var layerCount = reader.ReadInt32();

            for (var i = 0; i < layerCount; i++)
            {
                var layer = new TiledLayer();
                layer.Data = new TiledData();
                layer.Name = reader.ReadString();
                layer.Width = reader.ReadInt32();
                layer.Height = reader.ReadInt32();

                var tileDataCount = reader.ReadInt32();
                layer.Data.Tiles = new uint[layer.Width, layer.Height];
                for (int y = 0; y < layer.Height; ++y)
                {
                    for (int x = 0; x < layer.Width; ++x)
                    {
                        layer.Data.Tiles[x, y] = reader.ReadUInt32();
                    }
                }

                tiledMap.Layers.Add(layer);
            }
        }
    }
}
