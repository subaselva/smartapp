using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartApp.Domain.ModelTemp;

public partial class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;
    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;
    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(128)]
    [Unicode(false)]
    public string? AdObjId { get; set; }

    [StringLength(512)]
    [Unicode(false)]
    public string? ProfileImageUrl { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
