namespace CoreApiProject.Models
{
    [System.Xml.Serialization.XmlRoot("Documents")]
    [MessagePack.MessagePackObject]
    public class Document
    {
        [MessagePack.Key(0)]
        public required int Id { get; set; }

        [MessagePack.Key(1)]
        public required string Tags { get; set; }

        [MessagePack.Key(2)]
        public DocumentData? Data { get; set; }
    }
}