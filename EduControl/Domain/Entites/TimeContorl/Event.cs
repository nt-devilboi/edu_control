using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using EduControl.Controllers.AppController.model;

namespace EduControl.DataBase.ModelBd;

[Table("event", Schema = "time_control")]
public record Event
{
    [Key] [Column("id")] [Required] public Guid Id { get; set; }
    [Column("status_id")] [Required] public Guid StatusId { get; set; }
    [Column("book_id")] [Required] public Guid BookId { get; set; }
    [Column("start")] [Required] public DateTime Start { get; set; }
    [Column("end")]  public DateTime End { get; set; }

    public static implicit operator Event(RequestEvent requestEvent)
        => From(requestEvent);

    public static Event From(RequestEvent requestEvent)
        => new()
        {
            Id = Guid.NewGuid(),
            BookId = requestEvent.BookId,
            StatusId = requestEvent.StatusId,
            Start = DateTime.UtcNow
        };

    public TimeSpan GetTime()
    {
        return End - Start;
    }
}