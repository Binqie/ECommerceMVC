using System.ComponentModel.DataAnnotations;

namespace ECommerceMVC.Entities;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int Count { get; set; }
    public double Price { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public List<Order> Orders { get; set; } = new List<Order>();
}