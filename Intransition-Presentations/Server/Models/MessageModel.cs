namespace Instend.Server.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}