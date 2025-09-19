using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Blog
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public bool IsPublished { get; set; } = true;

    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }

    [ForeignKey("EmployeeId")]
    public int EmployeeId { get; set; }

    public Employee Employee { get; set; }
}
