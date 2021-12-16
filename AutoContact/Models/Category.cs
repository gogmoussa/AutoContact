using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AutoContact.Models
{
    [Table("Category")]
    public partial class Category
    {
        public Category()
        {
            Parts = new HashSet<Part>();
        }

        [Key]
        public long CategoryId { get; set; }
        [Column("Category")]
        [StringLength(50)]
        public string CategoryName { get; set; }

        [InverseProperty(nameof(Part.Category))]
        public virtual ICollection<Part> Parts { get; set; }
    }
}
