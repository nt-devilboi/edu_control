using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using EduControl.Controllers.AppController.model;

namespace EduControl.DataBase.ModelBd;

[Table("status", Schema = "time_control")]
public record Status : IEntity,IUserLink, IName
{
    [Key] [Column("id")] [Required] public Guid Id { get; set; }
    [Column("user_id")] [Required] public Guid UserId { get; set; }
    [Column("name")] [Required] public string Name { get; set; }
    [Column("description")] [Required] public string Desc { get; set; }

    public static Status From(RequestStatus requestStatus, Account account)
        => new()
        {
            Desc = requestStatus.Desc,
            Id = Guid.NewGuid(),
            Name = requestStatus.Name,
            UserId = account.Id
        };
}