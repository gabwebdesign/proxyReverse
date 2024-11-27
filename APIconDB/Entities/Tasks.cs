using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIconDB.Entities;

[Table("tasks")]
public partial class Tasks
{
    

    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? DueDate { get; set; }
    public string State { get; set; }
    
    [ForeignKey("UserId")]
    public int UserId { get; set; }

}