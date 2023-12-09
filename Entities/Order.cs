using System.ComponentModel.DataAnnotations;
using ECommerceMVC.Enums;

namespace ECommerceMVC.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }
    public double TotalPrice { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public int UserId { get; set; }
    public User? User { get; set; }

    public List<Product> Products { get; set; } = new List<Product>();
}