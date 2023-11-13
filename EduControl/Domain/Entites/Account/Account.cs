using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EduControl.Controllers.Model;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace EduControl.DataBase.ModelBd;

[Table("account", Schema = "time_control")]
public class Account
{
    [Column("id")] [Key] public Guid Id { get; set; }
    [Column("user_name")] public string UserName { get; set; }
    [Column("email")] public string Email { get; set; }
    [Column("password_hash")] public string PasswordHash { get; set; }

    public static implicit operator Account(RequestNewUser newUser) 
        => From(newUser);
    
    public static Account From(RequestNewUser entity)
        => new Account()
        {
            Email = entity.Email,
            PasswordHash = Hasher.Password(entity.Password),
            UserName = entity.UserName,
            Id = Guid.NewGuid()
        };

    public bool IsMy(IUserLink entity)
        => entity.UserId == Id;
}