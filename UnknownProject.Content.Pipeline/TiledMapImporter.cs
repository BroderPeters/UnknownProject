using Microsoft.Xna.Framework.Content.Pipeline;
using MonoGame.Framework.Content.Pipeline.Builder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnknownProject.Content.Pipeline.Tiled;


namespace UnknownProject.Content.Pipeline
{
    [ContentImporter(".tmx", DefaultProcessor = "TiledMapProcessor",
    DisplayName = "TMX Importer - UnknownProject")]
    public class TiledMapImporter : ContentImporter<TiledMap>
    {
        public override TiledMap Import(string filename, ContentImporterContext context)
        {
            context.Logger.LogMessage("Importing tmx file: {0}", filename);
            var fileDir = Path.GetDirectoryName(filename);
            var outputDir = Path.GetDirectoryName(context.OutputDirectory);

            using (var streamReader = new StreamReader(filename))
            {
                var deserializer = new XmlSerializer(typeof(TiledMap));
                var tiledMap = (TiledMap)deserializer.Deserialize(streamReader);

                foreach (var set in tiledMap.Tilesets)
                {
                    var source = set.Image.Source;
                    var withOutExt = Path.GetFileNameWithoutExtension(source);
                    set.Image.Source = getStartPath(fileDir, outputDir) + withOutExt;
                    context.Logger.LogMessage("ImagePath: " + set.Image.Source);
                }

                return tiledMap;
            }
        }

        private String getStartPath(String fileDir, String outputDir)
        {
            var equalPath = getEqualFilePath(fileDir, outputDir);
            if (Path.GetDirectoryName(equalPath) != fileDir)
            {
                var endpath = fileDir.Remove(0, equalPath.Length);
                return endpath + Path.DirectorySeparatorChar;
            }
            return String.Empty;
        }

        private String getEqualFilePath(String fileDir, String otherDir)
        {
            var splitter = fileDir.Contains(Path.AltDirectorySeparatorChar) ? Path.AltDirectorySeparatorChar : Path.DirectorySeparatorChar;

            string[] fileSplit = fileDir.Split(splitter);
            string[] otherSplit = otherDir.Split(splitter);

            var result = "";
            for (int i = 0; i < fileSplit.Length && i < otherSplit.Length; i++)
            {
                var filePart = fileSplit[i];
                var otherPart = otherSplit[i];
                if (filePart == otherPart)
                {
                    result += filePart + splitter;
                }
                else break;
            }
            return result;
        }
    }
}
