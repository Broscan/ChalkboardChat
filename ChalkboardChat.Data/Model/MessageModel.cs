namespace ChalkboardChat.Data.Model;

public class MessageModel
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Message { get; set; }
    public string Username { get; set; }
}