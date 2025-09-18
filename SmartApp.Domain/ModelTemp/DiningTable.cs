using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartApp.Domain.ModelTemp;

public partial class DiningTable
{
    [Key]
    public int Id { get; set; }

    public int RestaurantBranchId { get; set; }

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string? TableName { get; set; }

    [Required]
    public int Capacity { get; set; }

    [ForeignKey("RestaurantBranchId")]
    [InverseProperty("DiningTables")]
    public virtual RestaurantBranch Branch { get; set; } = null!;

    [InverseProperty("DiningTable")]
    public virtual ICollection<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
}
