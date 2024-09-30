using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instend.Server.Models
{
    [Table("chats")]
    public class ChatModel
    {
        [Column("id")][Key] Guid Id { get; set; } = Guid.NewGuid();
        [Column("username")][Key] string Username { get; set; } = string.Empty;
        [Column("owner")][Key] string Owner { get; set; } = string.Empty;
    }
}