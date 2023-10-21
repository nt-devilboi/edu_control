using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace EduControl.DataBase.ModelBd;

[Table("status", Schema = "edu_control")]
public class Status
{
    [Key] [Column("id")] [Required] public Guid Id { get; set; }
    [Column("user_id")] [Required] public Guid UserId { get; set; }
    [Column("name")] [Required] public string Name { get; set; }
    [Column("description")] [Required] public string Desc { get; set; }
}