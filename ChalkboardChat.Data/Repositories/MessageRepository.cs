using ChalkboardChat.Data.AppDbContext;
using ChalkboardChat.Data.Model;
using ChalkboardChat.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChalkboardChat.App.Models
{
    public class MessageRepository : IRepositoryMessage
    {
        private readonly AppDbContext _context;

        public MessageRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task AddMessageToDatabase(ChalkboardModel message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMessageFromDatabase(int id)
        {
            var messageToDelete = await _context.Messages.FindAsync(id);
            if (messageToDelete != null)
            {
                _context.Messages.Remove(messageToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ChalkboardModel> GetMessageByIdAsync(int id)
        {
            return await _context.Messages.FirstOrDefaultAsync(p => p.Id == id)!;
        }


    }

}