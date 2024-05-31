//Written for Break Arcade Games Out. https://store.steampowered.com/app/1145020
using System.IO;

namespace Break_Arcade_Games_Out_Extractor
{
    class Program
    {
        public static BinaryReader br;
        static void Main(string[] args)
        {
            br = new(File.OpenRead(args[0]));
            br.ReadInt32();
            int fileCount = br.ReadInt32();

            System.Collections.Generic.List<int> startsEnds = new();
            for (int i = 0; i < fileCount; i++)
                startsEnds.Add(br.ReadInt32());

            startsEnds.Add(br.ReadInt32());

            string path = Path.GetDirectoryName(args[0]) + "//" + Path.GetFileNameWithoutExtension(args[0]);
            Directory.CreateDirectory(path);
            for (int i = 0; i < fileCount; i++)
            {
                br.BaseStream.Position = startsEnds[i];
                br.ReadByte();
                BinaryWriter bw = new(File.Create(path + "//" + i));
                bw.Write(br.ReadBytes(startsEnds[i + 1] - startsEnds[i]));
                bw.Close();
            }
        }
    }
}
