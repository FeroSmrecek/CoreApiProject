namespace CoreApiProject.Models
{
    [MessagePack.MessagePackObject]
    public class DocumentData
    {
        [MessagePack.Key(0)]
        public string? Data { get; set; }

        [MessagePack.Key(1)]
        public string? Optional { get; set; }
    }
}