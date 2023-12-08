using System.ComponentModel.DataAnnotations;

namespace ECommerceMVC.Entities;

public class Cart
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
}