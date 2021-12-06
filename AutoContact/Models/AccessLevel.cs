using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AutoContact.Models
{
    [Table("AccessLevel")]
    public partial class AccessLevel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AccessLevelId { get; set; }
        public long? ClientId { get; set; }
        public long? EmployeeId { get; set; }
        [Required]
        [Column("AccessLevel")]
        [StringLength(20)]
        [Display(Name = "Access Level")]
        public string AccessLevel1 { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty("AccessLevels")]
        public virtual Client Client { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty("AccessLevels")]
        public virtual Employee Employee { get; set; }
    }
}
