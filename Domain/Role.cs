using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Role
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
