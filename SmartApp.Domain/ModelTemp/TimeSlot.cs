using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartApp.Domain.ModelTemp;

[Table("TimeSlot")]
public partial class TimeSlot
{
    [Key]
    public int Id { get; set; }

    public int DiningTableId { get; set; }
    [Required]
    public DateOnly ReservationDay { get; set; }

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string MealType { get; set; } = null!;

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string TableStatus { get; set; } = null!;

    
    [ForeignKey("DiningTableId")]
    [InverseProperty("TimeSlots")]
    public virtual DiningTable DiningTable { get; set; } = null!;

    [InverseProperty("TimeSlot")]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
