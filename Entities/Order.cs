using System.ComponentModel.DataAnnotations;

namespace ECommerceMVC.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }
    public double TotalPrice { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}