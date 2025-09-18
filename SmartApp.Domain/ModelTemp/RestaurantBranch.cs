using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartApp.Domain.ModelTemp;

public partial class RestaurantBranch
{
    [Key]
    public int Id { get; set; }

    public int RestaurantId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string Address { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("ImageURL")]
    [StringLength(500)]
    [Unicode(false)]
    public string? ImageUrl { get; set; }

    [InverseProperty("Branch")]
    public virtual ICollection<DiningTable> DiningTables { get; set; } = new List<DiningTable>();

    [ForeignKey("RestaurantId")]
    [InverseProperty("RestaurantBranches")]
    public virtual Restaurant Restaurant { get; set; } = null!;

   
}
