using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnknownProject.Content.Pipeline.Tiled;

namespace UnknownProject.Content.Pipeline
{
    [ContentProcessor(DisplayName = "Tiled Map Processor - UnknownProject")]
    public class TiledMapProcessor : ContentProcessor<TiledMap, TiledMapProcessor.TiledMapProcessorResult>
    {
        public override TiledMapProcessorResult Process(TiledMap input, ContentProcessorContext context)
        {
            var layers = input.Layers;

            foreach(var layer in layers)
            {
                var data = layer.Data;
                data.Tiles = DecodeBase64Data(layer);
            }

            return new TiledMapProcessorResult(input, context.Logger);
        }

        private static uint[,] DecodeBase64Data(TiledLayer layer)
        {
            var width = layer.Width;
            var height = layer.Height;
            var tileList = new uint[width,height];

            var data = layer.Data;
            var encodedData = data.Value.Trim();
            var decodedData = Convert.FromBase64String(encodedData);

            using (var stream = OpenStream(decodedData, data.Compression))
            using (var reader = new BinaryReader(stream))
            {
                for (var y = 0; y < width; y++)
                {
                    for (var x = 0; x < height; x++)
                    {
                        tileList[x, y] = reader.ReadUInt32();
                    }
                }
            }

            return tileList;
        }

        private static GZipStream OpenStream(byte[] decodedData, string compressionMode)
        {
            return new GZipStream(new MemoryStream(decodedData, writable: false), CompressionMode.Decompress);
        }

        public class TiledMapProcessorResult
        {
            public TiledMap Map { get; private set; }
            public ContentBuildLogger Logger { get; private set; }

            public TiledMapProcessorResult(TiledMap map, ContentBuildLogger logger)
            {
                Map = map;
                Logger = logger;
            }

            
        }
    }
}
