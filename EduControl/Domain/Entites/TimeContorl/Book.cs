using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EduControl.Controllers.AppController.model;

namespace EduControl.DataBase.ModelBd;

[Table("book", Schema = "time_control")]
public record Book : IEntity, IUserLink, IName
{
    [Key] [Column("id")] [Required] public Guid Id { get; set; }
    [Column("name")] [Required] public string Name { get; set; }
    [Column("description")] [Required] public string Desc { get; set; }
    [Column("user_id")] [Required] public Guid UserId { get; set; }

    //public static implicit operator Book(RequestNewBook requestNewBook)
    //{
    //   return From(requestNewBook); // эх а прикольная могла бы выйту штука
    //}
    
  
    public static Book From(RequestNewBook request, Account account)
        => new Book()
        {
            Desc = request.Desc,
            Id = Guid.NewGuid(),
            Name = request.Name,
            UserId = account.Id
        };
}