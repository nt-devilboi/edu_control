using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EduControl.Controllers.AppController.model;

namespace EduControl.DataBase.ModelBd;

[Table("book", Schema = "edu_control")]
public class Book
{
    [Key] [Column("id")] [Required] public Guid Id { get; set; }
    [Column("name")] [Required] public string Name { get; set; }
    [Column("description")] [Required] public string Desc { get; set; }
    [Column("user_id")] [Required] public Guid UserId { get; set; }

    //public static implicit operator Book(RequestNewBook requestNewBook)
    //{
    //   return From(requestNewBook); // эх а прикольная могла бы выйту штука
    //}
    
    public static Book From(RequestNewBook requestNewBook, Account account)
        => new ()
        {
            Desc = requestNewBook.Desc,
            Id = Guid.NewGuid(),
            Name = requestNewBook.Name,
            UserId = account.Id
        };
}