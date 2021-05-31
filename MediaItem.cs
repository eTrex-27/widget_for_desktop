using System;
using System.IO;

namespace Number_2C
{
    public class MediaItem
    {
        public MediaItem()
        {

        }

        public MediaItem(String fileName)
        {
            Init(fileName);
        }

        private void Init(String fileName)
        {
            name = Path.GetFileName(fileName);
            path = fileName;
        }

        public String path { get; set; }
        public String name { get; set; }

        public override string ToString()
        {
            return name;
        }

        public void Save(StreamWriter streamWriter)
        {
            streamWriter.WriteLine(path);
        }

        public void Load(StreamReader reader)
        {
            Init(reader.ReadLine());
        }
    }
}
