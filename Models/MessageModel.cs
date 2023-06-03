using Microsoft.EntityFrameworkCore;

namespace task6x.Models
{
    public class MessageModel
    {
        public Guid ID { get; set; }
        public string SenderName { get; set; }
        public string RecipientName { get; set; }
        public string Title { get; set; }
        public string MessageBody { get; set; }
        public DateTime DeliverDate { get; set; }

        public MessageModel(string senderName, string recipientName, string title, string messageBody)
        {
            SenderName = senderName;
            RecipientName = recipientName;
            Title = title;
            MessageBody = messageBody;
            DeliverDate = DateTime.UtcNow.AddHours(3);
        }
    }
}