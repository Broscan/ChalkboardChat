using System.ComponentModel.DataAnnotations;

namespace ChalkboardChat.Data.Model
{
	public class ChalkboardModel
	{
		[Key]
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public string Message { get; set; }
		public string Username { get; set; }
		public string UserId { get; set; }
	}
}
