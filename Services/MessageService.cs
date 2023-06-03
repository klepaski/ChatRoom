using task6x.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace task6x.Services
{
    public interface IMessageService
    {
        public Task<List<UserModel>> GetUsersAsync();
        public Task CreateUserAsync(string userName);
        public Task SendMessage(string senderName, string recipientName, string title, string messageBody);
        public Task<List<MessageModel>> GetMessagesForUserAsync(string userName);
    }

    public class MessageService : Hub, IMessageService
    {
        private readonly ILogger<MessageService> _logger;
        private readonly ChatContext _db;

        public MessageService(ILogger<MessageService> logger, ChatContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task SendMessage(string senderName, string recipientName, string title, string messageBody)
        {
            MessageModel newMessage = new MessageModel(senderName, recipientName, title, messageBody);
            _db.Messages.Add(newMessage);
            await _db.SaveChangesAsync();
            await Clients.All.SendAsync("ReceiveMessage", recipientName);
        }

        public async Task CreateUserAsync(string userName)
        {
            UserModel newUser = new UserModel(userName);
            if (_db.Users.Find(userName) != null) return;
            _db.Users.Add(newUser);
            await _db.SaveChangesAsync();
        }

        public async Task<List<UserModel>> GetUsersAsync()
        {
            List<UserModel> users = await _db.Users.ToListAsync();
            return users;
        }

        public async Task<List<MessageModel>> GetMessagesForUserAsync(string userName)
        {
            List<MessageModel> messages = await _db.Messages
                .Where(x => x.RecipientName == userName)
                .OrderByDescending(x => x.DeliverDate)
                .ToListAsync();
            return messages;
        }
    }
}
