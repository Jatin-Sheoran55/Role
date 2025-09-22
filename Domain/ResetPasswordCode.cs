using System.ComponentModel.DataAnnotations;

namespace YourAppNamespace.Models
{
    public class ResetPasswordCode
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required, MaxLength(256)]
        public string Email { get; set; }

        [Required, MaxLength(256)]
        public string Code { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime ValidDate { get; set; }

        public DateTime? UsedDate { get; set; }

        [Required]
        public ResetPasswordStatus Status { get; set; } = ResetPasswordStatus.Created;

        [MaxLength(50)]
        public string IpAddress { get; set; }

        // Custom constructor for creating new reset code

        public ResetPasswordCode()
        {

        }
        public ResetPasswordCode(int userId, string email, string ipAddress = null)
        {
            UserId = userId;
            Email = email;
            CreatedDate = DateTime.UtcNow;
            ValidDate = DateTime.UtcNow.AddMinutes(30); // 30 min validity
            Status = ResetPasswordStatus.Created;
            Code = Guid.NewGuid().ToString("N"); // GUID without 
            IpAddress = ipAddress;
        }
    }

    public enum ResetPasswordStatus
    {
        Created = 1,
        Used = 2,
        Expired = 3
    }
}
