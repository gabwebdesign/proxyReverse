using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIconDB.Entities;

[Table("users")]
public partial class Users
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    
    public int IsActive { get; set; }
    
    public ICollection<Tasks>? Tasks { get; set; }
    
}