using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instend.Server.Models
{
    [Table("files")]
    public class FileModel
    {
        [Key][Column("id")] public Guid Id { get; set; }
        [Column("file")] public byte[] File { get; set; } = [];
        [Column("message_id")] public Guid MessageId { get; set; }
    }
}