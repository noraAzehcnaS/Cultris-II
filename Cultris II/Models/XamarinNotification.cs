namespace Cultris_II.Models
{
    public class XamarinNotification
    {
        private static int NextId = 300;
        public readonly int Id;
        public string Title { get; set; }
        public string Message { get; set; }
        public XamarinNotification(string title, string message)
        {
            Id = ++NextId;
            Title = title;
            Message = message;
        }
    }
}
