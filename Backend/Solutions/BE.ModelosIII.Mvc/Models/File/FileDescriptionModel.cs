using System.Runtime.Serialization;

namespace BE.ModelosIII.Mvc.Models.File
{
    [DataContract]
    public class FileDescriptionModel
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Path { get; set; }

        [DataMember]
        public long Size { get; set; }

        public FileDescriptionModel(string name, string path, long size)
        {
            Name = name;
            Path = path;
            Size = size;
        }
    }
}