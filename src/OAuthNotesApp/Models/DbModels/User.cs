namespace OAuthNotesApp.Models.Db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [Key]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(500)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Role { get; set; }
    }
}
