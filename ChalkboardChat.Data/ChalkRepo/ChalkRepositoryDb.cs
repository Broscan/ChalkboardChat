using ChalkboardChat.Data.Database;
using ChalkboardChat.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace ChalkboardChat.Data.ChalkRepo;

public class ChalkRepositoryDb
{
    private readonly AppDbContext context;

    public ChalkRepositoryDb(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<MessageModel> GetMessageByIdAsync(int id)
    {
        return await context.Message.FirstOrDefaultAsync(p => p.Id == id)!;
    }

    // Hämta alla messages.
    public async Task<IEnumerable<MessageModel>> GetAllMessagesAsync()
    {
        return await context.Message.ToListAsync();
    }

    // Hämta ett meddelande
    public async Task<MessageModel?> GetMessageByNameAsync(string name)
    {
        return await context.Message.FirstOrDefaultAsync(user => user.Username.ToLower() == name.ToLower());
    }

    // Adda ett message till db 
    public async Task<IEnumerable<MessageModel>> AddMessageAsync(MessageModel message)
    {
        context.Message.Add(message);

        await context.SaveChangesAsync();

        return await GetAllMessagesAsync();
    }

    // Delete med Id?
    public async Task<IEnumerable<MessageModel>> DeleteMessageAsync(int id)
    {
        var message = await context.Message.FirstOrDefaultAsync(m => m.Id == id);
        if (message != null)
        {
            context.Message.Remove(message);
            await context.SaveChangesAsync();
        }

        return await GetAllMessagesAsync();
    }

    //Uppdatera message med ID?
    public async Task<IEnumerable<MessageModel>> UpdateMessageAsync(MessageModel updatedMessage)
    {
        var existingMessage = await context.Message.FirstOrDefaultAsync(m => m.Id == updatedMessage.Id);

        if (existingMessage != null)
        {
            existingMessage.Date = updatedMessage.Date;
            existingMessage.Message = updatedMessage.Message;
            existingMessage.Username = updatedMessage.Username;

            await context.SaveChangesAsync();
        }

        return await GetAllMessagesAsync();
    }
}
