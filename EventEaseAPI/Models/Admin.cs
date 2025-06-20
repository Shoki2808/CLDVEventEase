using System.ComponentModel.DataAnnotations;

namespace EventEaseAPI.Models
{
    public partial class Admin : BaseModel
    {
        [Key]
        public int AdminId { get; set; }

        [Required]
        [StringLength(100)]
        public string AdminName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string AdminEmail { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string AdminRole { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string AdminPosition { get; set; } = null!;

        [StringLength(200)]
        public string? Action { get; set; }
    }
}
