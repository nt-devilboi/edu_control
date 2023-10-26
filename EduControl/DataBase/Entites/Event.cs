using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduControl.DataBase.ModelBd;

[Table("event", Schema = "edu_control")]
public record Event
{
    [Key] [Column("id")] [Required] public Guid Id { get; set; }
    [Column("status_id")] [Required] public Guid StatusId { get; set; }
    [Column("book_id")] [Required] public Guid BookId { get; set; }
    [Column("name")] [Required] public DateTime Start { get; set; }
}