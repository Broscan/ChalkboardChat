using ChalkboardChat.Data.Model;

namespace ChalkboardChat.Data.Repositories
{
    public interface IRepositoryMessage
    {
        Task AddMessageToDatabase(ChalkboardModel message);
        Task DeleteMessageFromDatabase(int id);
    }
}
